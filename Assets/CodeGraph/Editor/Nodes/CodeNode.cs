using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public abstract class CodeNode : Node {
        public readonly Vector2 DefaultNodeSize = new Vector2(200, 150);
        public string GUID;
    }
}