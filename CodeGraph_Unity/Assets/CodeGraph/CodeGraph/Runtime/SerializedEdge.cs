using System;

namespace CodeGraph {
    [Serializable]
    public class SerializedEdge {
        public string SourceNodeGUID;
        public int SourceNodeIndex;
        public string TargetNodeGUID;
        public int TargetNodeIndex;
    }
}