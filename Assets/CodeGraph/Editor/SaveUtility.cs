using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    public class SaveUtility {
        private List<Edge> Edges => graphView.edges.ToList();
        private List<CodeNode> Nodes => graphView.nodes.ToList().Cast<CodeNode>().ToList();
        private CodeGraphObject graphObject;
        private CodeGraphView graphView;

        public static SaveUtility GetInstance(CodeGraphView graphView) => new SaveUtility {graphView = graphView};

        public void Save(string fileName) {
            graphObject = ScriptableObject.CreateInstance<CodeGraphObject>();
            graphObject.Initialize(new CodeGraphData {AssetPath = fileName});
            var connectedEdges = Edges.Where(x => x.input.node != null).ToArray();

            foreach (var edge in connectedEdges) {
                var inputNode = edge.input.node as CodeNode;
                var outputNode = edge.output.node as CodeNode;
                graphObject.CodeGraphData.Edges.Add(new SerializedEdge {
                    SourceNodeGUID = inputNode?.GUID,
                    TargetNodeGUID = outputNode?.GUID
                });
            }

            foreach (var node in Nodes) {
                graphObject.CodeGraphData.Nodes.Add(new SerializedNode {
                    GUID = node.GUID,
                    NodeType = node.GetType().Name,
                    Position = node.GetPosition().position
                });
            }

            graphObject.CodeGraphData.LastEditedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            File.WriteAllText(fileName, JsonUtility.ToJson(graphObject.CodeGraphData, true));
        }
    }
}