using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class SaveUtility {
        private List<Edge> Edges => graphView.edges.ToList();
        private List<AbstractNode> Nodes => graphView.nodes.ToList().Cast<AbstractNode>().ToList();
        private CodeGraphObject graphObject;
        private CodeGraphView graphView;

        public static SaveUtility GetInstance(CodeGraphView graphView) => new SaveUtility {graphView = graphView};

        public CodeGraphObject Save(string fileName) {
            graphObject = ScriptableObject.CreateInstance<CodeGraphObject>();
            graphObject.Initialize(new CodeGraphData {AssetPath = fileName});
            var connectedEdges = Edges.Where(x => x.input.node != null).ToArray();
            foreach (var edge in connectedEdges) {
                var inputNode = edge.input.node as AbstractNode;
                var outputNode = edge.output.node as AbstractNode;
                var inputPortIndex = -1;
                for (var i = 0; i < inputNode.InputPorts.Count; i++) {
                    if (edge.input == inputNode.InputPorts[i].PortReference) {
                        inputPortIndex = i;
                        break;
                    }
                }

                var outputPortIndex = -1;
                for (var i = 0; i < outputNode.OutputPorts.Count; i++) {
                    if (edge.output == outputNode.OutputPorts[i].PortReference) {
                        outputPortIndex = i;
                        break;
                    }
                }

                graphObject.CodeGraphData.Edges.Add(new SerializedEdge {
                    SourceNodeGUID = outputNode.GUID,
                    SourceNodeIndex = outputPortIndex,
                    TargetNodeGUID = inputNode.GUID,
                    TargetNodeIndex = inputPortIndex
                });
            }

            foreach (var node in Nodes) {
                graphObject.CodeGraphData.Nodes.Add(new SerializedNode {
                    GUID = node.GUID,
                    NodeType = node.GetType().Name,
                    Position = node.GetPosition().position,
                    NodeData = node.GetNodeData()
                });
            }

            var startIndex = fileName.LastIndexOf('/');
            var endIndex = fileName.LastIndexOf('.');
            var graphName = fileName.Substring(startIndex + 1, endIndex - startIndex - 1);
            graphObject.CodeGraphData.LastEditedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            graphObject.CodeGraphData.GraphName = graphName;
            File.WriteAllText(fileName, JsonUtility.ToJson(graphObject.CodeGraphData, true));
            AssetDatabase.Refresh();
            return graphObject;
        }

        public void LoadGraph(CodeGraphObject graphObject) {
            this.graphObject = graphObject;
            ClearGraph();
            GenerateNodes();
            ConnectNodes();
        }

        private void ClearGraph() {
            foreach (var node in Nodes) {
                Edges.Where(x => x.input.node == node).ToList().ForEach(edge => graphView.RemoveElement(edge));
                graphView.RemoveElement(node);
            }
        }

        private void GenerateNodes() {
            foreach (var serializedNode in graphObject.CodeGraphData.Nodes) {
                var type = Type.GetType("CodeGraph.Editor." + serializedNode.NodeType);

                // Debug.Log("CodeGraph.Nodes." + serializedNode.NodeType + " | TEST: "+ typeof(StartEventNode).FullName);
                var tempNode = (AbstractNode) Activator.CreateInstance(type);
                tempNode.GUID = serializedNode.GUID;
                tempNode.SetNodeData(serializedNode.NodeData);
                tempNode.SetPosition(new Rect(serializedNode.Position, AbstractNode.DefaultNodeSize));
                if (serializedNode.NodeType == "StartEventNode") {
                    var startEventNode = tempNode as StartEventNode;
                    graphView.StartEventNode = startEventNode;
                    for (var i = 0; i < startEventNode.PortCount - 1; i++) startEventNode.AddChildPort(false);

                    // var nodePorts = graphObject.CodeGraphData.Edges.Where(x => x.SourceNodeGUID == serializedNode.GUID).ToList();
                    // nodePorts.ForEach(_ => startEventNode.AddChildPort());
                }

                if (serializedNode.NodeType == "UpdateEventNode") {
                    var updateEventNode = tempNode as UpdateEventNode;
                    graphView.UpdateEventNode = updateEventNode;
                    for (var i = 0; i < updateEventNode.PortCount - 1; i++) updateEventNode.AddChildPort(false);

                    // var nodePorts = graphObject.CodeGraphData.Edges.Where(x => x.SourceNodeGUID == serializedNode.GUID).ToList();
                    // nodePorts.ForEach(_ => updateEventNode.AddChildPort());
                }

                graphView.AddElement(tempNode);
            }
        }

        private void ConnectNodes() {
            foreach (var connection in graphObject.CodeGraphData.Edges) {
                var sourceNode = Nodes.First(x => x.GUID == connection.SourceNodeGUID);
                var targetNode = Nodes.First(x => x.GUID == connection.TargetNodeGUID);

                // var sourceOutputContainerLength = sourceNode.outputContainer.childCount;
                // var targetInputContainerLength = targetNode.inputContainer.childCount;
                LinkNodesTogether(sourceNode.outputContainer[connection.SourceNodeIndex].Q<Port>(),
                    targetNode.inputContainer[connection.TargetNodeIndex].Q<Port>());
            }
        }

        private void LinkNodesTogether(Port outputSocket, Port inputSocket) {
            var tempEdge = new Edge {
                output = outputSocket,
                input = inputSocket
            };
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            graphView.Add(tempEdge);
        }
    }
}