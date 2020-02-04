namespace Nodes {
    public class CodeBranchOutputNode : OutputNode {
        public CodeBranchOutputNode(Node parent) {
            OutputType = typeof(CodeBranchNode);
            ParentNodeReference = parent;
        }
        public override bool CanAcceptNode(InputNode inputNode) {
            return inputNode.InputType == OutputType;
        }
    }
}