using System;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    public abstract class AbstractStartNode : AbstractNode {
        [Obsolete("Start nodes cannot have Input ports", true)]
        public new void AddInputPort(Port portReference, Func<string> requestCode) {
            throw new NotSupportedException("Start nodes cannot have Input ports");
        }
    }
}