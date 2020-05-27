using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(true, true)]
    [Title("Basic", "Create Variable")]
    public class CreateVariableNode : AbstractEndNode {
        private string VariableName = "varName";
        private string VariableType = "int";

        public CreateVariableNode() {
            Initialize("Create Variable", DefaultNodePosition);
            
            var eventInputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            eventInputPort.portName = "branch";
            eventInputPort.portColor = new Color(1,1,1,0.2f);
            AddInputPort(eventInputPort, GetCode);
            
            var variableType = new TextField {label = "Type:", value = "int"};
            variableType.labelElement.style.minWidth = 0;
            variableType.name = "variableType";
            variableType.RegisterValueChangedCallback(evt => VariableType = evt.newValue);
            inputContainer.Add(variableType);

            var variableName = new TextField {label = "Name:", value = "varName"};
            variableName.labelElement.style.minWidth = 0;
            variableName.name = "variableName";
            variableName.RegisterValueChangedCallback(evt => VariableName = evt.newValue);
            inputContainer.Add(variableName);
            Refresh();
        }
        
        public override string GetCode() {
            return $"{VariableType.ToSafeTypeName()} {VariableName.ToSafeVariableName()};{GetDebugData}";
        }
        
        public override string GetNodeData() {
            var root = new JObject();
            root["VariableName"] = VariableName;
            root["VariableType"] = VariableType;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            VariableName = root.Value<string>("VariableName");
            VariableType = root.Value<string>("VariableType");
            inputContainer.Q<TextField>("variableName").SetValueWithoutNotify(VariableName);
            inputContainer.Q<TextField>("variableType").SetValueWithoutNotify(VariableType);
        }
    }
}