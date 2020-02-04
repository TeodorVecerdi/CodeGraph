using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeGraph {
    [Serializable]
    public class GraphFile {
        [SerializeField] public string GraphName;
        // [SerializeField] public string FilePath;
        [SerializeField] public string AssetGuid;
        [SerializeField] public string GeneratedMonoBehaviourName;
        [SerializeField] public List<GraphFileNode> Nodes;
        [SerializeField] public List<GraphFileConnection> Connections;

        public GraphFile(string graphName, string generatedMonoBehaviourName, List<GraphFileNode> nodes, List<GraphFileConnection> connections, string assetGuid) {
            GraphName = graphName;
            // FilePath = filePath;
            GeneratedMonoBehaviourName = generatedMonoBehaviourName;
            Nodes = nodes;
            Connections = connections;
        }

        public void AddNode(GraphFileNode node) {
            Nodes.Add(node);
        }

        public void AddConnection(GraphFileConnection connection) {
            Connections.Add(connection);
        }
    }
    
    [Serializable]
    public sealed class GraphFileObject : ScriptableObject {
        [SerializeField] public string GraphName;
        [SerializeField] public string AssetGuid;
        [SerializeField] public string GeneratedMonoBehaviourName;
        [SerializeField] public List<GraphFileNode> Nodes;
        [SerializeField] public List<GraphFileConnection> Connections;

        public void Init(GraphFile obj) {
            GraphName = obj.GraphName;
            AssetGuid = obj.AssetGuid;
            GeneratedMonoBehaviourName = obj.GeneratedMonoBehaviourName;
            Nodes = obj.Nodes;
            Connections = obj.Connections;
            name = GraphName;
        }
    }
}