     public class Vector2OutputNode : OutputNode {
        public Vector2OutputNode(Node parent) {
            OutputType = typeof(UnityEngine.Vector2);
            ParentNodeReference = parent;
        }

        public override bool CanAcceptNode(InputNode inputNode) {
            return inputNode.InputType == OutputType;
        }
     }