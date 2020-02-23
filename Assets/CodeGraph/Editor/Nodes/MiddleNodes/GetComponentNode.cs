using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;
using PopupWindow = UnityEngine.UIElements.PopupWindow;

namespace CodeGraph.Editor {
    [Title("General", "Get Component")]
    public class GetComponentNode : AbstractMiddleNode {
        private string ComponentType;

        public void SetComponentType(string comp) {
            ComponentType = comp;
            inputContainer.Q<TextField>().SetValueWithoutNotify(ComponentType);
        }
        public GetComponentNode() {
            Initialize("Get Component", DefaultNodePosition);
            
            var foldout = new Foldout();
            foldout.text = "Type (click to select)";
            var baseComponents = (from assembly in AppDomain.CurrentDomain.GetAssemblies() from type in assembly.GetTypes() where type.IsSubclassOf(typeof(Component)) select type.Name).ToList();
            var components = new List<string>();
            components.AddRange(baseComponents);
            var listView = new ListView(components, 20, () => new Label(), (visualElement, index) => {
                var element = (Label) visualElement;
                element.text = components[index];
            });
            listView.onSelectionChanged += selection => {
                var text = (string) selection[0];
                ComponentType = text;
                foldout.text = $"Type ({text})";
                foldout.SetValueWithoutNotify(false);
            };
            listView.style.height = 100;
            var searchBar = new TextField();
            searchBar.RegisterValueChangedCallback(evt => {
                var value = evt.newValue.Trim();
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value)) {
                    components = baseComponents.Where(s => s.ToLower(CultureInfo.InvariantCulture).Contains(value.ToLower())).ToList();
                    listView.itemsSource = components;
                    listView.Refresh();
                } else {
                    components = baseComponents.Where(s => true).ToList();
                    listView.itemsSource = components;
                    listView.Refresh();
                }
            });

            foldout.Q("unity-checkmark").style.width = 0;
            foldout.contentContainer.Add(searchBar);
            foldout.contentContainer.Add(listView);
            foldout.RegisterValueChangedCallback(evt => {
                if (evt.newValue) {
                    searchBar[0].Focus();
                    searchBar.value = "";
                }
            });
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
            inputContainer.Q<Foldout>().text = $"Type ({(string.IsNullOrEmpty(ComponentType) ? "none" : ComponentType)})";;
        }
    }
}