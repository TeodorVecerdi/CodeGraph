using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Experimental", "Custom Code")]
    public class CustomCodeNode : AbstractEndNode {
        public string CustomCode = "";
        public CustomCodeNode() {
            Initialize("Custom Code", DefaultNodePosition);
            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "branch";
            eventInputPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(eventInputPort, GetCode);
            
            var propertyType = new TextField(int.MaxValue, true, false, ' ') {label = "Code"};
            propertyType.labelElement.style.minWidth = 0;
            propertyType.name = "customCode";
            propertyType.RegisterValueChangedCallback(evt => CustomCode = evt.newValue);
            inputContainer.Add(propertyType);
            
            // Experimental warning
            var container = new VisualElement();
            container.AddToClassList("experimental-warning");
            var text = new TextElement {text = "This node is experimental and\ncan lead to problems. Use this\nif you know what you're doing."};
            container.contentContainer.Add(text);
            extensionContainer.Add(container);
            Refresh();
        }
        
        public override string GetCode() {
            return $"{CustomCode}{GetDebugData}";
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