using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "Get Property")]
    public class GetPropertyNode : AbstractStartNode {
        private string PropertyName = "varName";

        public GetPropertyNode() {
            Initialize("Get Property", DefaultNodePosition);
            var propertyNameField = new TextField {label = "Name: ", value = "varName"};
            propertyNameField.labelElement.style.minWidth = 0;
            propertyNameField.RegisterValueChangedCallback(evt => PropertyName = evt.newValue);
            inputContainer.Add(propertyNameField);

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "value";
            AddOutputPort(valuePort, () => $"{PropertyName}");
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PropertyName"] = PropertyName;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PropertyName = root.Value<string>("PropertyName");
            inputContainer.Q<TextField>().SetValueWithoutNotify(PropertyName);
        }
    }
}