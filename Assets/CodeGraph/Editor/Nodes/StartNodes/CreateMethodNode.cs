using System;
using System.Collections.Generic;
using System.Text;
using GitHub.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Functions", "Create Method")]
    public class CreateMethodNode : AbstractEventNode {
        private Dictionary<int, string> parameters;
        private int nextId;
        private string methodName;

        public CreateMethodNode() {
            Initialize("Create Method", DefaultNodePosition);
            this.AddStyleSheet("CreateMethodNode");
            IsBaseEventNode = false;
            parameters = new Dictionary<int, string>();
            nextId = 0;
            methodName = "ExampleMethod";

            var methodNameTextField = new TextField("Method name") {name = "method-name-text-field", value = methodName};
            methodNameTextField.labelElement.style.minWidth = 0;
            methodNameTextField.RegisterValueChangedCallback(evt => methodName = evt.newValue);

            var methodNameInputContainer = new VisualElement {name = "method-name-input"};
            methodNameInputContainer.Add(methodNameTextField);

            var contents = mainContainer.Q("contents");
            var divider = new VisualElement {name = "horizontal-divider"};
            contents.Insert(0, methodNameInputContainer);
            contents.Insert(0, divider);

            outputContainer.Add(new VisualElement {name = "parameter-ports"});
            titleButtonContainer.Add(new Button(() => AddParameter()) {text = "Add Parameter"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Print code"});
            Refresh();
            //BUG=====================================================
            //BUG    EDGE SERIALIZATION BREAKS BECAUSE OF THE
            //BUG    parameter-ports VisualElement. SAY YOU HAVE
            //BUG    4 PARAMS AND 1 EVENT PORT. THEN THE EVENT
            //BUG    PORT WILL BE AT INDEX 1 INSTEAD OF 4 BECAUSE
            //BUG    THE PARAMS GET TREATED AS ONE SINGLE CHILD
            //BUG    (BECAUSE THEY ARE A CHILD OF parameter-ports)
            //BUG=====================================================
        }

        private void AddParameter(int id = -1, string value = "") {
            if (id == -1) id = nextId++;
            if (value == "") value = "param" + id;
            parameters.Add(id, value);

            // Create textfield
            var paramNameField = new TextField("name:") {name = id + "_paramTextField", userData = id,};
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
            outputContainer.Q("parameter-ports").Add(valuePort);
            Refresh();
        }

        private string GetCodeForParam(int paramID) {
            return parameters[paramID];
        }

        private void UpdateParam(int id, string newValue) {
            if (parameters.ContainsKey(id)) {
                parameters[id] = newValue;
            }

            var port = outputContainer.Q<Port>(id + "_paramPort");
            port.portName = newValue;
            Refresh();
        }

        private void RemoveParameter(int id) {
            var textField = inputContainer.Q<TextField>(id + "_paramTextField");
            inputContainer.Remove(textField);
            var port = outputContainer.Q("parameter-ports").Q<Port>(id + "_paramPort");
            outputContainer.Q("parameter-ports").Remove(port);
            parameters.Remove(id);
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            var @params = new JArray();
            root["methodName"] = methodName;
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
            methodName = root.Value<string>("methodName");
            mainContainer.Q<TextField>("method-name-text-field").SetValueWithoutNotify(methodName);

            var @params = root.Value<JArray>("params").ToObject<List<string>>();
            foreach (var param in @params) {
                AddParameter(value: param);
            }
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.Append($"public void {methodName}(");
            if (parameters.Count > 0) code.Append("dynamic ");
            code.Append(parameters.Values.Join(", dynamic "));
            code.AppendLine(") {");
            code.AppendLine(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }
}