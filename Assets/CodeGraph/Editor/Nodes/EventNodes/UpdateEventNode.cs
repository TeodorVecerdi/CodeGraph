using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class UpdateEventNode : AbstractEventNode {
        public UpdateEventNode() {
            Initialize("UPDATE EVENT", DefaultNodePosition);
            AddChildPort();
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())){text = "Get Code"});
            inputContainer.Add(new Button(AddChildPort){text = "Add New Port"});
            Refresh();
        }

        private void AddChildPort() {
            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = $"Child {OutputPorts.Count+1}";
            outputPort.portColor = new Color(0.53f, 0f, 0.67f);
            AddOutputPort(outputPort, () => "");
        }

        public override string GetCode() {
            var code = "private void Update() {\n";
            foreach (var outputPort in OutputPorts) {
                var connections = outputPort.PortReference.connections.ToList();
                if (connections.Count == 0) continue;
                
                var node = connections[0].input.node as AbstractEndNode;
                if (node != null) {
                    code += node.GetCode() + "\n";
                }
            }
            code += "}";
            return code;
        }
    }
}