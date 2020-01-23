    public class BooleanInputNode : InputNode {

        public BooleanInputNode(Node parent) {
            InputType = typeof(bool);
            ParentNodeReference = parent;
        }

        public override bool CanAcceptNode(OutputNode outputNode) {
            return outputNode.OutputType == InputType;
        }
    }