using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Input", "GetAxis")]
    public class GetAxisNode : AbstractMiddleNode {
        public GetAxisNode() {
            Initialize("GetAxis", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "axisName (str)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"\"\" /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"\"\" /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "float";
            AddOutputPort(outputPort, () => $"Input.GetAxis({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<StringNode>(position, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetAxisRaw")]
    public class GetAxisRawNode : AbstractMiddleNode {
        public GetAxisRawNode() {
            Initialize("GetAxisRaw", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "axisName (str)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"\"\" /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"\"\" /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "float";
            AddOutputPort(outputPort, () => $"Input.GetAxisRaw({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<StringNode>(position, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetButton")]
    public class GetButtonNode : AbstractMiddleNode {
        public GetButtonNode() {
            Initialize("GetButton", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "buttonName (str)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetButton({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<StringNode>(position, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetButtonDown")]
    public class GetButtonDownNode : AbstractMiddleNode {
        public GetButtonDownNode() {
            Initialize("GetButtonDown", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "buttonName (str)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetButtonDown({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<StringNode>(position, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetButtonUp")]
    public class GetButtonUpNode : AbstractMiddleNode {
        public GetButtonUpNode() {
            Initialize("GetButtonUp", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "buttonName (str)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetButtonUp({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<StringNode>(position, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetMouseButton")]
    public class GetMouseButtonNode : AbstractMiddleNode {
        public GetMouseButtonNode() {
            Initialize("GetMouseButton", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "button (int)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetMouseButton({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<IntNode>(position, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetMouseButtonDown")]
    public class GetMouseButtonDownNode : AbstractMiddleNode {
        public GetMouseButtonDownNode() {
            Initialize("GetMouseButtonDown", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "button (int)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetMouseButtonDown({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<IntNode>(position, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetMouseButtonUp")]
    public class GetMouseButtonUpNode : AbstractMiddleNode {
        public GetMouseButtonUpNode() {
            Initialize("GetMouseButtonUp", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "button (int)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetMouseButtonUp({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var position = new Rect(nodePosition, DefaultNodeSize);
            position.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<IntNode>(position, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetKey")]
    public class GetKeyNode : AbstractMiddleNode {
        public GetKeyNode() {
            Initialize("GetKey", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "key (KeyCode)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetKey({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var keyNodePos = new Rect(nodePosition, DefaultNodeSize);
            keyNodePos.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<KeyCodeNode>(keyNodePos, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetKeyUp")]
    public class GetKeyUpNode : AbstractMiddleNode {
        public GetKeyUpNode() {
            Initialize("GetKeyUp", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "key (KeyCode)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetKeyUp({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var keyNodePos = new Rect(nodePosition, DefaultNodeSize);
            keyNodePos.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<KeyCodeNode>(keyNodePos, 0, 0, this);
        }
    }

    [Node(true, true)]
    [Title("Input", "GetKeyDown")]
    public class GetKeyDownNode : AbstractMiddleNode {
        public GetKeyDownNode() {
            Initialize("GetKeyDown", DefaultNodePosition);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "key (KeyCode)";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"false /* WARNING: You probably want to connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"false /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            outputPort.portName = "bool";
            AddOutputPort(outputPort, () => $"Input.GetKeyDown({InputPorts[0].RequestCode()})");
            Refresh();
        }

        public override void OnCreateFromSearchWindow(Vector2 nodePosition) {
            var keyNodePos = new Rect(nodePosition, DefaultNodeSize);
            keyNodePos.center += new Vector2(-DefaultNodeSize.x / 1.5f, 0);
            CreateAndConnectNode<KeyCodeNode>(keyNodePos, 0, 0, this);
        }
    }
}