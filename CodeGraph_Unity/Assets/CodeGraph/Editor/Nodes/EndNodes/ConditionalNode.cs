using System;
using System.Linq;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "Conditional")]
    public class ConditionalNode : AbstractEndNode {
        public ConditionalNode() {
            Initialize("Conditional", DefaultNodePosition);
            var branchInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            branchInputPort.portName = "branch";
            branchInputPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(branchInputPort, GetCode);
            var conditionInputNode = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            conditionInputNode.portName = "condition";
            AddInputPort(conditionInputNode, () => {
                var connections = conditionInputNode.connections.ToList();
                if (connections.Count == 0) return $"true /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"true /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var trueOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            trueOutputPort.portName = "true";
            trueOutputPort.portColor = new Color(1,1,1,0.2f);
            var falseOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            falseOutputPort.portName = "false";
            falseOutputPort.portColor = new Color(1,1,1,0.2f);
            AddOutputPort(trueOutputPort, () => "");
            AddOutputPort(falseOutputPort, () => "");
            
            // titleButtonContainer.Add(new Button(()=>Debug.Log(GetCode())){text = "Get Code"});
            Refresh();
        }
        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine($"if({InputPorts[1].RequestCode()}) {{{GetDebugData}");
            if (OutputPorts[0].PortReference.connections.Count() != 0) {
                var trueBranchNode = OutputPorts[0].PortReference.connections.ToList()[0].input.node;
                var trueAbstractEndNode = trueBranchNode as AbstractEndNode;
                var trueEventExtenderNode = trueBranchNode as EventExtenderNode;
                if (trueAbstractEndNode != null) code.AppendLine(trueAbstractEndNode.GetCode());
                else if (trueEventExtenderNode != null) code.AppendLine(trueEventExtenderNode.GetCode());
            }
            code.Append("}");
            if (OutputPorts[1].PortReference.connections.Count() != 0) {
                code.AppendLine(" else {");
                    var falseBranchNode = OutputPorts[1].PortReference.connections.ToList()[0].input.node;
                    var falseAbstractEndNode = falseBranchNode as AbstractEndNode;
                    var falseEventExtenderNode = falseBranchNode as EventExtenderNode;
                    if (falseAbstractEndNode != null) code.AppendLine(falseAbstractEndNode.GetCode());
                    else if (falseEventExtenderNode != null) code.AppendLine(falseEventExtenderNode.GetCode());
                code.AppendLine("}");
            }

            return code.ToString();
        }
        
        private new void AddOutputPort(Port portReference, Func<string> getCode, bool alsoAddToHierarchy = true) {
            var outputPort = new OutputPort(this, portReference, getCode); 
            OutputPorts.Add(outputPort);
            OutputPortDictionary.Add(portReference, outputPort);
            if(alsoAddToHierarchy) outputContainer.Add(portReference);
        }
    }
}