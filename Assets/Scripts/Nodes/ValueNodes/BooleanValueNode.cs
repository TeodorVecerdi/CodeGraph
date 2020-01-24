
using System.Collections.Generic;

namespace Nodes {
    public class BooleanValueNode : ValueNode{
        public BooleanValueNode() {
            NodeValueType = typeof(bool);
            Outputs = new List<OutputNode> {new BooleanOutputNode(this)};
        }
        public override string GetCode(int callStackLevel) {
            return (bool)NodeValue ? "true" : "false";
        }

        public override void SetValue(object value) {
            if (value.GetType() == NodeValueType) NodeValue = (bool)value;
        }
    }
}