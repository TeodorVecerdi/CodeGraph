using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace CodeGraph.Editor {
    public class CreateCodeGraphFunction : EndNameEditAction {
        [MenuItem ("Assets/Create/Empty Code Graph Function", false, 0)]
        public static void CreateEmptyCodeGraphFunction () {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists (0, CreateInstance<CreateCodeGraphFunction> (),
                "New Code Graph Function.codegraphfunction", Resources.Load<Texture2D> ("codegraph_256"), null);
        }

        public override void Action (int instanceId, string pathName, string resourceFile) {
            var startIndex = pathName.LastIndexOf ('/');
            var endIndex = pathName.LastIndexOf ('.');
            var graphName = pathName.Substring (startIndex + 1, endIndex - startIndex - 1);
            var graph = new CodeGraphData { AssetPath = pathName, GraphName = graphName };
            var json = JsonUtility.ToJson (graph, true);
            File.WriteAllText (pathName, json);
            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh ();
        }
    }
}