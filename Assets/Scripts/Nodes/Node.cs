using System;
using System.Collections.Generic;

public abstract class Node {
    public List<InputNode> Inputs;
    public List<OutputNode> Outputs;
    public abstract string GetCode(int callStackLevel);
    public bool SetNodeInput(int inputIndex, OutputNode outputNode, bool alsoSetOutput = false) {
        if(inputIndex >= Inputs.Count) throw new IndexOutOfRangeException();
        if (!Inputs[inputIndex].CanAcceptNode(outputNode)) return false;
        Inputs[inputIndex].OutputLocationReference = outputNode;
        if(alsoSetOutput) outputNode.InputLocationReference = Inputs[inputIndex];
        return true;
    }

    public bool SetNodeOutput(int outputIndex, InputNode inputNode, bool alsoSetInput = false) {
        if (outputIndex > Outputs.Count) throw new IndexOutOfRangeException();
        if (!Outputs[outputIndex].CanAcceptNode(inputNode)) return false;
        Outputs[outputIndex].InputLocationReference = inputNode;
        if(alsoSetInput) inputNode.OutputLocationReference = Outputs[outputIndex];
        return true;
    }
}