using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "While")]
    public class WhileNode : AbstractEndNode {
        public WhileNode() {
            Initialize("While", DefaultNodePosition);
            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "branch";
            eventInputPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(eventInputPort, GetCode);
            var countPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            countPort.portName = "condition";
            AddInputPort(countPort, () => {
                var connections = countPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var loopPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            loopPort.portName = "loop block";
            loopPort.portColor = new Color(1,1,1,0.2f);
            AddOutputPort(loopPort, () => "");
        }

        public override string GetCode() {
            var code = $"while({InputPorts[1].RequestCode()}) {{\n";
            var connections = OutputPorts[0].PortReference.connections.ToList();
            if (connections.Count != 0) {
                var input = connections[0].input;
                if (input.node is AbstractEventNode node) {
                    code += node.GetCode();
                } else if (input.node is AbstractEndNode node2) {
                    code += node2.GetCode();
                }
            }
            code += "\n}";
            return code;
        }
        
        private new void AddOutputPort(Port portReference, Func<string> getCode, bool alsoAddToHierarchy = true) {
            var outputPort = new OutputPort(this, portReference, getCode); 
            OutputPorts.Add(outputPort);
            OutputPortDictionary.Add(portReference, outputPort);
            if(alsoAddToHierarchy) outputContainer.Add(portReference);
        }
    }
}