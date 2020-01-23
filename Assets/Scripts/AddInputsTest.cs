using System.Collections.Generic;
using System.Reflection;
using Nodes;
using UnityEngine;

public class AddInputsTest : MonoBehaviour {
    public GameObject InputPrefab;
    public GameObject InputsGroup;
    public List<NodeInputMB> Inputs;

    void Start() {
        /*Inputs = new List<NodeInputMB>();
        var method = typeof(Vector3).GetMethod("RotateTowards");
        var parameters = method.GetParameters();
        foreach (ParameterInfo parameter in parameters) {
            NodeInputMB nodeInputMb = Instantiate(InputPrefab, InputsGroup.transform).GetComponent<NodeInputMB>();
            nodeInputMb.SetLabel($"{parameter.Name} ({parameter.ParameterType.Name})");
            Inputs.Add(nodeInputMb);
        }*/
        var floatValueNode1 = new FloatValueNode();
        var floatValueNode2 = new FloatValueNode();
        floatValueNode1.SetValue(1.5f);
        floatValueNode2.SetValue(3.7f);
        var equalsNode = new EqualsNode();
        equalsNode.SetNodeInput(0, floatValueNode1.Outputs[0]);
        equalsNode.SetNodeInput(1, floatValueNode2.Outputs[0]);
        var lessNode = new LessNode();
        lessNode.SetNodeInput(0, floatValueNode1.Outputs[0]);
        lessNode.SetNodeInput(1, floatValueNode2.Outputs[0]);
        var vector2Node = new Vector2Node();
        vector2Node.SetNodeInput(0, floatValueNode1.Outputs[0]);
        vector2Node.SetNodeInput(1, floatValueNode2.Outputs[0]);
        var printNode1 = new DebugLogNode();
        var printNode2 = new DebugLogNode();
        printNode1.SetNodeInput(0, equalsNode.Outputs[0]);
        printNode2.SetNodeInput(0, vector2Node.Outputs[0]);
        // print(floatValueNode1.GetCode());   
        // print(floatValueNode2.GetCode());   
        Debug.Log($"Equals Node: {equalsNode.GetCode()}");
        Debug.Log($"Less Node: {lessNode.GetCode()}");
        Debug.Log($"Vector2 Node: {vector2Node.GetCode()}");
        Debug.Log($"PrintNode1 Node: {printNode1.GetCode()}");
        Debug.Log($"PrintNode2 Node: {printNode2.GetCode()}");
    }
}