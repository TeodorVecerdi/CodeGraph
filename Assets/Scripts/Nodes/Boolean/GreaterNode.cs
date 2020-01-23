using System.Collections.Generic;

namespace Nodes {
    public class GreaterNode : Node {
        public GreaterNode() {
            Inputs = new List<InputNode> {new AnyInputNode(this), new AnyInputNode(this)};
            Outputs = new List<OutputNode> {new BooleanOutputNode(this)};
        }

        public override string GetCode() {
            return $"{Inputs[0].OutputLocationReference.ParentNodeReference.GetCode()} > {Inputs[1].OutputLocationReference.ParentNodeReference.GetCode()}";
        }
    }
}