using System.Collections.Generic;

namespace Nodes {
    public class IntegerValueNode : ValueNode {
        public IntegerValueNode() {
            NodeValueType = typeof(int);
            Outputs = new List<OutputNode> {new IntegerOutputNode(this)};
        }
        public override string GetCode(int callStackLevel) {
            return $"{NodeValue}";
        }

        public override void SetValue(object value) {
            if (value.GetType() == NodeValueType) NodeValue = (int)value;
        }
    }
}