using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Vector2", "Create Vector2")]
    public class CreateVector2Node : AbstractStartNode {
        private float x;
        private float y;

        public CreateVector2Node() {
            Initialize("Create Vector2", DefaultNodePosition);
            var inputField = new FloatField {label = "x:", value = 0};
            inputField.labelElement.style.minWidth = 0;
            inputField.name = "xInput";
            inputField.RegisterValueChangedCallback(evt => x = evt.newValue);
            inputContainer.Add(inputField);
            
            var inputField2 = new FloatField {label = "y:", value = 0};
            inputField2.labelElement.style.minWidth = 0;
            inputField2.name = "yInput";
            inputField2.RegisterValueChangedCallback(evt => y = evt.newValue);
            inputContainer.Add(inputField2);

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "value (2)";
            AddOutputPort(valuePort, () => $"new Vector2({x}f,{y}f)");
            Refresh();
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["x"] = x;
            root["y"] = y;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override  void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            x = root.Value<float>("x");
            y = root.Value<float>("y");
            inputContainer.Q<FloatField>("xInput").SetValueWithoutNotify(x);
            inputContainer.Q<FloatField>("yInput").SetValueWithoutNotify(y);
        }
    }
}