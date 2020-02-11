using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeGraph.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace CodeGraph.Nodes {
    [Serializable]
    public abstract class NodeBase : IGeneratesCode, ISerializationCallbackReceiver {
        [NonSerialized] private Guid guid;
        [SerializeField] private string guidSerialized;
        [SerializeField] private string name;
        [SerializeField] private List<SlotBase> slots = new List<SlotBase>();
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

        protected NodeBase() {
            guid = Guid.NewGuid();
        }

        public Guid RewriteGuid() {
            guid = Guid.NewGuid();
            return guid;
        }
        
        public void GetInputSlots<T>(List<T> foundSlots) where T : SlotBase {
            foundSlots.AddRange(slots.Where(slot => slot.IsInputSlot && slot is T).Cast<T>());
        }
        public void GetOutputSlots<T>(List<T> foundSlots) where T : SlotBase {
            foundSlots.AddRange(slots.Where(slot => slot.IsOutputSlot && slot is T).Cast<T>());
        }
        public void GetSlots<T>(List<T> foundSlots) where T : SlotBase {
            foundSlots.AddRange(slots.OfType<T>());
        }
        
        public T FindSlot<T>(int slotId) where T : SlotBase {
            foreach (var slot in slots) {
                if (slot.Id == slotId && slot is T)
                    return (T)slot;
            }
            return default;
        }
        public T FindInputSlot<T>(int slotId) where T : SlotBase {
            foreach (var slot in slots) {
                if (slot.IsInputSlot && slot.Id == slotId && slot is T)
                    return (T)slot;
            }
            return default;
        }
        public T FindOutputSlot<T>(int slotId) where T : SlotBase {
            foreach (var slot in slots) {
                if (slot.IsOutputSlot && slot.Id == slotId && slot is T)
                    return (T)slot;
            }
            return default;
        }
        
        public void AddSlot(SlotBase slot) {
            slots.RemoveAll(x => x.Id == slot.Id);
            slots.Add(slot);
            slot.Owner = this;
        }

        public void RemoveSlot(int slotId)
        {
            // Remove edges that use this slot
            // no owner can happen after creation
            // but before added to graph
            if (Owner != null)
            {
                var connections = Owner.GetConnections(GetSlotReference(slotId));

                foreach (var connection in connections.ToArray())
                    Owner.RemoveConnection(connection);
            }

            slots.RemoveAll(x => x.Id == slotId);
        }
        
        public virtual string GetVariableNameForSlot(int slotId)
        {
            var slot = FindSlot<SlotBase>(slotId);
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
            var slot = FindSlot<SlotBase>(slotId);
            if (slot == null)
                throw new ArgumentException("Slot could not be found", nameof(slotId));
            return new NodeSlot(slotId,guid);
        }
        
        protected abstract MethodInfo GetFunctionToConvert();
        public virtual IEnumerable<SlotBase> GetInputsWithNoConnection() {
            return this.GetInputSlots<SlotBase>().Where(x => !Owner.GetConnections(GetSlotReference(x.Id)).Any());
        }
        
        public void OnBeforeSerialize() {
            guidSerialized = guid.ToString();
        }

        public void OnAfterDeserialize() {
            guid = string.IsNullOrEmpty(guidSerialized) ? Guid.NewGuid() : new Guid(guidSerialized);
        }
    }
}