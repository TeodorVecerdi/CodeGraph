using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    public class SplitVector3Node : AbstractMiddleNode {
        public SplitVector3Node() {
            Initialize("Split Vector3", DefaultNodePosition);
            var vector3InputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            vector3InputPort.portName = "(3)";
            AddInputPort(vector3InputPort, () => {
                var connections = vector3InputPort.connections.ToList();
                if (connections.Count == 0) return $"new Vector3(0.0f,0.0f,0.0f) /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"new Vector3(0.0f,0.0f,0.0f) /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            
            var xOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            xOutputPort.portName = "x";
            var yOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            yOutputPort.portName = "y";
            var zOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            zOutputPort.portName = "z";
            AddOutputPort(xOutputPort, () => $"{InputPortDictionary[vector3InputPort].RequestCode()}.x");
            AddOutputPort(yOutputPort, () => $"{InputPortDictionary[vector3InputPort].RequestCode()}.y");
            AddOutputPort(zOutputPort, () => $"{InputPortDictionary[vector3InputPort].RequestCode()}.z");
            Refresh();

        }
    }
}