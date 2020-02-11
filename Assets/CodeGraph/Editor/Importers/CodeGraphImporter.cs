using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace CodeGraph {
    [ScriptedImporter(1, Extension)]
    public class CodeGraphImporter : ScriptedImporter {
        public const string Extension = "codegraph";
        public override void OnImportAsset(AssetImportContext ctx) {
            var graph = GraphFileSaveManager.LoadGraphFile(ctx.assetPath);
            Debug.Log(AssetDatabase.AssetPathToGUID(ctx.assetPath));
            
            Texture2D texture = Resources.Load<Texture2D>("Icons/codegraph_256");
            // var obj = ScriptableObject.CreateInstance<CodeGraphObject>();
            // obj.Init(graph);
            ctx.AddObjectToAsset("MainAsset", graph, texture);
            ctx.SetMainObject(graph);
        }
    }
}