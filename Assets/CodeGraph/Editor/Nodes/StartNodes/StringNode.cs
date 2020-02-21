using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Basic", "String Input")]
    public class StringNode : AbstractStartNode {
        private string value;

        public StringNode() {
            Initialize("String", DefaultNodePosition);
            var inputField = new TextField {label = "x:", value = ""};
            inputField.labelElement.style.minWidth = 0;
            inputField.RegisterValueChangedCallback(evt => value = evt.newValue);
            inputContainer.Add(inputField);
            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "value";
            AddOutputPort(valuePort, () => $"\"{value}\"");
            Refresh();
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["value"] = value;
            return root.ToString(Formatting.None);
        }

        public override  void SetNodeData(string jsonData) {
            var root = JObject.Parse(jsonData);
            value = root.Value<string>("value");
            inputContainer.Q<TextField>().SetValueWithoutNotify(value);
        }
    }
}