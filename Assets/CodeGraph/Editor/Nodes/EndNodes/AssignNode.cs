using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Basic", "Assign")]
    public class AssignNode : AbstractEndNode{
        public AssignNode() {
            Initialize("Assign", DefaultNodePosition);
            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "branch";
            eventInputPort.portColor = Color.white;
            AddInputPort(eventInputPort, GetCode);
            
            var lhsPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            lhsPort.portName = "lhs";
            AddInputPort(lhsPort, () => {
                var connections = lhsPort.connections.ToList();
                if (connections.Count == 0) return $"var _{Guid.NewGuid().ToString()} /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"var _{Guid.NewGuid().ToString()} /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            
            var rhsPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            rhsPort.portName = "rhs";
            AddInputPort(rhsPort, () => {
                var connections = rhsPort.connections.ToList();
                if (connections.Count == 0) return $"null /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"null /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });

            
            
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())){text="Get Code"});
            Refresh();
        }
        
        public override string GetCode() {
            return $"{InputPorts[1].RequestCode()}={InputPorts[2].RequestCode()};{GetDebugData}";
        }

        public override void SetNodeData(string jsonData) {
            // This node does not not require any data
        }
        public override string GetNodeData() {
            return "";
        }
    }
}