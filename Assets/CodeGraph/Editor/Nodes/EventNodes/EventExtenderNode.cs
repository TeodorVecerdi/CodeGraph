using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Events", "Event Extender")]
    public class EventExtenderNode : AbstractEventNode {
        public string SourceTitle = "none";
        public EventExtenderNode() {
            IsBaseEventNode = false;
            Initialize("EXTENDER (none)", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(() => CleanPorts()) {text = "Clean Ports"});
            var eventPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            eventPort.portName = "branch";
            eventPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(eventPort, () => "");
            Refresh();
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["SourceTitle"] = SourceTitle;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            SourceTitle = root.Value<string>("SourceTitle");
            UpdateSourceTitle(SourceTitle);
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.Append(GetEventCode());
            return code.ToString();
        }
        
        private new void AddInputPort(Port portReference, Func<string> requestCode, bool alsoAddToHierarchy = true) {
            var inputPort = new InputPort(this, portReference, requestCode);
            InputPorts.Add(inputPort);
            InputPortDictionary.Add(portReference, inputPort);
            if(alsoAddToHierarchy) inputContainer.Add(portReference);
        }

        public void UpdateSourceTitle(string otherTitle) {
            base.title = $"EXTENDER ({otherTitle})";
            SourceTitle = otherTitle;
        }
    }
}