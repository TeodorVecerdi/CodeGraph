using System;

namespace CodeGraph.Editor {
    [AttributeUsage( AttributeTargets.Class)]
    public class NodeAttribute : Attribute {
        public bool AllowOnClassGraph;
        public bool AllowOnMonoBehaviourGraph;

        public NodeAttribute(bool allowOnClassGraph = true, bool allowOnMonoBehaviourGraph = true) {
            AllowOnClassGraph = allowOnClassGraph;
            AllowOnMonoBehaviourGraph = allowOnMonoBehaviourGraph;
        }
    }
}