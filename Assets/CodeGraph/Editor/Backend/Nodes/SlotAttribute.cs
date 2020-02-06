using System;

namespace CodeGraph.Nodes {
    
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SlotAttribute : Attribute {
        public int SlotId { get; }
        public bool Hidden { get; }
        public object DefaultValue { get; }

        public SlotAttribute(int slotId) {
            SlotId = slotId;
            DefaultValue = null;
        }

        public SlotAttribute(int slotId, bool hidden) {
            SlotId = slotId;
            Hidden = hidden;
            DefaultValue = null;
        }

        public SlotAttribute(int slotId, object defaultValue) {
            SlotId = slotId;
            DefaultValue = defaultValue;
        }
    }
}