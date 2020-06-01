using System.IO;
using System.Text;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace CodeGraph.Editor {
    [ScriptedImporter(13, Extension)]
    public class CodeGraphFunctionImporter : ScriptedImporter {
        public const string Extension = "codegraphfunction";

        public override void OnImportAsset(AssetImportContext ctx) {
            var textGraph = File.ReadAllText(ctx.assetPath, Encoding.UTF8);
            var fileIcon = Resources.Load<Texture2D>("codegraph_256");
            var startIndex = ctx.assetPath.LastIndexOf('/');
            var endIndex = ctx.assetPath.LastIndexOf('.');
            var graphName = ctx.assetPath.Substring(startIndex + 1, endIndex - startIndex - 1);
            // Create new graph object from json
            var changes = false;
            var graph = JsonUtility.FromJson<CodeGraphData>(textGraph);
            if (graph.AssetPath != ctx.assetPath || graph.GraphName != graphName) {
                Debug.LogWarning("Detected file inconsistencies. Writing fixes to disk.");
                graph.AssetPath = ctx.assetPath;
                graph.GraphName = graphName;
                File.WriteAllText(ctx.assetPath, JsonUtility.ToJson(graph, true));
                changes = true;
            }
            var codeGraphObject = ScriptableObject.CreateInstance<CodeGraphObject>();
            codeGraphObject.Initialize(graph);
            ctx.AddObjectToAsset("MainAsset", codeGraphObject, fileIcon);
            ctx.SetMainObject(codeGraphObject);


            var windows = Resources.FindObjectsOfTypeAll<CodeGraph>();
            if (windows.Length > 0 && changes) {
                windows[0].SetGraph(codeGraphObject);
                windows[0].Initialize();
            }
        }
    }
}