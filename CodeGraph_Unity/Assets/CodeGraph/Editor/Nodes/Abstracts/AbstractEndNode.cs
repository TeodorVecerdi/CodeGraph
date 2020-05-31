using System;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    public abstract class AbstractEndNode : AbstractNode {
        [Obsolete("End nodes cannot have Output ports", true)]
        protected new void AddOutputPort(Port portReference, Func<string> getCode, bool alsoAddToHierarchy = true) {
            throw new NotSupportedException("End nodes cannot have Output ports");
        }

        public abstract string GetCode();
    }
}