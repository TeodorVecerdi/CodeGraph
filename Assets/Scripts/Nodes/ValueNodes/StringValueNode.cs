using System.Collections.Generic;

namespace Nodes {
    public class StringValueNode : ValueNode{
        public StringValueNode() {
            NodeValueType = typeof(string);
            Outputs = new List<OutputNode>{new StringOutputNode(this)};
        }
        public override string GetCode(int callStackLevel) {
            return $"\"{NodeValue}\"";
        }

        public override void SetValue(object value) {
            if (value.GetType() == NodeValueType) NodeValue = (string) value;
        }
    }
}