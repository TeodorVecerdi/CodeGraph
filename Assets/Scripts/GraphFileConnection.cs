using System;

namespace CodeGraph {
    [Serializable]
    public class GraphFileConnection {
        public Guid FromNode;
        public Guid ToNode;
        public GraphFileConnection(Guid fromNode, Guid node) {
            FromNode = fromNode;
            ToNode = node;
        }
    }
}