    using UnityEngine;

    public class Vector2InputNode : InputNode{
        public Vector2InputNode(Node parent) {
            InputType = typeof(UnityEngine.Vector2);
            ParentNodeReference = parent;
        }
        
        public override bool CanAcceptNode(OutputNode outputNode) {
            return outputNode.OutputType == InputType;
        }
    }