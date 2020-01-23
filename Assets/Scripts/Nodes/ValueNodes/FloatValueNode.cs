using System.Collections.Generic;

namespace Nodes {
    public class FloatValueNode : ValueNode {
        public FloatValueNode() {
            NodeValueType = typeof(float);
            Outputs = new List<OutputNode> {new FloatOutputNode(this)};
        }
        public override string GetCode() {
            return $"{NodeValue}f";
        }

        public override void SetValue(object value) {
            if (value.GetType() == NodeValueType) NodeValue = (float)value;
        }
    }
}