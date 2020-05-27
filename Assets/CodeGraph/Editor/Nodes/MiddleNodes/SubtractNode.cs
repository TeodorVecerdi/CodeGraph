using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "Math", "Subtract")]
    public class SubtractNode : AbstractMiddleNode {
        public SubtractNode() {
            Initialize("Subtract", DefaultNodePosition);
            var firstInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            firstInputPort.portName = "first";
            AddInputPort(firstInputPort, () => {
                var connections = firstInputPort.connections.ToList();
                if (connections.Count == 0) return $"((object)0)/* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"((object)0) /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var secondInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            secondInputPort.portName = "second";
            AddInputPort(secondInputPort, () => {
                var connections = secondInputPort.connections.ToList();
                if (connections.Count == 0) return $"((object)null)/* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"((object)null) /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var equalsOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            equalsOutputPort.portName = "result";
            AddOutputPort(equalsOutputPort, () => $"{InputPortDictionary[firstInputPort].RequestCode()}-{InputPortDictionary[secondInputPort].RequestCode()}");
            Refresh();
        }
    }
}