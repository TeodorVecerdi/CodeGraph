using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace CodeGraph.Editor {
    public static class CodeGraphFileOpener {
        
        [UnityEditor.Callbacks.OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line) {
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            if (!assetPath.EndsWith(".codegraph"))
                return false; //let unity open it.
            
            var textGraph = File.ReadAllText(assetPath, Encoding.UTF8);
            var graph = JsonUtility.FromJson<CodeGraphData>(textGraph);
            var graphObject = ScriptableObject.CreateInstance<CodeGraphObject>();
            graphObject.Initialize(graph);
                
            var window = EditorWindow.GetWindow<CodeGraph>();
            CodeGraph.Instance = window;
            window.titleContent = new GUIContent("CodeGraph Editor", Resources.Load<Texture2D>("codegraph_256"));
            window.SetGraph(graphObject);
            window.Initialize();
            return true;
        }
    }
}