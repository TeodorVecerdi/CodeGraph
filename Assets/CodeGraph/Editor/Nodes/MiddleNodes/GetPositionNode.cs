using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Title("Transform", "Get Position")]
    public class GetPositionNode : AbstractMiddleNode{
        public GetPositionNode() {
            Initialize("Get Position", DefaultNodePosition);
            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "transformable";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"new Transform() /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"new Transform() /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            
            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "(3)";
            AddOutputPort(outputPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.position");
            Refresh();
        }
        
        public override void SetNodeData(string jsonData) {
            // This node does not not require any data
        }
        public override string GetNodeData() {
            return "";
        }
    }
}