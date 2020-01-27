using System;
using System.Collections.Generic;
using System.Linq;
using Nodes;
using UnityEngine;

public abstract class Node {
    public List<InputNode> Inputs;
    public List<OutputNode> Outputs;
    public List<NodeConnection> Connections = new List<NodeConnection>();
    public abstract string GetCode(int callStackLevel);

    public void AddConnection(int inputIndex, OutputNode outputNode) {
        Debug.Assert(inputIndex >= Inputs.Count, $"Index {inputIndex} out of range {Inputs.Count} on node {GetType().Name}");

        if (!Inputs[inputIndex].CanAcceptNode(outputNode)) {
            Debug.LogError($"Input {Inputs[inputIndex].GetType().Name} cannot accept output node {outputNode.GetType().Name}");
            return;
        }

        var connection = NodeConnection.Create(outputNode, Inputs[inputIndex]);
        Connections.Add(connection);
        Inputs[inputIndex].OutputLocationReference = outputNode;
        if(!outputNode.InputLocationReferences.Contains(Inputs[inputIndex]))
                outputNode.InputLocationReferences.Add(Inputs[inputIndex]);
    }

    public void RemoveConnection(int inputIndex, OutputNode outputNode) {
        Debug.Assert(inputIndex >= Inputs.Count, $"Index {inputIndex} out of range {Inputs.Count} on node {GetType().Name}");

        if (Connections.All(connection => connection.To != Inputs[inputIndex])) {
            Debug.LogError($"Could not find connection from {Inputs[inputIndex].GetType().Name} to {outputNode.GetType().Name}");
            return;
        }

        var conn = Connections.Find(connection => connection.To == Inputs[inputIndex]);
        Inputs[inputIndex].OutputLocationReference = null;
        outputNode.InputLocationReferences.Remove(Inputs[inputIndex]);
    }
    public bool SetNodeInput(int inputIndex, OutputNode outputNode, bool alsoSetOutput = false) {
        Debug.Assert(inputIndex >= Inputs.Count, $"Index {inputIndex} out of range {Inputs.Count} on node {GetType().Name}");
        if (!Inputs[inputIndex].CanAcceptNode(outputNode)) return false;
        Inputs[inputIndex].OutputLocationReference = outputNode;
        if (alsoSetOutput) {
            if(!outputNode.InputLocationReferences.Contains(Inputs[inputIndex]))
                outputNode.InputLocationReferences.Add(Inputs[inputIndex]);
        }
        return true;
    }

    public bool SetNodeOutput(int outputIndex, InputNode inputNode, bool alsoSetInput = false) {
        Debug.Assert(outputIndex > Outputs.Count, $"Index {outputIndex} out of range {Outputs.Count} on node {GetType().Name}");
        if (!Outputs[outputIndex].CanAcceptNode(inputNode)) return false;
        Outputs[outputIndex].InputLocationReferences.Add(inputNode);
        if(alsoSetInput) inputNode.OutputLocationReference = Outputs[outputIndex];
        return true;
    }
}