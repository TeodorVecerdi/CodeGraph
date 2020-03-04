using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace CodeGraph.Editor {
    public class CodeGraphView : GraphView {
        public readonly Vector2 DefaultNodeSize = new Vector2(200, 150);

        private SearchWindowProvider searchWindowProvider;
        private CodeGraph editorWindow;

        public CodeGraphView(CodeGraph editorWindow) {
            this.editorWindow = editorWindow;
            this.AddStyleSheet("CodeGraph");
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            searchWindowProvider = ScriptableObject.CreateInstance<SearchWindowProvider>();
            searchWindowProvider.Initialize(this.editorWindow, this);
            nodeCreationRequest = c => SearchWindow.Open(new SearchWindowContext(c.screenMousePosition), searchWindowProvider);
            graphViewChanged += OnGraphViewChanged;
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
            if (graphViewChange.edgesToCreate?.Count > 0) {
                graphViewChange.edgesToCreate.ForEach(edge => {
                    var input = edge.input.node as AbstractNode;
                    var output = edge.output.node as AbstractNode;
                    EventExtenderNode extenderNode = null;
                    AbstractNode other = null;
                    if (input is EventExtenderNode && edge.input.direction != Direction.Output) {
                        extenderNode = input as EventExtenderNode;
                        other = output;
                    } else if (output is EventExtenderNode && edge.output.direction != Direction.Output) {
                        extenderNode = output as EventExtenderNode;
                        other = input;
                    }

                    if (extenderNode != null) {
                        extenderNode.UpdateSourceTitle(other.title);
                    }
                });
            }

            return graphViewChange;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
            var compatiblePorts = new List<Port>();
            ports.ForEach(port => {
                if (startPort != port && startPort.node != port.node && port.direction != startPort.direction)
                    compatiblePorts.Add(port);
            });
            return compatiblePorts;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
            Vector2 mousePosition = evt.mousePosition;
            base.BuildContextualMenu(evt);
            
            /*if (evt.target is GraphView) {
                evt.menu.InsertAction(1, "Create Sticky Note", e => AddStickyNote(mousePosition));
            }*/

            if (evt.target is GraphView || evt.target is Node) {
                evt.menu.AppendAction("Group Selection", GroupSelection, (a) => {
                    var filteredSelection = new List<ISelectable>();
                    foreach (ISelectable selectedObject in selection) {
                        if (selectedObject is Group)
                            return DropdownMenuAction.Status.Disabled;
                        var visualElement = selectedObject as AbstractNode;
                        filteredSelection.Add(visualElement);
                    }

                    return filteredSelection.Count > 0 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
                });

                evt.menu.AppendAction("Ungroup Selection", RemoveFromGroupNode, (a) => {
                    var filteredSelection = new List<ISelectable>();
                    foreach (ISelectable selectedObject in selection) {
                        if (selectedObject is Group)
                            return DropdownMenuAction.Status.Disabled;
                        var visualElement = selectedObject as AbstractNode;
                        if (visualElement.GetContainingScope() is Group)
                            filteredSelection.Add(selectedObject);
                    }

                    return filteredSelection.Count > 0 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
                });
            }
        }

        private void GroupSelection(DropdownMenuAction action) {
            var title = "New Group";
            var groupData = new GroupData(title, new Vector2(10f, 10f));
            editorWindow.GraphObject.CodeGraphData.CreateGroup(groupData);
            foreach (var node in selection.OfType<AbstractNode>()) {
                editorWindow.GraphObject.CodeGraphData.SetGroup(node, groupData);
            }
        }

        private void RemoveFromGroupNode(DropdownMenuAction action) {
            foreach (var selectable in selection) {
                var node = selectable as Node;
                if (node == null)
                    continue;

                if (node.GetContainingScope() is Group group) {
                    group.RemoveElement(node);
                }
            }
        }

        /*public CodeNode CreateNode(string nodeName, int inputPorts = 1, int outputPorts = 1) {
            var node = new CodeNode {
                title = nodeName,
                GUID = Guid.NewGuid().ToString()
            };
            node.AddStyleSheet("CodeNode");
            for (var i = 0; i < inputPorts; i++) {
                var port = node.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
                port.portName = $"Input {i + 1}";
                port.portColor = Random.ColorHSV();
                node.inputContainer.Add(port);
            }

            for (var i = 0; i < outputPorts; i++) {
                var port = node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
                port.portName = $"Output {i + 1}";
                port.portColor = Random.ColorHSV();
                node.outputContainer.Add(port);
            }

            node.RefreshExpandedState();
            node.RefreshPorts();
            node.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
            return node;
        }*/
    }
}