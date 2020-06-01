using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "Assign")]
    public class AssignNode : AbstractEndNode{
        public AssignNode() {
            Initialize("Assign", DefaultNodePosition);
            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "branch";
            eventInputPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(eventInputPort, GetCode);
            
            var lhsPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            lhsPort.portName = "lhs";
            AddInputPort(lhsPort, () => {
                var connections = lhsPort.connections.ToList();
                if (connections.Count == 0) return $"var _{Guid.NewGuid().ToString()} /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"var _{Guid.NewGuid().ToString()} /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
            
            var rhsPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            rhsPort.portName = "rhs";
            AddInputPort(rhsPort, () => {
                var connections = rhsPort.connections.ToList();
                if (connections.Count == 0) return $"null /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"null /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            
            
            // titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())){text="Get Code"});
            Refresh();
        }
        
        public override string GetCode() {
            return $"{InputPorts[1].RequestCode()}={InputPorts[2].RequestCode()};{GetDebugData}";
        }
    }
}