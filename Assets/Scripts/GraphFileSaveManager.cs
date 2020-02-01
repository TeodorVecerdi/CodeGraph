using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace CodeGraph {
    public static class GraphFileSaveManager {
        public static void SaveGraphFile(string path, GraphFile file) {
            string savePath = path;
            var binaryFormatter = new BinaryFormatter();
            var fileStream = File.Create(savePath);
            binaryFormatter.Serialize(fileStream, file);
            fileStream.Close();
            AssetDatabase.Refresh();
        }

        public static GraphFile LoadGraphFile(string path) {
            // string savePath = Application.dataPath + "/Code Graph/" + graphFileName;
            // if (!savePath.EndsWith(".codegraph")) savePath += ".codegraph";
            if (!File.Exists(path)) {
                // Debug.LogError($"Could not find graph at path {savePath}");
                return null;
            }
            var binaryFormatter = new BinaryFormatter();
            var fileStream = File.Open(path, FileMode.Open);
            var graph = (GraphFile)binaryFormatter.Deserialize(fileStream);
            if (graph != null) graph.AssetGuid = AssetDatabase.AssetPathToGUID(path);
            fileStream.Close();
            return graph;
        }
    }
}