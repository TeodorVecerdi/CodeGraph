using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGraph {
    [Serializable] public class CodeGraphData {
        [SerializeField] public string AssetPath;
        [SerializeField] public string LastEditedAt = "0";
        [SerializeField] public List<SerializedNode> Nodes = new List<SerializedNode>();
        [SerializeField] public List<SerializedEdge> Edges = new List<SerializedEdge>();
    }
}