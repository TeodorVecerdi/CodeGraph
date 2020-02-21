using System;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public class SerializedNode {
        public string GUID;
        public string NodeType;
        public Vector2 Position;
        public string NodeData;
    }
}