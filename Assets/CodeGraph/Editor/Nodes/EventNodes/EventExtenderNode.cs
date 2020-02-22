using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Events", "Event Extender")]
    public class EventExtenderNode : AbstractEventNode {
        public string SourceTitle = "none";
        public EventExtenderNode() {
            capabilities |= Capabilities.Deletable;
            IsBaseEventNode = false;
            Initialize("EXTENDER (none)", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            var eventPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            eventPort.portName = "branch";
            eventPort.portColor = Color.white;
            AddInputPort(eventPort, () => "");
            Refresh();
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root["SourceTitle"] = SourceTitle;
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
            SourceTitle = root.Value<string>("SourceTitle");
            UpdateSourceTitle(SourceTitle);
        }

        public override string GetCode() {
            var code = new StringBuilder();
            (from outputPort in OutputPorts
                    select outputPort.PortReference.connections.ToList()
                    into connections
                    where connections.Count != 0
                    select connections[0].input.node)
                .OfType<AbstractEndNode>().ToList()
                .ForEach(node => code.AppendLine(node.GetCode()));
            return code.ToString();
        }
        
        private new void AddInputPort(Port portReference, Func<string> requestCode) {
            var inputPort = new InputPort(this, portReference, requestCode);
            InputPorts.Add(inputPort);
            InputPortDictionary.Add(portReference, inputPort);
            inputContainer.Add(portReference);
        }

        public void UpdateSourceTitle(string otherTitle) {
            base.title = $"EXTENDER ({otherTitle})";
            SourceTitle = otherTitle;
        }
    }
}