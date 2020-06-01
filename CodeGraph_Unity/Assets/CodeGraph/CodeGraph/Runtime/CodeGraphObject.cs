using UnityEngine;

namespace CodeGraph {
    public class CodeGraphObject : ScriptableObject {
        [SerializeField] private CodeGraphData codeGraphData;
        public CodeGraphData CodeGraphData => codeGraphData;

        /*private void OnEnable() {
            hideFlags = HideFlags.HideAndDontSave;
        }*/

        public void Initialize(CodeGraphData codeGraphData) {
            this.codeGraphData = codeGraphData;
        }
    }
}