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
        [NonSerialized] public Dictionary<Guid, List<IGroupItem>> GroupItems = new Dictionary<Guid, List<IGroupItem>>();

        public void AddGroup(GroupData groupData) {
            if (!Groups.Contains(groupData))
                Groups.Add(groupData);
            if (!GroupItems.ContainsKey(groupData.Guid))
                GroupItems.Add(groupData.Guid, new List<IGroupItem>());
        }

        public void SetGroup(IGroupItem node, GroupData group) {
            var groupGuid = group?.Guid ?? Guid.Empty;
            if (node.GroupGuid != Guid.Empty) {
                Debug.Log(groupGuid);
                var oldGroupNodes = GroupItems[groupGuid];
                oldGroupNodes.Remove(node);
            }

            node.GroupGuid = groupGuid;
            GroupItems[groupGuid].Add(node);
        }
    }
}