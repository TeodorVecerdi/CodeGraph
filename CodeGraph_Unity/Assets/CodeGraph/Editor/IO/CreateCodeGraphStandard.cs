using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace CodeGraph.Editor {
    public class CreateCodeGraphStandard : EndNameEditAction {
        [MenuItem ("Assets/Create/Empty Code Graph Class", false, 0)]
        public static void CreateEmptyCodeGraph () {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists (0, CreateInstance<CreateCodeGraphStandard> (),
                "New Code Graph Class.codegraph", Resources.Load<Texture2D> ("codegraph_256"), null);
        }

        public override void Action (int instanceId, string pathName, string resourceFile) {
            var startIndex = pathName.LastIndexOf ('/');
            var endIndex = pathName.LastIndexOf ('.');
            var graphName = pathName.Substring (startIndex + 1, endIndex - startIndex - 1);
            var graph = new CodeGraphData { AssetPath = pathName, GraphName = graphName, IsMonoBehaviour = false };
            var json = JsonUtility.ToJson (graph, true);
            File.WriteAllText (pathName, json);
            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh ();
        }
    }
}