using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace CodeGraph {
    public static class GraphFileSaveManager {
        public static void SaveGraphFile(GraphFile file) {
            string savePath = Application.dataPath + "/Code Graph/" + file.GraphName + ".codegraph";
            var binaryFormatter = new BinaryFormatter();
            var fileStream = File.Create(savePath);
            binaryFormatter.Serialize(fileStream, file);
            fileStream.Close();
            AssetDatabase.Refresh();
        }

        public static GraphFile LoadGraphFile(string graphFileName) {
            string savePath = Application.dataPath + "/Code Graph/" + graphFileName;
            if (!savePath.EndsWith(".codegraph")) savePath += ".codegraph";
            if (!File.Exists(savePath)) {
                Debug.LogError($"Could not find graph at path {savePath}");
                return null;
            }
            var binaryFormatter = new BinaryFormatter();
            var fileStream = File.Open(savePath, FileMode.Open);
            var graph = (GraphFile)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return graph;
        }
    }
}