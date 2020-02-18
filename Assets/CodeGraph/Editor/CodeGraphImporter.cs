using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace CodeGraph.Editor {
    [ScriptedImporter(13, Extension)]
    public class CodeGraphImporter : ScriptedImporter {
        public const string Extension = "codegraph";

        public override void OnImportAsset(AssetImportContext ctx) {
            var textGraph = File.ReadAllText(ctx.assetPath, Encoding.UTF8);
            var fileIcon = Resources.Load<Texture2D>("codegraph_256");

            // Create new graph object from json
            var graph = JsonUtility.FromJson<CodeGraphData>(textGraph);
            if (string.IsNullOrEmpty(graph.AssetPath))
                graph.AssetPath = ctx.assetPath;
            var codeGraphObject = ScriptableObject.CreateInstance<CodeGraphObject>();
            codeGraphObject.Initialize(graph);
            ctx.AddObjectToAsset("MainAsset", codeGraphObject, fileIcon);
            ctx.SetMainObject(codeGraphObject);
        }
    }
}