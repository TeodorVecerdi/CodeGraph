using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Title("Quaternion", "Quaternion Euler Angles")]
    public class QuaternionGetEulerAnglesNode : AbstractMiddleNode {
        public QuaternionGetEulerAnglesNode() {
            Initialize("Quaternion Euler Angles", DefaultNodePosition);
            var xInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            xInputPort.portName = "Q";
            AddInputPort(xInputPort, () => {
                var connections = xInputPort.connections.ToList();
                if (connections.Count == 0) return $"Quaternion.identity /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"Quaternion.identity /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var vector3OutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            vector3OutputPort.portName = "(3)";
            AddOutputPort(vector3OutputPort, () => $"{InputPorts[0].RequestCode()}.eulerAngles");
            Refresh();
        }
        
        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x, 0f);
            CreateAndConnectNode<QuaternionIdentityNode>(position, 0, 0, this);
        }
    }
}