using System.Collections.Generic;

namespace Nodes {
    public class TextValueNode : ValueNode {
        public TextValueNode() {
            NodeValueType = typeof(string);
            Outputs = new List<OutputNode>{new StringOutputNode(this)};
        }
        public override string GetCode(int callStackLevel) {
            string nv = (string) NodeValue;
            if (nv.Contains("\\")) nv = nv.Replace("\\", "\\\\");
            if (nv.Contains("\'")) nv = nv.Replace("\'", "\\\'");
            if (nv.Contains("\"")) nv = nv.Replace("\"", "\\\"");
            if (nv.Contains("\0")) nv = nv.Replace("\0", "\\0");
            if (nv.Contains("\a")) nv = nv.Replace("\a", "\\a");
            if (nv.Contains("\b")) nv = nv.Replace("\b", "\\b");
            if (nv.Contains("\f")) nv = nv.Replace("\f", "\\f");
            if (nv.Contains("\n")) nv = nv.Replace("\n", "\\n");
            if (nv.Contains("\r")) nv = nv.Replace("\r", "\\r");
            if (nv.Contains("\t")) nv = nv.Replace("\t", "\\t");
            if (nv.Contains("\v")) nv = nv.Replace("\v", "\\v");
            return $"{nv}";
        }

        public override void SetValue(object value) {
            if (value.GetType() == NodeValueType) NodeValue = (string) value;
        }
    }
}