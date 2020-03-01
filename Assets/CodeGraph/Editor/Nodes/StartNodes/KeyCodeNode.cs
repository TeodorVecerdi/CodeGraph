using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Input", "KeyCode")]
    public class KeyCodeNode : AbstractStartNode {
        private string KeyCode;

        public KeyCodeNode() {
            Initialize("KeyCode", DefaultNodePosition);

            var foldout = new Foldout();
            foldout.text = "KeyCode (click to select)";
            var baseComponents = Enum.GetNames(typeof(KeyCode)).ToList();
            var components = new List<string>();
            components.AddRange(baseComponents);
            var listView = new ListView(components, 20, () => new Label(), (visualElement, index) => {
                var element = (Label) visualElement;
                element.text = components[index];
            });
            listView.onSelectionChanged += selection => {
                var text = (string) selection[0];
                KeyCode = text;
                foldout.text = $"KeyCode ({text})";
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

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "keyCode";
            AddOutputPort(valuePort, () => $"KeyCode.{KeyCode}");
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["KeyCode"] = KeyCode;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            KeyCode = root.Value<string>("KeyCode");
            inputContainer.Q<Foldout>().text = $"KeyCode ({(string.IsNullOrEmpty(KeyCode) ? "none" : KeyCode)})";;
        }
    }
}