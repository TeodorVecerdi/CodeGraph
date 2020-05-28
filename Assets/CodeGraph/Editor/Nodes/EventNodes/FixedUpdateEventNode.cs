using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(false, true)]
    [Title("Events", "Fixed Update Event")]
    public class FixedUpdateEventNode : AbstractEventNode {
        public FixedUpdateEventNode() {
            Initialize("Fixed Update", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void FixedUpdate() {");
            code.AppendLine(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }
}