    using System;

    public class AnyInputNode : InputNode {
        public AnyInputNode(Node parent) {
            ParentNodeReference = parent;
        }

        public override bool CanAcceptNode(OutputNode outputNode) {
            return true;
        }
    }