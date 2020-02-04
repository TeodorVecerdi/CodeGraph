namespace Nodes {
    public class CodeBranchInputNode : InputNode {
        public CodeBranchInputNode(Node parent) {
            InputType = typeof(CodeBranchNode);
            ParentNodeReference = parent;
        }
        public override bool CanAcceptNode(OutputNode outputNode) {
            return true;
        }
    }
}