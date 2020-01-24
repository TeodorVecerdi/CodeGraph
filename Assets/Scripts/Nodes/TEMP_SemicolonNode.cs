using System.Collections.Generic;

namespace Nodes {
    public class TEMP_SemicolonNode : Node {
        public TEMP_SemicolonNode() {
            Inputs = new List<InputNode>{new AnyInputNode(this)};
            Outputs = new List<OutputNode>();
        }
        public override string GetCode(int callStackLevel) {
            return $"{Inputs[0].OutputLocationReference.ParentNodeReference.GetCode(callStackLevel+1)};";
        }
    }
}