namespace Nodes {
    public class AnyOutputNode : OutputNode {
        public AnyOutputNode(Node parent) {
            ParentNodeReference = parent;
        }

        public override bool CanAcceptNode(InputNode inputNode) {
            return true;
        }
    }
}