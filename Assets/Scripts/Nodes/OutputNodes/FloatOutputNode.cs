using UnityEngine;

public class FloatOutputNode : OutputNode {
    public FloatOutputNode(Node parent) {
        OutputType = typeof(float);
        ParentNodeReference = parent;
    }
    public override bool CanAcceptNode(InputNode inputNode) {
        if (inputNode.InputType == OutputType) return true;
        if (inputNode.InputType == typeof(sbyte)
            || inputNode.InputType == typeof(byte)
            || inputNode.InputType == typeof(short)
            || inputNode.InputType == typeof(ushort)
            || inputNode.InputType == typeof(int)
            || inputNode.InputType == typeof(uint)
            || inputNode.InputType == typeof(long)
            || inputNode.InputType == typeof(ulong)
            || inputNode.InputType == typeof(double)
            || inputNode.InputType == typeof(decimal)) {
            Debug.LogWarning($"OutputNode of type {inputNode.InputType.Name} will be converted to {typeof(float).Name}");
            return true;
        }
        return false;
    }
}