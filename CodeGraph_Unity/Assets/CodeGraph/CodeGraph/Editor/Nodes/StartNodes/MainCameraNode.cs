using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Camera", "Main Camera")]
    public class MainCameraNode : AbstractStartNode {
        public MainCameraNode() {
            Initialize("Main Camera", DefaultNodePosition);
            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "camera";
            AddOutputPort(valuePort, () => $"Camera.main");
            Refresh();
        }
    }
}