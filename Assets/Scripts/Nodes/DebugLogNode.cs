using System;
using System.Collections.Generic;

namespace Nodes {
    public class DebugLogNode : Node {
        public DebugLogNode() {
            Inputs = new List<InputNode> {new AnyInputNode(this)};
            Outputs = new List<OutputNode>();
        }

        public override string GetCode() {
            return $"UnityEngine.Debug.Log({Inputs[0].OutputLocationReference.ParentNodeReference.GetCode()})";
        }
    }
}