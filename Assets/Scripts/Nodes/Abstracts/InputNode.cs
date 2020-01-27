using System;

public abstract class InputNode {
    public Type InputType = typeof(object);
    public OutputNode OutputLocationReference;
    public Node ParentNodeReference;
    public abstract bool CanAcceptNode(OutputNode outputNode);
}