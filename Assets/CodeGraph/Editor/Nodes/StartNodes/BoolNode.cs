using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Basic", "Boolean Input")]
    public class BoolNode : AbstractStartNode {
        private bool value;

        public BoolNode() {
            Initialize("Boolean", DefaultNodePosition);
            var inputField = new Toggle {label = "x:", value = false};
            inputField.labelElement.style.minWidth = 0;
            inputField.RegisterValueChangedCallback(evt => value = evt.newValue);
            inputContainer.Add(inputField);

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "value";
            AddOutputPort(valuePort, () => $"{value.ToString().ToLower()}");
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["value"] = value;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            value = root.Value<bool>("value");
            inputContainer.Q<Toggle>().SetValueWithoutNotify(value);
        }
    }
}