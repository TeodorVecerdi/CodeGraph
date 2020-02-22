using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Title("Vector3", "Create Vector3")]
    public class Vector3Node : AbstractMiddleNode {
        public Vector3Node() {
            Initialize("Vector3", DefaultNodePosition);
            var xInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            xInputPort.portName = "x";
            var yInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            yInputPort.portName = "y";
            var zInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            zInputPort.portName = "z";
            AddInputPort(xInputPort, () => {
                var connections = xInputPort.connections.ToList();
                if (connections.Count == 0) return $"0.0f /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"0.0f /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            AddInputPort(yInputPort, () => {
                var connections = yInputPort.connections.ToList();
                if (connections.Count == 0) return $"0.0f /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"0.0f /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            AddInputPort(zInputPort, () => {
                var connections = zInputPort.connections.ToList();
                if (connections.Count == 0) return $"0.0f /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"0.0f /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var vector3OutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            vector3OutputPort.portName = "(3)";
            AddOutputPort(vector3OutputPort, () => $"new Vector3({InputPorts[0].RequestCode()},{InputPorts[1].RequestCode()},{InputPorts[2].RequestCode()})");
            Refresh();
        }
    }
}