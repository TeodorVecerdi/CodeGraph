using System.Collections.Generic;
using System.Linq;

namespace Nodes {
    public class ClassNode : Node {
        public List<Node> ChildrenNodes;
        public ClassNode() {
            Inputs = new List<InputNode>{new StringInputNode(this)};
            Outputs = new List<OutputNode>();
            ChildrenNodes = new List<Node>();
        }
        public override string GetCode(int callStackLevel) {
            var code =  $"public class {Inputs[0].OutputLocationReference.ParentNodeReference.GetCode(callStackLevel+1)} : UnityEngine.MonoBehaviour{{\n";
            ChildrenNodes.ForEach(node => {
                if (node.Outputs.Count == 0 || node.Outputs.All(nodeOutput => nodeOutput.InputLocationReferences.Count == 0))
                    code += node.GetCode(callStackLevel + 1) + "\n";
            });
            code += "}";
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