using System;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public struct NodeSlot : IEquatable<NodeSlot>, ISerializationCallbackReceiver {
        [SerializeField] private int slotId;
        [NonSerialized] private Guid nodeGuid;
        [SerializeField] private string serializedNodeGuid;

        public NodeSlot(int slotId, Guid nodeGuid) {
            this.slotId = slotId;
            this.nodeGuid = nodeGuid;
            serializedNodeGuid = string.Empty;
        }

        public bool Equals(NodeSlot other) {
            return slotId == other.slotId && nodeGuid.Equals(other.nodeGuid);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj.GetType() == GetType() && Equals((NodeSlot) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (slotId * 397) ^ nodeGuid.GetHashCode();
            }
        }

        public void OnBeforeSerialize() {
            serializedNodeGuid = nodeGuid.ToString();
        }

        public void OnAfterDeserialize() {
            nodeGuid = new Guid(serializedNodeGuid);
        }
    }
}