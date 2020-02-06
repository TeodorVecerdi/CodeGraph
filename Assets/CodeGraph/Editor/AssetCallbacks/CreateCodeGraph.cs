using System;
using System.Collections.Generic;
using System.IO;
using CodeGraph;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace CodeGraph {
    public class CreateCodeGraph : EndNameEditAction {
        [MenuItem(itemName: "Assets/Create/Code Graph/Empty Graph", isValidateFunction: false, priority: 0)]
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
            graph.Init(graphName, AssetDatabase.AssetPathToGUID(pathName), graphName);
            GraphFileSaveManager.SaveGraphFile(pathName, graph);
            AssetDatabase.Refresh();
        }
    }
}