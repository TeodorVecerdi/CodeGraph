using System;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public class Connection : IConnection {
        [SerializeField] private NodeSlot inputSlot;
        [SerializeField] private NodeSlot outputSlot;

        public Connection(NodeSlot inputSlot, NodeSlot outputSlot) {
            this.inputSlot = inputSlot;
            this.outputSlot = outputSlot;
        }

        protected bool Equals(Connection other) {
            return inputSlot.Equals(other.inputSlot) && outputSlot.Equals(other.outputSlot);
        }

        public bool Equals(IConnection other) {
            return Equals(other as object);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Connection) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (inputSlot.GetHashCode() * 397) ^ outputSlot.GetHashCode();
            }
        }
        
        public NodeSlot InputSlot => inputSlot;
        public NodeSlot OutputSlot => outputSlot;
    }
}