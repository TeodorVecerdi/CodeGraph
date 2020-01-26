using System;

namespace Nodes {
    public abstract class ValueNode : Node {
        public object NodeValue;
        public Type NodeValueType;
        public abstract void SetValue(object value);
    }
}