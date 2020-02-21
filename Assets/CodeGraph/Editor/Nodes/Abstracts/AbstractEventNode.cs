using System;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    public abstract class AbstractEventNode : AbstractNode {
        [Obsolete("Event nodes cannot have Input ports", true)]
        public new void AddInputPort(Port portReference, Func<string> requestCode) {
            throw new NotSupportedException("Event nodes cannot have Input ports");
        }

        public abstract string GetCode();
    }
}