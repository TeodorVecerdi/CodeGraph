using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Transform", "Split Transform")]
    public class SplitTransformNode : AbstractMiddleNode {
        public SplitTransformNode() {
            Initialize("Split Transform", DefaultNodePosition);
            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "transform";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"new Transform() /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"new Transform() /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });
            
            var positionPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            positionPort.portName = "position";
            var localPositionPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            localPositionPort.portName = "localPosition";
            var eulerAnglesPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            eulerAnglesPort.portName = "eulerAngles";
            var localEulerAnglesPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            localEulerAnglesPort.portName = "localEulerAngles";
            var rotationPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            rotationPort.portName = "rotation";
            var localRotationPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            localRotationPort.portName = "localRotation";
            var localScalePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            localScalePort.portName = "localScale";
            var lossyScalePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            lossyScalePort.portName = "lossyScale";
            var parentPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            parentPort.portName = "parent";
            
            AddOutputPort(positionPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.position");
            AddOutputPort(localPositionPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.localPosition");
            AddOutputPort(eulerAnglesPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.eulerAngles");
            AddOutputPort(localEulerAnglesPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.localEulerAngles");
            AddOutputPort(rotationPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.rotation");
            AddOutputPort(localRotationPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.localRotation");
            AddOutputPort(localScalePort, () => $"{InputPortDictionary[inputPort].RequestCode()}.localScale");
            AddOutputPort(lossyScalePort, () => $"{InputPortDictionary[inputPort].RequestCode()}.lossyScale");
            AddOutputPort(parentPort, () => $"{InputPortDictionary[inputPort].RequestCode()}.parent");
            Refresh();
        }
    }
}