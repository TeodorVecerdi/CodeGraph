using System;
using System.Collections.Generic;
using System.Linq;
using CodeGraph.Nodes;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public class CodeGraphData {
        [SerializeField] public string GraphName;
        [SerializeField] public string MonoBehaviourName;
        [SerializeField] public string AssetGuid;
        [NonSerialized] public CodeGraphObject Owner;
        [SerializeField] private List<NodeBase> nodes = new List<NodeBase>();
        [SerializeField] private Dictionary<Guid, NodeBase> nodeDictionary = new Dictionary<Guid, NodeBase>();
        public IEnumerable<T> GetNodes<T>() => nodes.Where(x => x != null).OfType<T>();

        [SerializeField] private List<Connection> connections = new List<Connection>();
        [SerializeField] private Dictionary<Guid, List<Connection>> connectionDictionary = new Dictionary<Guid, List<Connection>>();
        public IEnumerable<Connection> Connections => connections;

        public void AddNode(NodeBase node) {
            node.Owner = this;
            nodes.Add(node);
            nodeDictionary.Add(node.Guid, node);
        }

        public void RemoveNode(NodeBase node) {
            if (!nodeDictionary.ContainsKey(node.Guid)) {
                throw new InvalidOperationException("Cannot remove node that doesn't exist.");
            }
            nodes.Remove(node);
            nodeDictionary.Remove(node.Guid);
        }

        public void RemoveConnection(Connection connection) {
            connection = connections.FirstOrDefault(x => x.Equals(connection));
            if (connection == null)
                throw new ArgumentException("Trying to remove an edge that does not exist.", nameof(connection));
            connections.Remove(connection);

            List<Connection> inputNodeConnections;
            if (connectionDictionary.TryGetValue(connection.InputSlot.NodeGuid, out inputNodeConnections))
                inputNodeConnections.Remove(connection);

            List<Connection> outputNodeConnections;
            if (connectionDictionary.TryGetValue(connection.OutputSlot.NodeGuid, out outputNodeConnections))
                outputNodeConnections.Remove(connection);
        }
        

        /*public CodeGraphData(string graphName, string generatedMonoBehaviourName, List<GraphFileNode> nodes, List<GraphFileConnection> connections, string assetGuid) {
            GraphName = graphName;
            // FilePath = filePath;
            GeneratedMonoBehaviourName = generatedMonoBehaviourName;
            Nodes = nodes;
            Connections = connections;
        }*/
        
        public NodeBase GetNodeFromGuid(Guid guid) {
            nodeDictionary.TryGetValue(guid, out var node);
            return node;
        }

        public bool ContainsNodeGuid(Guid guid) => nodeDictionary.ContainsKey(guid);
        
        public T GetNodeFromGuid<T>(Guid guid) where T : NodeBase {
            var node = GetNodeFromGuid(guid);
            return node is T abstractNode ? abstractNode : default;
        }
        
        public List<Connection> GetConnections(NodeSlot s) {
            var connections = new List<Connection>();
            var node = GetNodeFromGuid(s.NodeGuid);
            if (node == null) {
                return null;
            }
            var slot = node.FindSlot<SlotBase>(s.SlotId);
            if (!connectionDictionary.TryGetValue(s.NodeGuid, out var availableConnections))
                return null;
            connections.AddRange(
                from connection in availableConnections
                let nodeSlot = slot.IsInputSlot ? connection.InputSlot : connection.OutputSlot
                where nodeSlot.NodeGuid == s.NodeGuid && nodeSlot.SlotId == s.SlotId 
                select connection
                );
            return connections;
        }
    }
    
    [Serializable]
    public class CodeGraphObject : ScriptableObject {
        [SerializeField] private string graphName;
        [SerializeField] private string assetGuid;
        [SerializeField] private string generatedMonoBehaviourName;
        [SerializeField] private CodeGraphData codeGraphData;

        public string MonoBehaviourName => generatedMonoBehaviourName;
        public string GraphName => graphName;
        public string AssetGuid {
            get => assetGuid;
            set => assetGuid = value;
        }
        public CodeGraphData Graph {
            get => codeGraphData;
            set {
                if (codeGraphData != null)
                    codeGraphData.Owner = null;
                codeGraphData = value;
                if (codeGraphData != null)
                    codeGraphData.Owner = this;
            }
        }

        public void Init(string graphName, string generatedMonoBehaviourName, string assetGuid) {
            this.graphName = graphName;
            this.assetGuid = assetGuid;
            this.generatedMonoBehaviourName = generatedMonoBehaviourName;
            name = graphName;
        }
    }
}