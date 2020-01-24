namespace Nodes {
    public class StringOutputNode : OutputNode{
        public StringOutputNode(Node parent) {
            OutputType = typeof(string);
            ParentNodeReference = parent;
        }

        public override bool CanAcceptNode(InputNode inputNode) {
            return inputNode.InputType == OutputType;
        }
    }
}