using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class CreateMethodInfo {
        public readonly string Name;
        public readonly string ValidatedName;
        public readonly CreateMethodNode NodeReference;

        public CreateMethodInfo(string name, string validatedName, CreateMethodNode nodeReference) {
            Name = name;
            ValidatedName = validatedName;
            NodeReference = nodeReference;
        }
    }

    [Node(true, true)]
    [Title("Basic", "Functions", "Call Method")]
    public class CallMethodNode : AbstractEndNode {
        private Dictionary<int, string> parameters;
        private CreateMethodInfo method;
        public CreateMethodInfo Method {
            get => method;
            private set {
                SetupListeners(method, value);
                method = value;
                RedoParameters();
            }
        }

        private List<CreateMethodInfo> cachedMethods;
        private readonly Foldout foldout;

        public CallMethodNode() {
            Initialize("Call Method", DefaultNodePosition);
            parameters = new Dictionary<int, string>();

            #region MethodNameFoldout
            foldout = new Foldout();
            foldout.text = "Method (click to select)";
            var methodNames = CacheMethods();
            var listView = new ListView(methodNames, 20, () => new Label(), (visualElement, index) => {
                var element = (Label) visualElement;
                element.text = methodNames[index].Name;
            });
            listView.onSelectionChanged += selection => {
                var obj = (CreateMethodInfo) selection[0];
                Method = obj;
                foldout.text = $"Method ({obj.Name})";
                foldout.SetValueWithoutNotify(false);
            };
            listView.style.height = 100;
            var searchBar = new TextField();
            searchBar.RegisterValueChangedCallback(evt => {
                var value = evt.newValue.Trim();
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value)) {
                    methodNames = cachedMethods.Where(s => s.Name.ToLower(CultureInfo.InvariantCulture).Contains(value.ToLower())).ToList();
                    listView.itemsSource = methodNames;
                    listView.Refresh();
                } else {
                    methodNames = cachedMethods.ToList();
                    listView.itemsSource = methodNames;
                    listView.Refresh();
                }
            });

            foldout.Q("unity-checkmark").style.width = 0;
            foldout.contentContainer.Add(searchBar);
            foldout.contentContainer.Add(listView);
            foldout.RegisterValueChangedCallback(evt => {
                if (evt.newValue) {
                    CacheMethods();
                    methodNames = cachedMethods.ToList();
                    listView.itemsSource = methodNames;
                    listView.Refresh();
                    searchBar[0].Focus();
                    searchBar.value = "";
                }
            });
            foldout.SetValueWithoutNotify(false);
            outputContainer.Add(foldout);
            #endregion

            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "branch";
            eventInputPort.portColor = new Color(1, 1, 1, 0.2f);
            AddInputPort(eventInputPort, GetCode);

            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Print code"});

            Refresh();
        }

        private List<CreateMethodInfo> CacheMethods() {
            cachedMethods = CodeGraph.Instance.GraphView.CreateMethodNodes.Select(node => new CreateMethodInfo(node.MethodName, node.ValidatedMethodName, node)).ToList();
            return cachedMethods;
        }

        private void SetupListeners(CreateMethodInfo oldMethod, CreateMethodInfo newMethod) {
            // Remove listeners from old method
            if (oldMethod?.NodeReference != null) {
                oldMethod.NodeReference.OnParameterAdded -= OnParameterAdded;
                oldMethod.NodeReference.OnParameterRemoved -= OnParameterRemoved;
                oldMethod.NodeReference.OnParameterUpdated -= OnParameterUpdated;
                oldMethod.NodeReference.OnMethodRemoved -= OnMethodRemoved;
            }

            // Add listeners to new method
            if (newMethod?.NodeReference != null) {
                newMethod.NodeReference.OnParameterAdded += OnParameterAdded;
                newMethod.NodeReference.OnParameterRemoved += OnParameterRemoved;
                newMethod.NodeReference.OnParameterUpdated += OnParameterUpdated;
                newMethod.NodeReference.OnMethodRemoved += OnMethodRemoved;
            }
        }

        private void RedoParameters() {
            // Clear params
            var allPorts = inputContainer.Children().OfType<Port>().ToList();
            foreach (var port in allPorts.Where(port => port.name.EndsWith("_paramPort"))) {
                var connectionsToRemove = new List<Edge>();
                connectionsToRemove.AddRange(port.connections);
                while (connectionsToRemove.Count > 0) {
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
            
            // Exit if current method is null. Probably means we just created the node.
            if(method?.NodeReference == null) return;
            
            // Add params
            var @params = method.NodeReference.Parameters;
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

        private void OnMethodRemoved(CreateMethodNode node) {
            // check if the selected method is the removed method
            if (Method != null && Method.NodeReference.GUID == node.GUID) {
                Method = null;
                foldout.text = "Method (click to select)";
            }
        }

        private void OnParameterRemoved(int id) {
            var port = inputContainer.Q<Port>(id + "_paramPort");

            // Remove connections
            var connectionsToRemove = new List<Edge>();
            connectionsToRemove.AddRange(port.connections);
            while (connectionsToRemove.Count > 0) {
                var conn = connectionsToRemove[0];
                CodeGraph.Instance.GraphView.RemoveElement(conn);
                conn.input.Disconnect(conn);
                conn.output.Disconnect(conn);
                connectionsToRemove.RemoveAt(0);
            }

            inputContainer.Remove(port);
            InputPorts.Remove(InputPortDictionary[port]);
            parameters.Remove(id);
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["MethodName"] = Method.ValidatedName;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            var methodName = root.Value<string>("MethodName");
            var tmpMth = CodeGraph.Instance.GraphView.CreateMethodNodes.Find(node => node.ValidatedMethodName == methodName);
            Method = tmpMth == null ? null : new CreateMethodInfo(tmpMth.MethodName, tmpMth.ValidatedMethodName, tmpMth);
            outputContainer.Q<Foldout>().text = $"Method ({(Method == null ? "click to select" : Method.Name)})";
        }

        public override string GetCode() {
            if (Method == null) return "";
            
            var code = new StringBuilder();
            code.Append(Method.ValidatedName + "(");
            var ports = InputPorts.Where(port => port.PortReference.name.EndsWith("_paramPort")).ToList();
            var paramCode = ports.Select(port => port.RequestCode()).ToList().Join(", ");
            code.Append(paramCode);
            code.AppendLine($");{GetDebugData}");
            return code.ToString();
        }
    }
}