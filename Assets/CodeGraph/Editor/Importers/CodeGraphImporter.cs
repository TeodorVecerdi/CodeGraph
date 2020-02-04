using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace CodeGraph {
    [ScriptedImporter(1, Extension)]
    public class CodeGraphImporter : ScriptedImporter {
        public const string Extension = "codegraph";
        public override void OnImportAsset(AssetImportContext ctx) {
            var graph = GraphFileSaveManager.LoadGraphFile(ctx.assetPath);
            
            Texture2D texture = Resources.Load<Texture2D>("Icons/sg_graph_icon@64");
            var obj = ScriptableObject.CreateInstance<GraphFileObject>();
            obj.Init(graph);
            ctx.AddObjectToAsset("MainAsset", obj, texture);
            ctx.SetMainObject(obj);
        }
    }
}