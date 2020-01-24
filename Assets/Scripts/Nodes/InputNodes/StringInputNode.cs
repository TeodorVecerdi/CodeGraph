namespace Nodes {
    public class StringInputNode : InputNode{
        public StringInputNode(Node parent) {
            InputType = typeof(string);
            ParentNodeReference = parent;
        }
        public override bool CanAcceptNode(OutputNode outputNode) {
            return outputNode.OutputType == InputType;
        }
    }
}