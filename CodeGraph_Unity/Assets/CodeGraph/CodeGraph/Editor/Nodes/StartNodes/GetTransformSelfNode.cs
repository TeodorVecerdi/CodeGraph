using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Node(false, true)]
    [Title("Mono", "Transform", "Get Transform (Self)")]
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