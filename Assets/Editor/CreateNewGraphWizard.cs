using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeGraph.Editor {
    public class CreateNewGraphWizard : ScriptableWizard {
        public string GeneratedMonoBehaviourName;
        public string GraphName;

        [MenuItem(itemName:"Assets/Create/Code Graph/New Empty Graph", isValidateFunction: false, priority: 0)]
        public static void CreateWizard() {
            var wizard = DisplayWizard<CreateNewGraphWizard>("Create new graph", "Create", "Cancel");
            wizard.GraphName = "NewCodeGraph";
            wizard.GeneratedMonoBehaviourName = wizard.GraphName;
        }

        public void OnWizardCreate() {
            if (!Directory.Exists(Application.dataPath + "/Code Graph"))
                AssetDatabase.CreateFolder("Assets", "Code Graph");
            var path = Path.Combine(Application.dataPath, "Code Graph", GraphName + ".codegraph");
            var graph = new GraphFile(GraphName, GeneratedMonoBehaviourName, new List<GraphFileNode>(), new List<GraphFileConnection>(), path);
            GraphFileSaveManager.SaveGraphFile(path, graph);
        }

        private void OnWizardOtherButton() {
            Close();
        }

        public void OnWizardUpdate() { }
    }
}