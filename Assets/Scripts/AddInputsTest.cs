using System.Collections.Generic;
using System.Reflection;
using Nodes;
using UnityEngine;

public class AddInputsTest : MonoBehaviour {
    public GameObject InputPrefab;
    public GameObject InputsGroup;
    public List<NodeInputMB> Inputs;

    private void Start() {
        /*Inputs = new List<NodeInputMB>();
        var method = typeof(Vector3).GetMethod("RotateTowards");
        var parameters = method.GetParameters();
        foreach (ParameterInfo parameter in parameters) {
            NodeInputMB nodeInputMb = Instantiate(InputPrefab, InputsGroup.transform).GetComponent<NodeInputMB>();
            nodeInputMb.SetLabel($"{parameter.Name} ({parameter.ParameterType.Name})");
            Inputs.Add(nodeInputMb);
        }*/
        var stringValueNode1 = new StringValueNode();
        var stringValueNode2 = new StringValueNode();
        var printNode1 = new DebugLogNode();
        var printNode2 = new DebugLogNode();
        var endNode1 = new TEMP_SemicolonNode();
        var endNode2 = new TEMP_SemicolonNode();
        var startEventNode = new StartEventNode();
        var updateEventNode = new UpdateEventNode();
        stringValueNode1.SetValue("Start method called");
        stringValueNode2.SetValue("Update method called");
        printNode1.SetNodeInput(0, stringValueNode1.Outputs[0], true);
        printNode2.SetNodeInput(0, stringValueNode2.Outputs[0], true);
        endNode1.SetNodeInput(0, printNode1.Outputs[0], true);
        endNode2.SetNodeInput(0, printNode2.Outputs[0], true);
        startEventNode.AddChildNode(endNode1);
        startEventNode.AddChildNode(printNode1);
        startEventNode.AddChildNode(stringValueNode1);
        updateEventNode.AddChildNode(endNode2);
        updateEventNode.AddChildNode(printNode2);
        updateEventNode.AddChildNode(stringValueNode2);
        print($"{startEventNode.GetCode(0)}\n\n{updateEventNode.GetCode(0)}");
    }
}