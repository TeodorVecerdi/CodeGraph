using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Title("Basic", "Instantiate")]
    public class InstantiateNode : AbstractEndNode {
        public InstantiateNode() {
            Initialize("Instantiate", DefaultNodePosition);
            var branchPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            branchPort.portName = "branch";
            branchPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(branchPort, GetCode);
            
            var originalPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            originalPort.portName = "object";
            AddInputPort(originalPort, () => {
                var connections = originalPort.connections.ToList();
                if (connections.Count == 0) return $"null /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"null /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var positionPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            positionPort.portName = "position (3)";
            AddInputPort(positionPort, () => {
                var connections = positionPort.connections.ToList();
                if (connections.Count == 0) return $"Vector3.zero /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"Vector3.zero /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var rotationPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            rotationPort.portName = "rotation (Q)";
            AddInputPort(rotationPort, () => {
                var connections = rotationPort.connections.ToList();
                if (connections.Count == 0) return $"Quaternion.identity /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"Quaternion.identity /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var parentPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            parentPort.portName = "parent (transform)";
            AddInputPort(parentPort, () => {
                var connections = parentPort.connections.ToList();
                if (connections.Count == 0) return $"null /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"null /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "object";
            AddOutputPort(outputPort, () => $"instantiateResult_{GUID.ToSafeGUID()}");
        }
        public override string GetCode() {
            var code = $"var instantiateResult_{GUID.ToSafeGUID()} = Instantiate({InputPorts[1].RequestCode()}";
            if (InputPorts[2].PortReference.connections.Any()) {
                code += $", {InputPorts[2].RequestCode()}, {InputPorts[3].RequestCode()}";
                if (InputPorts[4].PortReference.connections.Any()) {
                    code += $", {InputPorts[4].RequestCode()}";
                }
            }
            code += $");{GetDebugData}";
            return code;
        }
        
        private new void AddOutputPort(Port portReference, Func<string> getCode) {
            var outputPort = new OutputPort(this, portReference, getCode); 
            OutputPorts.Add(outputPort);
            OutputPortDictionary.Add(portReference, outputPort);
            outputContainer.Add(portReference);
        }
    }
}