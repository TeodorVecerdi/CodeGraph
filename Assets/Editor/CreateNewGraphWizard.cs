using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeGraph.Editor {
    public class CreateNewGraphWizard : ScriptableWizard {
        public string GeneratedMonoBehaviourName;
        public string GraphName;

        [MenuItem("Code Graph/Create new graph")]
        public static void CreateWizard() {
            var wizard = DisplayWizard<CreateNewGraphWizard>("Create new graph", "Create", "Cancel");
            wizard.GraphName = "NewCodeGraph";
            wizard.GeneratedMonoBehaviourName = wizard.GraphName;
        }

        public void OnWizardCreate() {
            if (!Directory.Exists(Application.dataPath + "/Code Graph"))
                AssetDatabase.CreateFolder("Assets", "Code Graph");
            var graph = new GraphFile(GraphName, GeneratedMonoBehaviourName, new List<GraphFileNode>(), new List<GraphFileConnection>());
            GraphFileSaveManager.SaveGraphFile(graph);
        }

        private void OnWizardOtherButton() {
            Close();
        }

        public void OnWizardUpdate() { }
    }
}