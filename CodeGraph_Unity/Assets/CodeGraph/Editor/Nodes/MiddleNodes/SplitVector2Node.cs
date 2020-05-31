using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Vector2", "Split Vector2")]
    public class SplitVector2Node : AbstractMiddleNode {
        public SplitVector2Node() {
            Initialize("Split Vector2", DefaultNodePosition);
            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "(2)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"new Vector2(0.0f,0.0f) /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"new Vector2(0.0f,0.0f) /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
            
            var xOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            xOutputPort.portName = "x";
            var yOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            yOutputPort.portName = "y";
            AddOutputPort(xOutputPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.x");
            AddOutputPort(yOutputPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.y");
            Refresh();
        }
    }
}