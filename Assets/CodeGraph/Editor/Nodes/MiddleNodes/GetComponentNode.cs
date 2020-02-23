using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("General", "Get Component")]
    public class GetComponentNode : AbstractMiddleNode {
        private string ComponentType;

        public GetComponentNode() {
            Initialize("Get Component", DefaultNodePosition);

            var componentTypeField = new TextField {label = "Type: ", value = "Transform"};
            componentTypeField.labelElement.style.minWidth = 0;
            componentTypeField.RegisterValueChangedCallback(evt => ComponentType = evt.newValue);
            inputContainer.Add(componentTypeField);

            var foldout = new Foldout();
            foldout.text = "Type (click to select)";
            List<string> components = new List<string> {"Component1", "Component2", "Component3", "Component4","Component1", "Component2", "Component3", "Component4"};
            var listView = new ListView(components, 20, () => new Label(), (visualElement, index) => {
                var element = (Label) visualElement;
                element.text = components[index];
            });
            listView.onSelectionChanged += selection => {
                var text = (string) selection[0];
                ComponentType = text;
                componentTypeField.SetValueWithoutNotify(text);
                foldout.text = $"Type ({text})";
                foldout.SetValueWithoutNotify(false);
            };
            listView.style.height = 100;
            foldout.contentContainer.Add(listView);
            foldout.SetValueWithoutNotify(false);
            inputContainer.Add(foldout);

            var inputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            inputPort.portName = "gameobject";
            AddInputPort(inputPort, () => {
                var connections = inputPort.connections.ToList();
                if (connections.Count == 0) return $"new GameObject() /* WARNING: You probably want connect this node to something. Node GUID: {GUID} */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"new GameObject() /* ERROR: Something went wrong and the connected node ended up as null. Node GUID: {GUID} */";
                return node.OutputPortDictionary[output].GetCode();
            });

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "component";
            AddOutputPort(valuePort, () => $"{InputPortDictionary[inputPort].RequestCode()}.GetComponent<{ComponentType}>()");
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