using System.Text;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(false, true)]
    [Title("Events", "Start Event")]
    public class StartEventNode : AbstractEventNode {
        public StartEventNode() {
            Initialize("Start", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }
        
        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void Start() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }
}