using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("General", "Get Component (Self)")]
    public class GetComponentSelfNode : AbstractStartNode {
        private string ComponentType;

        public GetComponentSelfNode() {
            Initialize("Get Component", DefaultNodePosition);

            var componentTypeField = new TextField {label = "Type: ", value = "Transform"};
            componentTypeField.labelElement.style.minWidth = 0;
            componentTypeField.RegisterValueChangedCallback(evt => ComponentType = evt.newValue);
            inputContainer.Add(componentTypeField);

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "component";
            AddOutputPort(valuePort, () => $"GetComponent<{ComponentType}>()");
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["ComponentType"] = ComponentType;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            ComponentType = root.Value<string>("ComponentType");
            inputContainer.Q<TextField>().SetValueWithoutNotify(ComponentType);
        }
    }
}