    public class BooleanOutputNode : OutputNode {
        public BooleanOutputNode(Node parent) {
            OutputType = typeof(bool);
            ParentNodeReference = parent;
        }

        public override bool CanAcceptNode(InputNode inputNode) {
            return inputNode.InputType == OutputType;
        }
    }