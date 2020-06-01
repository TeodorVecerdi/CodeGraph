using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "Functions", "Create Method")]
    public class CreateMethodNode : AbstractEventNode {
        private int nextId;
        private readonly Dictionary<int, string> parameters;
        private readonly Dictionary<int, string> validatedParameters;
        public string MethodName { get; private set; }
        public string ValidatedMethodName { get; private set; }
        public (List<int> Keys, List<string> Values) Parameters => (parameters.Keys.ToList(), parameters.Values.ToList());

        public Action<int, string, string> OnParameterUpdated;
        public Action<int, string> OnParameterAdded;
        public Action<int> OnParameterRemoved;
        public Action<CreateMethodNode> OnMethodRemoved;

        ~CreateMethodNode() {
            OnMethodRemoved?.Invoke(this);
            CodeGraph.Instance.GraphView.CreateMethodNodes.Remove(this);
        }
        public CreateMethodNode() {
            CodeGraph.Instance.GraphView.CreateMethodNodes.Add(this);
            Initialize("Create Method", DefaultNodePosition);
            this.AddStyleSheet("CreateMethodNode");
            RemoveFromClassList("eventNode");
            
            IsBaseEventNode = false;
            parameters = new Dictionary<int, string>();
            validatedParameters = new Dictionary<int, string>();
            nextId = 0;
            MethodName = "ExampleMethod";

            var methodNameTextField = new TextField("Method name") {name = "method-name-text-field", value = MethodName};
            methodNameTextField.labelElement.style.minWidth = 0;
            methodNameTextField.RegisterValueChangedCallback(evt => MethodName = evt.newValue);

            var methodNameInputContainer = new VisualElement {name = "method-name-input"};
            methodNameInputContainer.Add(methodNameTextField);

            var contents = mainContainer.Q("contents");
            var divider = new VisualElement {name = "horizontal-divider_1"};
            divider.AddToClassList("horizontal-divider");
            
            contents.Insert(0, methodNameInputContainer);
            contents.Insert(0, divider);

            titleButtonContainer.Add(new Button(() => AddParameter()) {text = "Add Parameter"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            // titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Print code"});
            Refresh();
        }

        public override void OnNodeDeserialized() {
            base.OnNodeDeserialized();
            ValidatedMethodName = "Method_" + GUID.ToSafeGUID();
        }

        private void AddParameter(int id = -1, string value = "") {
            if (id == -1) id = nextId++;
            if (value == "") value = "param" + id;
            parameters.Add(id, value);
            validatedParameters.Add(id, SafeCodeExtensions.GenerateSafeName("parameter_"));

            // Create textfield
            var paramNameField = new TextField("name:") {name = id + "_paramTextField", userData = id};
            paramNameField.labelElement.style.minWidth = 0;
            paramNameField.RegisterValueChangedCallback(evt => { UpdateParam(id, evt.newValue); });
            paramNameField.SetValueWithoutNotify(value);
            var removeButton = new Button(() => RemoveParameter(id)) {text = "X"};
            paramNameField.contentContainer.Add(removeButton);
            inputContainer.Add(paramNameField);

            // Create port
            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.name = $"{id}_paramPort";
            valuePort.portName = $"{value}";
            AddOutputPort(valuePort, () => GetCodeForParam(id), false);
            outputContainer.Add(valuePort);
            SortPorts();
            Refresh();
            
            OnParameterAdded?.Invoke(id, value);
        }

        private string GetCodeForParam(int paramID) {
            return validatedParameters[paramID];
        }

        private void UpdateParam(int id, string newValue) {
            if (!parameters.ContainsKey(id)) return;
            var oldValue = parameters[id];
            
            parameters[id] = newValue;
            validatedParameters[id] = SafeCodeExtensions.GenerateSafeName("parameter_");
            var port = outputContainer.Q<Port>(id + "_paramPort");
            port.portName = newValue;
            Refresh();
            
            OnParameterUpdated?.Invoke(id, oldValue, newValue);
        }

        private void RemoveParameter(int id) {
            var textField = inputContainer.Q<TextField>(id + "_paramTextField");
            inputContainer.Remove(textField);
            var port = outputContainer.Q<Port>(id + "_paramPort");
            outputContainer.Remove(port);
            OutputPorts.Remove(OutputPortDictionary[port]);
            parameters.Remove(id);
            validatedParameters.Remove(id);
            SortPorts();
            Refresh();
            
            OnParameterRemoved?.Invoke(id);
        }

        private void SortPorts() {
            var allPorts = outputContainer.Children().Cast<Port>().ToList();
            var eventPorts = allPorts.Where(port => port.name.StartsWith("EventPort")).ToList();
            var paramPorts = allPorts.Where(port => port.name.EndsWith("_paramPort")).ToList();
            outputContainer.Clear();
            paramPorts.ForEach(outputContainer.Add);
            eventPorts.ForEach(outputContainer.Add);
        }

        public override string GetNodeData() {
            var root = new JObject();
            var @params = new JArray();
            root["methodName"] = MethodName;
            root["params"] = @params;
            foreach (var param in parameters) {
                @params.Add(param.Value);
            }

            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            MethodName = root.Value<string>("methodName");
            mainContainer.Q<TextField>("method-name-text-field").SetValueWithoutNotify(MethodName);

            var @params = root.Value<JArray>("params").ToObject<List<string>>();
            foreach (var param in @params) {
                AddParameter(value: param);
            }
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.Append($"public void {ValidatedMethodName}(");
            if (parameters.Count > 0) code.Append("dynamic ");
            code.Append(validatedParameters.Values.Join(", dynamic "));
            code.AppendLine(") {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }
}