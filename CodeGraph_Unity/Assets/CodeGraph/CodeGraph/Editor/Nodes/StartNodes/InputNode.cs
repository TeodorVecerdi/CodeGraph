using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Input", "Input")]
    public class InputNode : AbstractStartNode {
        public InputNode() {
            Initialize("Input", DefaultNodePosition);
            var anyKeyPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var anyKeyDownPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float)); 
            var inputStringPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var mousePositionPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var mouseScrollDeltaPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            anyKeyPort.portName = "anyKey";
            anyKeyDownPort.portName = "anyKeyDown"; 
            inputStringPort.portName = "inputString";
            mousePositionPort.portName = "mousePosition";
            mouseScrollDeltaPort.portName = "mouseScrollDelta";
            AddOutputPort(anyKeyPort, () => "Input.anyKey");
            AddOutputPort(anyKeyDownPort, () => "Input.anyKeyDown");
            AddOutputPort(inputStringPort, () => "Input.inputString");
            AddOutputPort(mousePositionPort, () => "Input.mousePosition");
            AddOutputPort(mouseScrollDeltaPort, () => "Input.mouseScrollDelta");
            Refresh();
        }
    }
}