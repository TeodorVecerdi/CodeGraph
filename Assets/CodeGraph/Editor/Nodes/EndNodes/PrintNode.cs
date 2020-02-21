using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class PrintNode : AbstractEndNode {
        public PrintNode() {
            Initialize("Print", DefaultNodePosition);

            var printInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            printInputPort.portName = "value";
            AddInputPort(printInputPort, () => {
                var connections = printInputPort.connections.ToList();
                if (connections.Count == 0) return $"\"\" /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"\"\" /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "event";
            eventInputPort.portColor = new Color(1f, 0.27f, 0f);
            AddInputPort(eventInputPort, GetCode);
            
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())){text="Get Code"});
            Refresh();
        }
        
        public override string GetCode() {
            return $"Debug.Log({InputPorts[0].RequestCode()});{GetDebugData}";
        }

        public override void SetNodeData(string jsonData) {
            
        }
        public override string GetNodeData() {
            return "";
        }
    }
}