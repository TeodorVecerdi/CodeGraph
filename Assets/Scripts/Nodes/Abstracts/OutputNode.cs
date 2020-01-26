using System;
using System.Collections.Generic;

public abstract class OutputNode {
    public Type OutputType = typeof(object);
    public List<InputNode> InputLocationReferences = new List<InputNode>();
    public Node ParentNodeReference = null;
    public abstract bool CanAcceptNode(InputNode inputNode);
}