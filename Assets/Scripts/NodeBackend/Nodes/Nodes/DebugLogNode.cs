using System;
using System.Collections.Generic;

namespace Nodes {
    public class DebugLogNode : Node {
        public DebugLogNode() {
            Inputs = new List<InputNode> {new AnyInputNode(this)};
            Outputs = new List<OutputNode>{new AnyOutputNode(this)};
        }

        public override string GetCode(int callStackLevel) {
            return $"UnityEngine.Debug.Log({Inputs[0].OutputLocationReference.ParentNodeReference.GetCode(callStackLevel+1)})";
        }
    }
}