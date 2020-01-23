public class IntegerOutputNode : OutputNode {
    public IntegerOutputNode(Node parent) {
        OutputType = typeof(int);
        ParentNodeReference = parent;
    }

}