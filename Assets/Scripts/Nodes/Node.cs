using System;
using System.Collections.Generic;

public abstract class Node {
    public List<InputNode> Inputs;
    public List<OutputNode> Outputs;
    // public abstract OutputNode Execute();
    public abstract string GetCode();
    // public abstract void SetNodeInput(int inputIndex, OutputNode inputValue);
    public bool SetNodeInput(int inputIndex, OutputNode outputNode) {
        if(inputIndex >= Inputs.Count) throw new IndexOutOfRangeException();
        if (!Inputs[inputIndex].CanAcceptNode(outputNode)) return false;
        Inputs[inputIndex].OutputLocationReference = outputNode;
        outputNode.InputLocationReference = Inputs[inputIndex];
        return true;
    }
}