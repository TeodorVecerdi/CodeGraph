using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class UpdateEventNode : AbstractEventNode {
        public int PortCount = 0;
        public UpdateEventNode() {
            Initialize("UPDATE EVENT", DefaultNodePosition);
            AddChildPort();
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())){text = "Get Code"});
            inputContainer.Add(new Button(() => AddChildPort()){text = "Add New Port"});
            Refresh();
        }

        public void AddChildPort(bool incrementPortCount = true) {
            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = $"Child {OutputPorts.Count+1}";
            outputPort.portColor = new Color(0.53f, 0f, 0.67f);
            AddOutputPort(outputPort, () => "");
            if(incrementPortCount) PortCount++;
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            return root.ToString();
        }

        public override void SetNodeData(string jsonData) {
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
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