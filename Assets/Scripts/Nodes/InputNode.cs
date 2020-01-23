using System;

public abstract class InputNode {
    public Type InputType = typeof(object);
    public OutputNode OutputLocationReference = null;
    public Node ParentNodeReference = null;
    public abstract bool CanAcceptNode(OutputNode outputNode);
}