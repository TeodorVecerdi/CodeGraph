using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class CodeGraphView : GraphView {
        public readonly Vector2 DefaultNodeSize = new Vector2(200, 150);
        public readonly List<CreateMethodNode> CreateMethodNodes = new List<CreateMethodNode>();
        public readonly Dictionary<Group, GroupData> GroupDictionary = new Dictionary<Group, GroupData>();
        public readonly Dictionary<Guid, GroupData> GroupGuidDictionary = new Dictionary<Guid, GroupData>();

        private readonly CodeGraph editorWindow;

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

            var searchWindowProvider = ScriptableObject.CreateInstance<SearchWindowProvider>();
            searchWindowProvider.Initialize(this.editorWindow, this);
            nodeCreationRequest = c => SearchWindow.Open(new SearchWindowContext(c.screenMousePosition), searchWindowProvider);
            graphViewChanged += OnGraphViewChanged;
            serializeGraphElements += SerializeGraphElementsImplementation;

            // canPasteSerializedData = CanPasteSerializedDataImplementation;
            unserializeAndPaste += UnserializeAndPasteImplementation;

            // deleteSelection = DeleteSelectionImplementation;
        }

        private void UnserializeAndPasteImplementation(string operationName, string data) {
            Undo.RegisterCompleteObjectUndo(CodeGraph.Instance.GraphObject, operationName);
            var pastedGraph = CopyPasteGraphData.FromJson(data);
            this.InsertCopyPasteGraph(pastedGraph);
        }

        private string SerializeGraphElementsImplementation(IEnumerable<GraphElement> elements) {
            // var groups = elements.OfType<ShaderGroup>().Select(x => x.userData);
            var nodes = elements.OfType<AbstractNode>().ToList();
            var edges = elements.OfType<Edge>().ToList();

            // var inputs = selection.OfType<BlackboardField>().Select(x => x.userData as ShaderInput);
            // var notes = enumerable.OfType<StickyNote>().Select(x => x.userData);

            // Collect the property nodes and get the corresponding properties
            // var propertyNodeGuids = nodes.OfType<PropertyNode>().Select(x => x.propertyGuid);
            // var metaProperties = this.graph.properties.Where(x => propertyNodeGuids.Contains(x.guid));

            // Collect the keyword nodes and get the corresponding keywords
            // var keywordNodeGuids = nodes.OfType<KeywordNode>().Select(x => x.keywordGuid);
            // var metaKeywords = this.graph.keywords.Where(x => keywordNodeGuids.Contains(x.guid));
            var assetGUID = AssetDatabase.AssetPathToGUID(CodeGraph.Instance.GraphObject.CodeGraphData.AssetPath);
            var graph = new CopyPasteGraphData(assetGUID, nodes, edges);
            return JsonUtility.ToJson(graph, true);
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
            editorWindow.InvalidateSaveButton();
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

                    extenderNode?.UpdateSourceTitle(other.title);
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
                evt.menu.AppendAction("Group Selection", action => GroupSelection(), (a) => {
                    var filteredSelection = new List<ISelectable>();
                    foreach (ISelectable selectedObject in selection) {
                        if (selectedObject is Group)
                            return DropdownMenuAction.Status.Disabled;
                        var visualElement = selectedObject as AbstractNode;
                        filteredSelection.Add(visualElement);
                    }

                    return filteredSelection.Count > 0 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
                });

                evt.menu.AppendAction("Ungroup Selection", action => UngroupSelection(), (a) => {
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

        public void GroupSelection() {
            var validSelection = selection.OfType<AbstractNode>().ToList();
            if (validSelection.Count == 0) {
                return;
            }

            editorWindow.InvalidateSaveButton();
            var group = new Group {title = "New Group"};
            var groupData = new GroupData("New Group", group);
            editorWindow.GraphObject.CodeGraphData.AddGroup(groupData);
            foreach (var node in validSelection) {
                if (node.GetContainingScope() is Group previousGroup) {
                    RemoveElement(previousGroup);
                }

                editorWindow.GraphObject.CodeGraphData.SetGroup(node, groupData);
            }

            group.AddElements(validSelection);
            AddElement(group);
            GroupDictionary.Add(group, groupData);
            GroupGuidDictionary.Add(groupData.Guid, groupData);
        }

        public void UngroupSelection() {
            editorWindow.InvalidateSaveButton();
            foreach (var selectable in selection) {
                if (!(selectable is Node node) || !(node.GetContainingScope() is Group group))
                    continue;

                ((IGroupItem) node).GroupGuid = Guid.Empty;
                group.RemoveElement(node);
                if (!group.containedElements.Any())
                    RemoveGroup(group);
            }
        }

        private void RemoveGroup(Group group) {
            RemoveElement(group);
            var groupData = GroupDictionary[group];
            GroupDictionary.Remove(group);
            GroupGuidDictionary.Remove(groupData.Guid);
            editorWindow.GraphObject.CodeGraphData.Groups.Remove(groupData);
            editorWindow.GraphObject.CodeGraphData.GroupItems[groupData.Guid].ForEach(item => item.GroupGuid = Guid.Empty);
            editorWindow.GraphObject.CodeGraphData.GroupItems.Remove(groupData.Guid);
        }
    }
}