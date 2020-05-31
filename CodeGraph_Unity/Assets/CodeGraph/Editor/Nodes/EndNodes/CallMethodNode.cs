using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GitHub.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Functions", "Call Method")]
    public class CallMethodNode : AbstractEndNode {
        private Dictionary<int, string> parameters;
        private string methodName;
        public string MethodName {
            get => methodName;
            set {
                SetupListeners(methodName, value);
                methodName = value;
                RedoParameters();
            }
        }
        private List<string> baseMethodNames => CodeGraph.Instance.GraphView.CreateMethodNodes.Select(node => node.MethodName).ToList();
        public CallMethodNode() {
            Initialize("Call Method", DefaultNodePosition);
            parameters = new Dictionary<int, string>();
            
            var foldout = new Foldout();
            foldout.text = "Method (click to select)";
            var methodNames = new List<string>();
            methodNames.AddRange(baseMethodNames);
            var listView = new ListView(methodNames, 20, () => new Label(), (visualElement, index) => {
                var element = (Label) visualElement;
                element.text = methodNames[index];
            });
            listView.onSelectionChanged += selection => {
                var text = (string) selection[0];
                MethodName = text;
                foldout.text = $"Method ({text})";
                foldout.SetValueWithoutNotify(false);
            };
            listView.style.height = 100;
            var searchBar = new TextField();
            searchBar.RegisterValueChangedCallback(evt => {
                var value = evt.newValue.Trim();
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value)) {
                    methodNames = baseMethodNames.Where(s => s.ToLower(CultureInfo.InvariantCulture).Contains(value.ToLower())).ToList();
                    listView.itemsSource = methodNames;
                    listView.Refresh();
                } else {
                    methodNames = baseMethodNames.ToList();
                    listView.itemsSource = methodNames;
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
            outputContainer.Add(foldout);
            
            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "branch";
            eventInputPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(eventInputPort, GetCode);
            
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Print code"});
        }

        private void SetupListeners(string oldMethodName, string newMethodName) {
            // Remove listeners from old method
            if (!string.IsNullOrEmpty(oldMethodName)) {
                var oldMethod = CodeGraph.Instance.GraphView.CreateMethodNodes.Find(node => node.MethodName == oldMethodName);
                oldMethod.OnParameterAdded -= OnParameterAdded;
                oldMethod.OnParameterRemoved -= OnParameterRemoved;
                oldMethod.OnParameterUpdated -= OnParameterUpdated;
            }

            // Add listeners to new method
            if (!string.IsNullOrEmpty(newMethodName)) {
                var newMethod = CodeGraph.Instance.GraphView.CreateMethodNodes.Find(node => node.MethodName == newMethodName);
                newMethod.OnParameterAdded += OnParameterAdded;
                newMethod.OnParameterRemoved += OnParameterRemoved;
                newMethod.OnParameterUpdated += OnParameterUpdated;
            }

        }

        private void RedoParameters() {
            // Clear params
            var allPorts = inputContainer.Children().OfType<Port>().ToList();
            foreach (var port in allPorts.Where(port => port.name.EndsWith("_paramPort"))) {
                var connectionsToRemove = new List<Edge>();
                connectionsToRemove.AddRange(port.connections);
                while(connectionsToRemove.Count > 0) {
                    var conn = connectionsToRemove[0];
                    CodeGraph.Instance.GraphView.RemoveElement(conn);
                    conn.input.Disconnect(conn);
                    conn.output.Disconnect(conn);
                    connectionsToRemove.RemoveAt(0);
                }

                InputPorts.Remove(InputPortDictionary[port]);
                inputContainer.Remove(port);
            }
            parameters.Clear();
            // Add params
            var method = CodeGraph.Instance.GraphView.CreateMethodNodes.Find(node => node.MethodName == MethodName);
            var @params = method.Parameters;
            var count = @params.Keys.Count;
            for (var i = 0; i < count; i++) {
                AddParameter(@params.Keys[i], @params.Values[i]);
            }
            Refresh();
        }

        private void AddParameter(int id, string name) {
            parameters.Add(id, name);
            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            valuePort.name = $"{id}_paramPort";
            valuePort.portName = $"{name}";
            AddInputPort(valuePort, () => {
                var connections = valuePort.connections.ToList();
                if (connections.Count == 0) return $"default(dynamic) /* WARNING: You probably want connect this node to something. */";
                var output = connections[0].output;
                var node = output.node as AbstractNode;
                if (node == null) return $"default(dynamic) /* ERROR: Something went wrong and the connected node ended up as null. */";
                return node.OutputPortDictionary[output].GetCode();
            });
        }

        private void OnParameterAdded(int id, string parameterName) {
            AddParameter(id, parameterName);
            Refresh();
        }

        private void OnParameterUpdated(int id, string oldName, string newName) {
            parameters[id] = newName;
            var port = inputContainer.Q<Port>(id + "_paramPort");
            port.portName = newName;
            Refresh();
        }

        private void OnParameterRemoved(int id) {
            var port = inputContainer.Q<Port>(id + "_paramPort");
            // Remove connections
            var connectionsToRemove = new List<Edge>();
            connectionsToRemove.AddRange(port.connections);
            while(connectionsToRemove.Count > 0) {
                var conn = connectionsToRemove[0];
                CodeGraph.Instance.GraphView.RemoveElement(conn);
                conn.input.Disconnect(conn);
                conn.output.Disconnect(conn);
                connectionsToRemove.RemoveAt(0);
            }
            
            inputContainer.Remove(port);
            parameters.Remove(id);
            Refresh();
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["MethodName"] = MethodName;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            MethodName = root.Value<string>("MethodName");
            outputContainer.Q<Foldout>().text = $"Method ({(string.IsNullOrEmpty(MethodName) ? "none" : MethodName)})";;
        }
        
        public override string GetCode() {
            var code = new StringBuilder();
            code.Append(MethodName + "(");
            var ports = InputPorts.Where(port => port.PortReference.name.EndsWith("_paramPort")).ToList();
            var paramCode = ports.Select(port => port.RequestCode()).ToList().Join(", ");
            code.Append(paramCode);
            code.AppendLine($");{GetDebugData}");
            return code.ToString();
        }
    }
}