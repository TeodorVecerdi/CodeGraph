using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Functions", "Create Method")]
    public class CreateMethodNode : AbstractStartNode {
        private Dictionary<int, string> parameters;
        private int nextId;

        public CreateMethodNode() {
            Initialize("Create Method", DefaultNodePosition);
            parameters = new Dictionary<int, string>();
            nextId = 0;
            titleButtonContainer.Add(new Button(() => AddParameter()) {text = "Add Parameter"});
        }

        private void AddParameter(int id = -1, string value = "") {
            if (id == -1) id = nextId++;

            var paramNameField = new TextField("paramName") {name = id + "_param"};
            paramNameField.SetValueWithoutNotify(value);
            var removeButton = new Button(() => RemoveParameter(id));
            paramNameField.contentContainer.Add(removeButton);
            inputContainer.Add(paramNameField);
        }

        private void RemoveParameter(int id) {
            var textField = inputContainer.Q<TextField>(id + "_param");
        }

        public override string GetNodeData() {
            var root = new JObject();
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
        }
    }
}