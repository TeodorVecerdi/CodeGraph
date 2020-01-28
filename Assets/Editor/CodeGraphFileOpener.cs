using UnityEditor;
using UnityEngine;

namespace CodeGraph.Editor {
    public class CodeGraphFileOpener {
        
        [UnityEditor.Callbacks.OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line) {
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            var graph = GraphFileSaveManager.LoadGraphFile(assetPath.Substring(assetPath.LastIndexOf('/') + 1));
            if (graph != null) {
                var window = EditorWindow.GetWindow<GraphEditorWindow>();
                window.SetGraph(graph);
                window.Init();
                return true;
            }
            return false; //let unity open it.
        }
    }
}