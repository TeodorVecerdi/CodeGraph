using UnityEditor;

namespace CodeGraph {
    public class CustomAssetOpener {
        [UnityEditor.Callbacks.OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line) {
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            
            TestScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<TestScriptableObject>(assetPath);
            if (scriptableObject != null) {
                var window = EditorWindow.GetWindow<TestWindow2>();
                window.SetScriptableObject(scriptableObject);
                window.Init();
                return true;
            }

            return false; //let unity open it.
        }
    }
}