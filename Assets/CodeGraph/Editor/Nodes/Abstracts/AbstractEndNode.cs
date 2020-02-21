using System;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    public abstract class AbstractEndNode : AbstractNode {
        [Obsolete("End nodes cannot have Output ports", true)]
        public new void AddOutputPort(Port portReference, Func<string> getCode) {
            throw new NotSupportedException("End nodes cannot have Output ports");
        }

        public abstract string GetCode();
    }
}