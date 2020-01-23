    public class BooleanOutputNode : OutputNode {
        public BooleanOutputNode(Node parent) {
            OutputType = typeof(bool);
            ParentNodeReference = parent;
        }
    }