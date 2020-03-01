using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Events", "Start Event")]
    public class StartEventNode : AbstractEventNode {
        public StartEventNode() {
            Initialize("Start", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void Start() {");
            var nodes = (from outputPort in OutputPorts
                    select outputPort.PortReference.connections.ToList()
                    into connections
                    where connections.Count != 0
                    select connections[0].input.node)
                .ToList();
            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if(eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }
}