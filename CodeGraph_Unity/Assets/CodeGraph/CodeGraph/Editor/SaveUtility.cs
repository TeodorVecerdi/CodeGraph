using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
            var connectedEdges = Edges.Where(x => x.input.node != null).ToList();
            graphObject.CodeGraphData.Edges.AddRange(SerializationHelper.SerializeEdges(connectedEdges));
            graphObject.CodeGraphData.Nodes.AddRange(SerializationHelper.SerializeNodes(Nodes));
            
            graphObject.CodeGraphData.LastEditedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            graphObject.CodeGraphData.GraphName = CodeGraph.Instance.GraphObject.CodeGraphData.GraphName;
            graphObject.CodeGraphData.SchemaVersion = CodeGraph.Instance.GraphObject.CodeGraphData.SchemaVersion;
            graphObject.CodeGraphData.IsMonoBehaviour = CodeGraph.Instance.GraphObject.CodeGraphData.IsMonoBehaviour;
            
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
            graphView.CreateMethodNodes.Clear();
            foreach (var node in Nodes) {
                Edges.Where(x => x.input.node == node).ToList().ForEach(edge => graphView.RemoveElement(edge));
                graphView.RemoveElement(node);
            }
        }

        private void GenerateNodes() {
            var deserializedNodes = SerializationHelper.DeserializeNodes(graphObject.CodeGraphData.Nodes);
            // deserializedNodes.ForEach(node => Debug.Log($"SaveUtility::GenerateNodes node pos: {node.worldBound.position}"));
            deserializedNodes.ForEach(graphView.AddElement);
        }

        private void ConnectNodes() {
             SerializationHelper.DeserializeAndLinkEdges(graphObject.CodeGraphData.Edges, Nodes).ForEach(graphView.Add);
        }
        
        private void PostInitNodes() {
            graphView.nodes.ForEach(node => ((AbstractNode) node).Refresh());
        }
    }
}