using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(false, true)]
    [Title("Events", "Awake Event")]
    public class AwakeEventNode : AbstractEventNode {
        public AwakeEventNode() {
            Initialize("Awake", DefaultNodePosition);
            // titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void Awake() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }
}