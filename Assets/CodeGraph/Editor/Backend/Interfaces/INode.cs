using System.Collections.Generic;
using CodeGraph.Nodes;

namespace CodeGraph {
    public delegate void OnNodeModified(NodeBase node);

    static class NodeExtensions {
        public static IEnumerable<T> GetSlots<T>(this NodeBase node) where T : SlotBase {
            var slots = new List<T>();
            node.GetSlots(slots);
            return slots;
        }

        public static IEnumerable<T> GetInputSlots<T>(this NodeBase node) where T : SlotBase {
            var slots = new List<T>();
            node.GetInputSlots(slots);
            return slots;
        }

        public static IEnumerable<T> GetOutputSlots<T>(this NodeBase node) where T : SlotBase {
            var slots = new List<T>();
            node.GetOutputSlots(slots);
            return slots;
        }
    }
}