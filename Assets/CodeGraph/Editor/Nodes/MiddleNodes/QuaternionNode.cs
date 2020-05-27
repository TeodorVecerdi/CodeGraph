using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Quaternion", "Quaternion")]
    public class QuaternionNode : AbstractMiddleNode {
        public QuaternionNode() {
            Initialize("Quaternion", DefaultNodePosition);
            var xInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            xInputPort.portName = "x";
            var yInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            yInputPort.portName = "y";
            var zInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            zInputPort.portName = "z";
            var wInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            wInputPort.portName = "w";
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
            AddInputPort(wInputPort, () => {
                var connections = wInputPort.connections.ToList();
                if (connections.Count == 0) return $"1.0f /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"1.0f /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var vector3OutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            vector3OutputPort.portName = "Q";
            AddOutputPort(vector3OutputPort, () => $"new Quaternion({InputPorts[0].RequestCode()},{InputPorts[1].RequestCode()},{InputPorts[2].RequestCode()},{InputPorts[3].RequestCode()})");
            Refresh();
        }
        
        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var floatNode1Position = new Rect(nodePosition, DefaultNodeSize);
            floatNode1Position.center += new Vector2(-DefaultNodeSize.x/1.5f, -100);
            var floatNode2Position = new Rect(nodePosition, DefaultNodeSize);
            floatNode2Position.center += new Vector2(-DefaultNodeSize.x/1.5f, -25);
            var floatNode3Position = new Rect(nodePosition, DefaultNodeSize);
            floatNode3Position.center += new Vector2(-DefaultNodeSize.x/1.5f, 50);
            var floatNode4Position = new Rect(nodePosition, DefaultNodeSize);
            floatNode4Position.center += new Vector2(-DefaultNodeSize.x/1.5f, 125);
            CreateAndConnectNode<FloatNode>(floatNode1Position, 0, 0, this);
            CreateAndConnectNode<FloatNode>(floatNode2Position, 0, 1, this);
            CreateAndConnectNode<FloatNode>(floatNode3Position, 0, 2, this);
            CreateAndConnectNode<FloatNode>(floatNode4Position, 0, 3, this);
        }
    }
}