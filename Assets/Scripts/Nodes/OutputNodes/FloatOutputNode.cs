public class FloatOutputNode : OutputNode {
    public FloatOutputNode(Node parent) {
        OutputType = typeof(float);
        ParentNodeReference = parent;
    }
}