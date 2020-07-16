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
            graphObject.CodeGraphData.Groups = new List<GroupData>(CodeGraph.Instance.GraphObject.CodeGraphData.Groups);
            graphObject.CodeGraphData.Groups.ForEach(group => { group.Title = group.GroupReference.title; });
            foreach (var groupItem in CodeGraph.Instance.GraphObject.CodeGraphData.GroupItems) {
                graphObject.CodeGraphData.GroupItems.Add(groupItem.Key, CodeGraph.Instance.GraphObject.CodeGraphData.GroupItems[groupItem.Key]);
            }
            graphObject.CodeGraphData.GraphName = CodeGraph.Instance.GraphObject.CodeGraphData.GraphName;
            graphObject.CodeGraphData.SchemaVersion = CodeGraph.Instance.GraphObject.CodeGraphData.SchemaVersion;
            graphObject.CodeGraphData.IsMonoBehaviour = CodeGraph.Instance.GraphObject.CodeGraphData.IsMonoBehaviour;

            File.WriteAllText(fileName, JsonUtility.ToJson(graphObject.CodeGraphData, true));
            if (shouldRefreshAssets) AssetDatabase.ImportAsset(fileName);
            return graphObject;
        }

        public void LoadGraph(CodeGraphObject graphObject) {
            this.graphObject = graphObject;
            ClearGraph();
            CreateGroups();
            GenerateNodes();
            ClearEmptyGroups();
            ConnectNodes();
            PostInitNodes();
        }

        private void ClearGraph() {
            foreach (var group in graphView.GroupDictionary.Keys) {
                graphView.RemoveElement(group);
            }
            graphView.GroupDictionary.Clear();
            graphView.GroupGuidDictionary.Clear();
            graphView.CreateMethodNodes.Clear();
            foreach (var node in Nodes) {
                Edges.Where(x => x.input.node == node).ToList().ForEach(edge => graphView.RemoveElement(edge));
                graphView.RemoveElement(node);
            }
        }

        private void CreateGroups() {
            graphObject.CodeGraphData.Groups.ForEach(groupData => {
                var group = new Group {title = groupData.Title};
                graphObject.CodeGraphData.AddGroup(groupData);
                groupData.GroupReference = group;
                
                graphView.GroupDictionary.Add(group, groupData);
                graphView.GroupGuidDictionary.Add(groupData.Guid, groupData);
                graphView.AddElement(group);
            });
        }

        private void GenerateNodes() {
            var deserializedNodes = SerializationHelper.DeserializeNodes(graphObject.CodeGraphData.Nodes);

            // deserializedNodes.ForEach(node => Debug.Log($"SaveUtility::GenerateNodes node pos: {node.worldBound.position}"));
            deserializedNodes.ForEach(node => {
                graphView.AddElement(node);
                if (node.GroupGuid != Guid.Empty && graphView.GroupGuidDictionary.ContainsKey(node.GroupGuid)) {
                    var groupData = graphView.GroupGuidDictionary[node.GroupGuid];
                    var group = groupData.GroupReference;

                    graphObject.CodeGraphData.SetGroup(node, groupData);
                    group.AddElement(node);
                }
            });
        }

        private void ClearEmptyGroups() {
            foreach (var group in graphView.GroupDictionary.ToDictionary(pair => pair.Key, pair => pair.Value)) {
                if (graphObject.CodeGraphData.GroupItems[group.Value.Guid].Count == 0) {
                    graphView.RemoveElement(group.Key);
                    graphObject.CodeGraphData.Groups.Remove(group.Value);
                    graphObject.CodeGraphData.GroupItems.Remove(group.Value.Guid);
                    graphView.GroupGuidDictionary.Remove(group.Value.Guid);
                    graphView.GroupDictionary.Remove(group.Key);
                }
            }
        }

        private void ConnectNodes() {
            SerializationHelper.DeserializeAndLinkEdges(graphObject.CodeGraphData.Edges, Nodes).ForEach(graphView.Add);
        }

        private void PostInitNodes() {
            graphView.nodes.ForEach(node => ((AbstractNode) node).Refresh());
        }
    }
}