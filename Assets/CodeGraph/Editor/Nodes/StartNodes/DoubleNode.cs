using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Basic", "Float Input")]
    public class FloatNode : AbstractStartNode {
        private float value;

        public FloatNode() {
            Initialize("Float", DefaultNodePosition);
            var floatField = new FloatField {label = "x:", value = 0};
            floatField.labelElement.style.minWidth = 0;
            floatField.RegisterValueChangedCallback(evt => value = evt.newValue);
            inputContainer.Add(floatField);

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "value";
            AddOutputPort(valuePort, () => $"{value}f");
            Refresh();
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["value"] = value;
            return root.ToString(Formatting.None);
        }

        public override  void SetNodeData(string jsonData) {
            var root = JObject.Parse(jsonData);
            value = root.Value<float>("value");
            inputContainer.Q<FloatField>().SetValueWithoutNotify(value);
        }
    }
}