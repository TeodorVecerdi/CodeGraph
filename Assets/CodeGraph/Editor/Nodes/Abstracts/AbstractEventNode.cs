using System;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    public abstract class AbstractEventNode : AbstractNode {
        [Obsolete("Event nodes cannot have Input ports", true)]
        public new void AddInputPort(Port portReference, Func<string> requestCode) { }

        [Obsolete("Temporary")]
        public abstract string GetCode();
    }
}