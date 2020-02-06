using System;
using System.Collections.Generic;
using System.Linq;
using CodeGraph.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace CodeGraph.Nodes {
    [Serializable]
    public abstract class AbstractNode : IGeneratesCode, ISerializationCallbackReceiver {
        [NonSerialized] private Guid guid;
        [SerializeField] private string guidSerialized;
        [SerializeField] private string name;
        [SerializeField] private List<AbstractSlot> slots = new List<AbstractSlot>();
        public CodeGraphData Owner { get; set; }
        
        private OnNodeModified onNodeModified;

        public void RegisterCallback(OnNodeModified callback) => onNodeModified += callback;

        public void UnregisterCallback(OnNodeModified callback) => onNodeModified -= callback;

        public void Dirty() => onNodeModified?.Invoke(this);

        public abstract string GetCode();

        public Guid Guid => guid;

        private string defaultVariableName;
        private string nameForDefaultVariableName;
        private Guid guidForDefaultVariableName;

        public string DefaultVariableName {
            get {
                if (nameForDefaultVariableName != name || guidForDefaultVariableName != guid) {
                    defaultVariableName = string.Format("{0}_{1}", CodeGraphUtils.GetCodeSafeName(name ?? "node"), CodeGraphUtils.EncodeGuid(guid));
                    nameForDefaultVariableName = name;
                    guidForDefaultVariableName = guid;
                }
                return defaultVariableName;
            }
        }

        protected AbstractNode() {
            guid = Guid.NewGuid();
        }

        public Guid RewriteGuid() {
            guid = Guid.NewGuid();
            return guid;
        }
        
        public void GetInputSlots<T>(List<T> foundSlots) where T : AbstractSlot {
            foundSlots.AddRange(slots.Where(slot => slot.IsInputSlot && slot is T).Cast<T>());
        }

        public void GetOutputSlots<T>(List<T> foundSlots) where T : AbstractSlot {
            foundSlots.AddRange(slots.Where(slot => slot.IsOutputSlot && slot is T).Cast<T>());
        }

        public void GetSlots<T>(List<T> foundSlots) where T : AbstractSlot {
            foundSlots.AddRange(slots.OfType<T>());
        }
        
        public virtual string GetVariableNameForSlot(int slotId)
        {
            var slot = FindSlot<AbstractSlot>(slotId);
            if (slot == null)
                throw new ArgumentException($"Attempting to use AbstractSlot({slotId}) on node of type {this} where this slot can not be found", nameof(slotId));
            return string.Format("_{0}_{1}_{2}", GetVariableNameForNode(), CodeGraphUtils.GetCodeSafeName(slot.Owner.Owner.Owner.MonoBehaviourName), unchecked((uint)slotId));
        }

        public virtual string GetVariableNameForNode()
        {
            return DefaultVariableName;
        }
        
        public NodeSlot GetSlotReference(int slotId)
        {
            var slot = FindSlot<AbstractSlot>(slotId);
            if (slot == null)
                throw new ArgumentException("Slot could not be found", nameof(slotId));
            return new NodeSlot(slotId,guid);
        }
        
        public T FindSlot<T>(int slotId) where T : AbstractSlot {
            foreach (var slot in slots) {
                if (slot.Id == slotId && slot is T)
                    return (T)slot;
            }
            return default;
        }

        public T FindInputSlot<T>(int slotId) where T : AbstractSlot {
            foreach (var slot in slots) {
                if (slot.IsInputSlot && slot.Id == slotId && slot is T)
                    return (T)slot;
            }
            return default;
        }

        public T FindOutputSlot<T>(int slotId) where T : AbstractSlot {
            foreach (var slot in slots) {
                if (slot.IsOutputSlot && slot.Id == slotId && slot is T)
                    return (T)slot;
            }
            return default;
        }
        
        public virtual IEnumerable<AbstractSlot> GetInputsWithNoConnection() {
            return this.GetInputSlots<AbstractSlot>().Where(x => !Owner.GetConnections(GetSlotReference(x.Id)).Any());
        }
        
        public void OnBeforeSerialize() {
            guidSerialized = guid.ToString();
        }

        public void OnAfterDeserialize() {
            guid = string.IsNullOrEmpty(guidSerialized) ? Guid.NewGuid() : new Guid(guidSerialized);
        }
    }
}