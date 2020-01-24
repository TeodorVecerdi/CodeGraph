using System.Collections.Generic;
using UnityEngine;

namespace Nodes {
    public class IfNode : Node {
        public IfNode() {
            Inputs = new List<InputNode> {new BooleanInputNode(this)};
            Outputs = new List<OutputNode> {new CodeBranchOutputNode(this), new CodeBranchOutputNode(this)};
        }

        public override string GetCode(int callStackLevel) {
            var code = $"if({Inputs[0].OutputLocationReference.ParentNodeReference.GetCode(callStackLevel + 1)})\n" +
                       $"{Outputs[0].InputLocationReferences[0].ParentNodeReference.GetCode(callStackLevel + 1)}";
            if (Outputs[1].InputLocationReferences[0] != null) {
                code += $"else\n" +
                        $"{Outputs[1].InputLocationReferences[0].ParentNodeReference.GetCode(callStackLevel + 1)}";
            }
            return code;
        }
    }
}