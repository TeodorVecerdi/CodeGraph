using System;
using System.Collections.Generic;
using System.Linq;
using CodeGraph.Editor;
using GitHub.ICSharpCode.SharpZipLib.Zip;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph {
    public static class SerializationHelper {
        public static SerializedNode SerializeNode(AbstractNode node) {
            return new SerializedNode {
                GUID = node.GUID,
                NodeType = node.GetType().Name,
                Position = node.GetPosition().position,
                GroupGUID = node.GroupGuid.ToString(),
                NodeData = node.GetNodeData()
            };
        }

        public static List<SerializedNode> SerializeNodes(List<AbstractNode> nodes) {
            var serializedNodes = new List<SerializedNode>();
            serializedNodes.AddRange(nodes.Select(SerializeNode));
            return serializedNodes;
        }

        public static AbstractNode DeserializeNode(SerializedNode serializedNode) {
            var type = Type.GetType("CodeGraph.Editor." + serializedNode.NodeType);
            var deserializedNode = (AbstractNode) Activator.CreateInstance(type);
            deserializedNode.GUID = serializedNode.GUID;
            deserializedNode.GroupGuid = string.IsNullOrEmpty(serializedNode.GroupGUID) ? Guid.Empty : new Guid(serializedNode.GroupGUID);
            deserializedNode.SetNodeData(serializedNode.NodeData);
            deserializedNode.SetPosition(new Rect(serializedNode.Position, AbstractNode.DefaultNodeSize));
            if (deserializedNode is AbstractEventNode eventNode) {
                for (var i = 0; i < eventNode.PortCount; i++) eventNode.AddChildPort(false);
            }
            deserializedNode.Refresh();
            return deserializedNode;
        }

        public static List<AbstractNode> DeserializeNodes(List<SerializedNode> nodes) {
            var deserializedNodes = new List<AbstractNode>();
            deserializedNodes.AddRange(nodes.Select(DeserializeNode));
            return deserializedNodes;
        }

        public static SerializedEdge SerializeEdge(Edge edge) {
            var inputNode = edge.input.node as AbstractNode;
            var outputNode = edge.output.node as AbstractNode;
            var inputPortIndex = -1;
            for (var i = 0; i < inputNode.InputPorts.Count; i++) {
                if (edge.input == inputNode.InputPorts[i].PortReference) {
                    inputPortIndex = i;
                    break;
                }
            }

            var outputPortIndex = -1;
            for (var i = 0; i < outputNode.OutputPorts.Count; i++) {
                if (edge.output == outputNode.OutputPorts[i].PortReference) {
                    outputPortIndex = i;
                    break;
                }
            }

            return new SerializedEdge {
                SourceNodeGUID = outputNode.GUID,
                SourceNodeIndex = outputPortIndex,
                TargetNodeGUID = inputNode.GUID,
                TargetNodeIndex = inputPortIndex
            };
        }
        
        public static List<SerializedEdge> SerializeEdges(List<Edge> edges) {
            var serializeEdges = new List<SerializedEdge>();
            serializeEdges.AddRange(edges.Select(SerializeEdge));
            return serializeEdges;
        }

        public static Edge DeserializeAndLinkEdge(SerializedEdge edge, List<AbstractNode> nodes) {
            var sourceNode = nodes.First(x => x.GUID == edge.SourceNodeGUID);
            var targetNode = nodes.First(x => x.GUID == edge.TargetNodeGUID);
            var deserializedEdge = LinkNodesTogether(sourceNode.outputContainer[edge.SourceNodeIndex].Q<Port>(),
                targetNode.inputContainer[edge.TargetNodeIndex].Q<Port>());
            return deserializedEdge;
        }

        public static List<Edge> DeserializeAndLinkEdges(List<SerializedEdge> edges, List<AbstractNode> nodes) {
            var deserializedEdges = new List<Edge>();
            deserializedEdges.AddRange(edges.Select(edge => DeserializeAndLinkEdge(edge, nodes)));
            return deserializedEdges;
        }
        
        public static Edge LinkNodesTogether(Port outputSocket, Port inputSocket) {
            var edge = new Edge {
                output = outputSocket,
                input = inputSocket
            };
            edge.input.Connect(edge);
            edge.output.Connect(edge);
            return edge;
        }
    }
}