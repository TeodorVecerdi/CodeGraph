using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "Compare")]
    public class CompareNode : AbstractMiddleNode {
        public CompareNode() {
            Initialize("Compare", DefaultNodePosition);
            var firstInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            firstInputPort.portName = "first";
            AddInputPort(firstInputPort, () => {
                var connections = firstInputPort.connections.ToList();
                if (connections.Count == 0) return $"((object)null) /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"((object)null) /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var secondInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            secondInputPort.portName = "second";
            AddInputPort(secondInputPort, () => {
                var connections = secondInputPort.connections.ToList();
                if (connections.Count == 0) return $"((object)null) /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"((object)null) /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
            var equalsOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            equalsOutputPort.portName = "==";
            var notEqualsOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            notEqualsOutputPort.portName = "!=";
            var lessOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            lessOutputPort.portName = "<";
            var greaterOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            greaterOutputPort.portName = ">";
            var lessEqualOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            lessEqualOutputPort.portName = "<=";
            var greaterEqualOutputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            greaterEqualOutputPort.portName = ">=";
            AddOutputPort(equalsOutputPort, () => $"{InputPortDictionary[firstInputPort].RequestCode()}.Equals({InputPortDictionary[secondInputPort].RequestCode()})");
            AddOutputPort(notEqualsOutputPort, () => $"!{InputPortDictionary[firstInputPort].RequestCode()}.Equals({InputPortDictionary[secondInputPort].RequestCode()})");
            AddOutputPort(lessOutputPort, () => $"{InputPortDictionary[firstInputPort].RequestCode()} is IComparable first_l_{GUID.ToSafeGUID()} && {InputPortDictionary[secondInputPort].RequestCode()} is IComparable second_l_{GUID.ToSafeGUID()} && first_l_{GUID.ToSafeGUID()}.CompareTo(second_l_{GUID.ToSafeGUID()}) < 0");
            AddOutputPort(greaterOutputPort, () => $"{InputPortDictionary[firstInputPort].RequestCode()} is IComparable first_g_{GUID.ToSafeGUID()} && {InputPortDictionary[secondInputPort].RequestCode()} is IComparable second_g_{GUID.ToSafeGUID()} && first_g_{GUID.ToSafeGUID()}.CompareTo(second_g_{GUID.ToSafeGUID()}) > 0");
            AddOutputPort(lessEqualOutputPort, () => $"{InputPortDictionary[firstInputPort].RequestCode()} is IComparable first_le_{GUID.ToSafeGUID()} && {InputPortDictionary[secondInputPort].RequestCode()} is IComparable second_le_{GUID.ToSafeGUID()} && first_le_{GUID.ToSafeGUID()}.CompareTo(second_le_{GUID.ToSafeGUID()}) <= 0");
            AddOutputPort(greaterEqualOutputPort, () =>$"{InputPortDictionary[firstInputPort].RequestCode()} is IComparable first_ge_{GUID.ToSafeGUID()} && {InputPortDictionary[secondInputPort].RequestCode()} is IComparable second_ge_{GUID.ToSafeGUID()} && first_ge_{GUID.ToSafeGUID()}.CompareTo(second_ge_{GUID.ToSafeGUID()}) >= 0");
            Refresh();
        }
        
        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position1 = new Rect(nodePosition, DefaultNodeSize);
            position1.center += new Vector2(-DefaultNodeSize.x/1.5f, -25f);
            var position2 = new Rect(nodePosition, DefaultNodeSize);
            position2.center += new Vector2(-DefaultNodeSize.x/1.5f, 50f);
            CreateAndConnectNode<BoolNode>(position1, 0, 0, this);
            CreateAndConnectNode<BoolNode>(position2, 0, 1, this);
        }
    }
}