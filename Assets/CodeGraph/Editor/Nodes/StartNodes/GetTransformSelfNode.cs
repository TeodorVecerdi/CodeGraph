using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Title("General", "Get Transform (Self)")]
    public class GetTransformSelfNode : AbstractStartNode {
        public GetTransformSelfNode() {
            Initialize("Get Transform", DefaultNodePosition);
            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "transform";
            AddOutputPort(valuePort, () => $"transform");
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