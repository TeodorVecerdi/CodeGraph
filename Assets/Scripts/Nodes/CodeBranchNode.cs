using System.Collections.Generic;

namespace Nodes {
    public class CodeBranchNode : Node {
        public List<Node> ChildrenNodes;
        public CodeBranchNode() {
            Inputs = new List<InputNode>{new CodeBranchInputNode(this)};
            Outputs = new List<OutputNode>();
            ChildrenNodes = new List<Node>();
        }
        public override string GetCode(int callStackLevel) {
            var code ="";
            ChildrenNodes.ForEach(node => code += node.Outputs.Count == 0 ? node.GetCode(callStackLevel+1) + "\n" : "");
            if (code.Split('\n').Length > 2) {
                code = "{" + code + "}";
            }
            return code;
        }
        public void AddChildNode(Node node) {
            ChildrenNodes.Add(node);
        }
        public void RemoveChildNode(Node node) {
            ChildrenNodes.Remove(node);
        }
    }
}