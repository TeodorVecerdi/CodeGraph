using System;
using System.Collections.Generic;

namespace CodeGraph {
    [Serializable]
    public class GraphFile {
        public string GraphName;
        public string GeneratedMonoBehaviourName;
        public List<GraphFileNode> Nodes;
        public List<GraphFileConnection> Connections;

        public GraphFile(string graphName, string generatedMonoBehaviourName, List<GraphFileNode> nodes, List<GraphFileConnection> connections) {
            GraphName = graphName;
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
}