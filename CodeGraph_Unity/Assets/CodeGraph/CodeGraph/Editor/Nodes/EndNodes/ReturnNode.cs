using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "Functions", "Return")]
    public class ReturnNode : AbstractEndNode {

        public ReturnNode() {
            Initialize("Return", DefaultNodePosition);
            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "branch";
            eventInputPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(eventInputPort, GetCode);
            
            var printInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            printInputPort.portName = "value";
            AddInputPort(printInputPort, () => {
                var connections = printInputPort.connections.ToList();
                if (connections.Count == 0) return $" /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $" /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
        }
        public override string GetCode() {
            return $"return {InputPorts[1].RequestCode()});{GetDebugData}";
        }
    }
}