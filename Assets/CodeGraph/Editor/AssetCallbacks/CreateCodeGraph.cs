using UnityEditor;
using UnityEditor.ProjectWindowCallback;

namespace CodeGraph {
    public class CreateCodeGraph : EndNameEditAction {
        [MenuItem("Assets/Create/Code Graph/Empty Graph", false, 0)]
        public static void CreateEmptyCodeGraph() {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, CreateInstance<CreateCodeGraph>(),
                "New Code Graph.codegraph", null, null);
        }

        public override void Action(int instanceId, string pathName, string resourceFile) {
            var startIndex = pathName.LastIndexOf('/');
            var endIndex = pathName.LastIndexOf('.');
            var graphName = pathName.Substring(startIndex + 1, endIndex - startIndex - 1);
            var graph = CreateInstance<CodeGraphObject>();
            graph.Graph = new CodeGraphData();
            graph.Graph.AssetGuid = AssetDatabase.AssetPathToGUID(pathName);
            graph.Graph.GraphName = graphName;
            graph.Graph.MonoBehaviourName = graphName;
            graph.Init(graphName, graphName, graph.Graph.AssetGuid);
            GraphFileSaveManager.SaveGraphFile(pathName, graph);
            AssetDatabase.Refresh();
        }
    }
}