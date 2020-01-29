using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using Nodes;
using UnityEngine;

public class AddInputsTest : MonoBehaviour {
    private void OnGUI() {
        if (GUI.Button(new Rect(0, 0, 300, 60), "Run Code")) {
            TestCode();
        }
    }

    public void TestCode() {
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
        endNode1.AddConnection(0, printNode1.Outputs[0]);
        endNode2.AddConnection(0, printNode2.Outputs[0]);
        startEventNode.AddChildNode(endNode1);
        startEventNode.AddChildNode(printNode1);
        startEventNode.AddChildNode(stringValueNode1);
        updateEventNode.AddChildNode(endNode2);
        updateEventNode.AddChildNode(printNode2);

        textValueNode.SetValue("ExampleClass");
        classNode.AddConnection(0, textValueNode.Outputs[0]);
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