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
    }
}