using System.Collections.Generic;

namespace Nodes {
    public class LessEqualNode : Node {
        public LessEqualNode() {
            Inputs = new List<InputNode> {new AnyInputNode(this), new AnyInputNode(this)};
            Outputs = new List<OutputNode> {new BooleanOutputNode(this)};
        }
        public override string GetCode(int callStackLevel) {
            return $"{Inputs[0].OutputLocationReference.ParentNodeReference.GetCode(callStackLevel+1)} <= {Inputs[1].OutputLocationReference.ParentNodeReference.GetCode(callStackLevel+1)}";
        }
    }
}