using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Properties", "Create Property")]
    public class CreatePropertyNode : AbstractStartNode {
        private string PropertyName = "varName";
        private string PropertyType = "int";

        public CreatePropertyNode() {
            Initialize("Create Property", DefaultNodePosition);

            var propertyType = new TextField {label = "Type:", value = "int"};
            propertyType.labelElement.style.minWidth = 0;
            propertyType.name = "propertyType";
            propertyType.RegisterValueChangedCallback(evt => PropertyType = evt.newValue);
            inputContainer.Add(propertyType);

            var propertyName = new TextField {label = "Name:", value = "varName"};
            propertyName.labelElement.style.minWidth = 0;
            propertyName.name = "propertyName";
            propertyName.RegisterValueChangedCallback(evt => PropertyName = evt.newValue);
            inputContainer.Add(propertyName);
            Refresh();
        }

        public string GetCode() {
            return $"public {PropertyType.ToSafeTypeName()} {PropertyName.ToSafeVariableName()};{GetDebugData}";
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PropertyName"] = PropertyName;
            root["PropertyType"] = PropertyType;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PropertyName = root.Value<string>("PropertyName");
            PropertyType = root.Value<string>("PropertyType");
            inputContainer.Q<TextField>("propertyName").SetValueWithoutNotify(PropertyName);
            inputContainer.Q<TextField>("propertyType").SetValueWithoutNotify(PropertyType);
        }
    }
}