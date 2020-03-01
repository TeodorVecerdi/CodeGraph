using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Basic", "Get Variable")]
    public class GetVariableNode : AbstractStartNode {
        private string VariableName = "varName";
        public GetVariableNode() {
            Initialize("Get Variable", DefaultNodePosition);
            var variableNameField = new TextField {label = "Name: ", value = "varName"};
            variableNameField.labelElement.style.minWidth = 0;
            variableNameField.RegisterValueChangedCallback(evt => VariableName = evt.newValue);
            inputContainer.Add(variableNameField);

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "value";
            AddOutputPort(valuePort, () => $"{VariableName}");
            Refresh();
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["VariableName"] = VariableName;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            VariableName = root.Value<string>("VariableName");
            inputContainer.Q<TextField>().SetValueWithoutNotify(VariableName);
        }
    }
}