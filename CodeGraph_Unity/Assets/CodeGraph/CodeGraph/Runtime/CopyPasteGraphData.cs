using System;
using System.Collections.Generic;
using System.Linq;
using CodeGraph.Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public class CopyPasteGraphData : ISerializationCallbackReceiver {
        [NonSerialized] private HashSet<AbstractNode> nodes = new HashSet<AbstractNode>();
        [NonSerialized] private HashSet<Edge> edges = new HashSet<Edge>();

        [SerializeField] private string sourceGraphGUID;
        [SerializeField] private List<SerializedNode> serializedNodes = new List<SerializedNode>();
        [SerializeField] private List<SerializedEdge> serializedEdges = new List<SerializedEdge>();

        public string SourceGraphGUID => sourceGraphGUID;
        public HashSet<AbstractNode> Nodes => nodes;
        public HashSet<Edge> Edges => edges;

        public CopyPasteGraphData(string sourceGraphGUID, List<AbstractNode> nodes, List<Edge> edges) {
            this.sourceGraphGUID = sourceGraphGUID;
            nodes.ForEach(AddNode);
            edges.ForEach(AddEdge);
            nodes.ForEach(node => GetEdgesForNode(node).ForEach(AddEdge));
        }

        public void AddNode(AbstractNode node) {
            nodes.Add(node);
        }

        public void AddEdge(Edge edge) {
            if(Nodes.Contains(edge.input.node) && Nodes.Contains(edge.output.node))
                edges.Add(edge);
        }

        public void OnBeforeSerialize() {
            serializedNodes = SerializationHelper.SerializeNodes(nodes.ToList());
            serializedEdges = SerializationHelper.SerializeEdges(edges.ToList());
        }

        public void OnAfterDeserialize() {
            var deserializedNodes = SerializationHelper.DeserializeNodes(serializedNodes);
            nodes = new HashSet<AbstractNode>();
            foreach (var node in deserializedNodes)
                nodes.Add(node);
            serializedNodes = null;
            


            var deserializedEdges = SerializationHelper.DeserializeAndLinkEdges(serializedEdges, nodes.ToList());
            edges = new HashSet<Edge>();
            foreach (var edge in deserializedEdges)
                edges.Add(edge);
            serializedEdges = null;
        }

        private List<Edge> GetEdgesForNode(AbstractNode node) {
            var edges = new List<Edge>();
            foreach (var port in node.InputPorts) {
                edges.AddRange(port.PortReference.connections);
            }

            foreach (var port in node.OutputPorts) {
                edges.AddRange(port.PortReference.connections);
            }

            return edges;
        }

        internal static CopyPasteGraphData FromJson(string copyBuffer) {
            try {
                return JsonUtility.FromJson<CopyPasteGraphData>(copyBuffer);
            } catch {
                // ignored. just means copy buffer was not a graph :(
                return null;
            }
        }
    }
}