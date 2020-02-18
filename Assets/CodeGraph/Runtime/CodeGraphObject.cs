using UnityEngine;

namespace CodeGraph {
    public class CodeGraphObject : ScriptableObject {
        [SerializeField] private CodeGraphData codeGraphData;
        public CodeGraphData CodeGraphData => codeGraphData;

        public void Initialize(CodeGraphData codeGraphData) {
            this.codeGraphData = codeGraphData;
        }
    }
}