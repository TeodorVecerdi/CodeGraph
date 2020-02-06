using System;
using System.Linq;
using UnityEngine;

namespace CodeGraph.Nodes {
    [Serializable]
    public abstract class AbstractSlot : IEquatable<AbstractSlot> {
        private const string notInit = "Not Initialized";

        [SerializeField] private int id;
        [SerializeField] private string displayName = notInit;
        [SerializeField] private SlotType slotType = SlotType.Input;
        [SerializeField] private bool hidden;

        protected AbstractSlot() { }

        protected AbstractSlot(int slotId, string displayName, SlotType slotType, bool hidden = false) {
            id = slotId;
            this.displayName = displayName;
            this.slotType = slotType;
            this.hidden = hidden;
        }

        static string ConcreteSlotValueTypeAsString(ConcreteSlotValueType concreteSlotValueType) {
            switch (concreteSlotValueType) {
                case ConcreteSlotValueType.Vector2:
                    return "(V2)";
                case ConcreteSlotValueType.Vector3:
                    return "(V3)";
                case ConcreteSlotValueType.Vector4:
                    return "(V4)";
                case ConcreteSlotValueType.Vector2Int:
                    return "(V2I)";
                case ConcreteSlotValueType.Vector3Int:
                    return "(V3I)";
                case ConcreteSlotValueType.Boolean:
                    return "(B)";
                case ConcreteSlotValueType.Integer:
                    return "(I)";
                case ConcreteSlotValueType.Float:
                    return "(F)";
                case ConcreteSlotValueType.Double:
                    return "(D)";
                case ConcreteSlotValueType.String:
                    return "(S)";
                case ConcreteSlotValueType.CodeBranch:
                    return "(CB)";
                default:
                    return "(Err)";
            }
        }

        public string DisplayName {
            get => displayName + ConcreteSlotValueTypeAsString(concreteValueType);
            set => displayName = value;
        }

        public bool isConnected
        {
            get
            {
                if (Owner == null || Owner.Owner == null)
                    return false;

                var graph = Owner.Owner;
                var edges = graph.GetConnections(SlotReference);
                return edges.Any();
            }
        }

        public NodeSlot SlotReference => new NodeSlot(id, Owner.Guid);
        public AbstractNode Owner { get; set; }
        public bool Hidden {
            get => hidden;
            set => hidden = value;
        }

        public int Id => id;

        public bool IsInputSlot => slotType == SlotType.Input;

        public bool IsOutputSlot => slotType == SlotType.Output;

        public SlotType SlotType => slotType;

        public abstract SlotValueType valueType { get; }

        public abstract ConcreteSlotValueType concreteValueType { get; }

        public bool IsCompatibleWith(AbstractSlot otherSlot) {
            return otherSlot != null
                   && otherSlot.Owner != Owner
                   && otherSlot.IsInputSlot != IsInputSlot
                   && (IsInputSlot
                       ? SlotValueTypeHelper.AreCompatible(valueType, otherSlot.concreteValueType)
                       : SlotValueTypeHelper.AreCompatible(otherSlot.valueType, concreteValueType));
        }

        public bool Equals(AbstractSlot other) => Equals(other as object);

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((AbstractSlot) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (id * 397) ^ (Owner != null ? Owner.GetHashCode() : 0);
            }
        }
    }
}