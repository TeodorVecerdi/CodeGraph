using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Experimental", "Custom Code Output")]
    public class CustomCodeOutputNode : AbstractStartNode {
        public string CustomCode;

        public CustomCodeOutputNode() {
            Initialize("Custom Code Output", DefaultNodePosition);

            var propertyType = new TextField(int.MaxValue, true, false, ' ') {label = "Code"};
            propertyType.labelElement.style.minWidth = 0;
            propertyType.name = "customCode";
            propertyType.RegisterValueChangedCallback(evt => CustomCode = evt.newValue);
            inputContainer.Add(propertyType);
            
            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "value";
            AddOutputPort(valuePort, () => CustomCode);
            
            // Experimental warning
            var container = new VisualElement();
            container.AddToClassList("experimental-warning");
            var text = new TextElement {text = "This node is experimental and\ncan lead to problems. Use this\nif you know what you're doing."};
            container.contentContainer.Add(text);
            extensionContainer.Add(container);
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["CustomCode"] = CustomCode;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            CustomCode = root.Value<string>("CustomCode");
            inputContainer.Q<TextField>("customCode").SetValueWithoutNotify(CustomCode);
        }
    }
}