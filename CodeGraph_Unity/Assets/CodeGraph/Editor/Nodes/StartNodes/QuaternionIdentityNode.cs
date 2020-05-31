using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Quaternion", "Identity Quaternion")]
    public class QuaternionIdentityNode : AbstractStartNode {
        public QuaternionIdentityNode() {
            Initialize("Identity Quaternion", DefaultNodePosition);
            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "identity";
            AddOutputPort(valuePort, () => $"Quaternion.identity");
            Refresh();
        }
    }
}