using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using Nodes;
using UnityEngine;

public class AddInputsTest : MonoBehaviour {
    public GameObject InputPrefab;
    public GameObject InputsGroup;
    public List<NodeInputMB> Inputs;

    private void OnGUI() {
        if (GUI.Button(new Rect(0, 0, 300, 60), "Run Code")) {
            TestCode();
        }
    }

    public void TestCode() {
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
        var textValueNode = new TextValueNode();
        var classNode = new ClassNode();

        stringValueNode1.SetValue("Start method called");
        stringValueNode2.SetValue("Update method called");
        printNode1.AddConnection(0, stringValueNode1.Outputs[0]);
        printNode2.AddConnection(0, stringValueNode1.Outputs[0]);
        // printNode1.SetNodeInput(0, stringValueNode1.Outputs[0], true);
        // printNode2.SetNodeInput(0, stringValueNode2.Outputs[0], true);
        
        endNode1.AddConnection(0, printNode1.Outputs[0]);
        endNode2.AddConnection(0, printNode2.Outputs[0]);
        // endNode1.SetNodeInput(0, printNode1.Outputs[0], true);
        // endNode2.SetNodeInput(0, printNode2.Outputs[0], true);

        startEventNode.AddChildNode(endNode1);
        startEventNode.AddChildNode(printNode1);
        startEventNode.AddChildNode(stringValueNode1);
        updateEventNode.AddChildNode(endNode2);
        updateEventNode.AddChildNode(printNode2);
        // updateEventNode.AddChildNode(stringValueNode2);

        textValueNode.SetValue("ExampleClass");
        classNode.AddConnection(0, textValueNode.Outputs[0]);
        // classNode.SetNodeInput(0, textValueNode.Outputs[0], true);
        classNode.AddChildNode(startEventNode);
        classNode.AddChildNode(updateEventNode);
        
        printNode2.RemoveConnection(0, stringValueNode1.Outputs[0]);
        printNode2.AddConnection(0, stringValueNode2.Outputs[0]);
        updateEventNode.AddChildNode(stringValueNode2);
        
        var code = classNode.GetCode(0);
        print(code);

        var provider = CodeDomProvider.CreateProvider("CSharp");
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a => !a.IsDynamic)
            .Select(a => a.Location);   
        var parameters = new CompilerParameters {GenerateExecutable = false};
        parameters.ReferencedAssemblies.AddRange(assemblies.ToArray());
        var results = provider.CompileAssemblyFromSource(parameters, code);
        if (results.Errors.Count > 0)
            foreach (CompilerError compErr in results.Errors) {
                Debug.LogError($"Error {compErr.ErrorNumber} => \"{compErr.ErrorText} at line number {compErr.Line}\"\r\n");
            }
    }
}