using System;
using System.Collections.Generic;
using CodeGraph.Editor;
using UnityEngine;

namespace CodeGraph {
    [Serializable] public class CodeGraphData {
        public static uint SchemaVersionLatest = 1;
        
        [SerializeField] public string AssetPath;
        [SerializeField] public string GraphName;
        [SerializeField] public string LastEditedAt = "0";
        [SerializeField] public uint SchemaVersion = SchemaVersionLatest;
        [SerializeField] public bool IsMonoBehaviour;
        [SerializeField] public List<SerializedNode> Nodes = new List<SerializedNode>();
        [SerializeField] public List<SerializedEdge> Edges = new List<SerializedEdge>();
        [SerializeField] public List<GroupData> Groups = new List<GroupData>();
        [NonSerialized] private Dictionary<Guid, List<IGroupItem>> groupItems = new Dictionary<Guid, List<IGroupItem>>();

        public void CreateGroup(GroupData groupData) {
            if (AddGroup(groupData)) { }
        }

        private bool AddGroup(GroupData groupData) {
            if (Groups.Contains(groupData))
                return false;
            Groups.Add(groupData);
            groupItems.Add(groupData.Guid, new List<IGroupItem>());
            return true;
        }

        public void SetGroup(IGroupItem node, GroupData group) {
            var groupGuid = group?.Guid ?? Guid.Empty;
            node.GroupGuid = groupGuid;
            var oldGroupNodes = groupItems[groupGuid];
            oldGroupNodes.Remove(node);
            groupItems[groupGuid].Add(node);
        }
    }
}