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

        public CodeGraphObject Save(string fileName, bool shouldRefreshAssets = true) {
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
            if(shouldRefreshAssets) AssetDatabase.ImportAsset(fileName);
            return graphObject;
        }

        public void LoadGraph(CodeGraphObject graphObject) {
            this.graphObject = graphObject;
            ClearGraph();
            GenerateNodes();
            ConnectNodes();
            PostInitNodes();
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
                var tempNode = (AbstractNode) Activator.CreateInstance(type);
                tempNode.GUID = serializedNode.GUID;
                tempNode.SetNodeData(serializedNode.NodeData);
                tempNode.SetPosition(new Rect(serializedNode.Position, AbstractNode.DefaultNodeSize));
                
                if (tempNode is AbstractEventNode eventNode) {
                    for (var i = 0; i < eventNode.PortCount; i++) eventNode.AddChildPort(false);
                }
                graphView.AddElement(tempNode);
            }
        }

        private void ConnectNodes() {
            foreach (var connection in graphObject.CodeGraphData.Edges) {
                var sourceNode = Nodes.First(x => x.GUID == connection.SourceNodeGUID);
                var targetNode = Nodes.First(x => x.GUID == connection.TargetNodeGUID);
                LinkNodesTogether(sourceNode.outputContainer[connection.SourceNodeIndex].Q<Port>(),
                    targetNode.inputContainer[connection.TargetNodeIndex].Q<Port>());
            }
        }
        
        private void PostInitNodes() {
            graphView.nodes.ForEach(node => ((AbstractNode) node).Refresh());
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