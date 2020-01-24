using System;

public abstract class OutputNode {
    public Type OutputType = typeof(object);
    public InputNode InputLocationReference = null;
    public Node ParentNodeReference = null;
    public abstract bool CanAcceptNode(InputNode inputNode);
}