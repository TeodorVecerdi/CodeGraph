using System;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public struct GraphFileConnection {
        [SerializeField] public Guid FromNode;
        [SerializeField] public Guid ToNode;

        public GraphFileConnection(Guid fromNode, Guid node) {
            FromNode = fromNode;
            ToNode = node;
        }
    }
}