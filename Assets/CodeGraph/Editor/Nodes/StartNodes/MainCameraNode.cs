using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Title("Camera", "Main Camera")]
    public class MainCameraNode : AbstractStartNode {
        public MainCameraNode() {
            Initialize("Main Camera", DefaultNodePosition);
            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "camera";
            AddOutputPort(valuePort, () => $"Camera.main");
            Refresh();
        }
        public override void SetNodeData(string jsonData) {
            // This node does not not require any data
        }
        public override string GetNodeData() {
            return "";
        }
    }
}