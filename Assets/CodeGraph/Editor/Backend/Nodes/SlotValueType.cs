using System;
using System.Collections.Generic;

namespace CodeGraph.Nodes {

    [Serializable]
    public enum SlotValueType {
        AnyVector,
        Vector2,
        Vector3,
        Vector4,
        Vector2Int,
        Vector3Int,

        Boolean,
        String,
        
        Integer,
        Float,
        Double,

        Any,
        CodeBranch
    }

    public enum ConcreteSlotValueType {
        Vector2,
        Vector3,
        Vector4,
        Vector2Int,
        Vector3Int,
        
        Boolean,
        Integer,
        Float,
        Double,
        String,
        
        CodeBranch
    }

    internal static class SlotValueTypeHelper {
        private static Dictionary<ConcreteSlotValueType, List<SlotValueType>> validConversions;
        private static List<SlotValueType> validSlotTypes;

        public static bool AreCompatible(SlotValueType inputSlotValueType, ConcreteSlotValueType outputSlotValueType) {
            if (validConversions == null) {
                var validVectors = new List<SlotValueType> {
                    SlotValueType.Any, SlotValueType.AnyVector, SlotValueType.Vector2, SlotValueType.Vector3, SlotValueType.Vector4, SlotValueType.Vector2Int, SlotValueType.Vector3Int
                };
                validConversions = new Dictionary<ConcreteSlotValueType, List<SlotValueType>>() {
                    {ConcreteSlotValueType.Vector2, validVectors},
                    {ConcreteSlotValueType.Vector3, validVectors},
                    {ConcreteSlotValueType.Vector4, validVectors},
                    {ConcreteSlotValueType.Vector2Int, validVectors},
                    {ConcreteSlotValueType.Vector3Int, validVectors},
                    {ConcreteSlotValueType.Boolean, new List<SlotValueType>{SlotValueType.Boolean}},
                    {ConcreteSlotValueType.Integer, new List<SlotValueType>{SlotValueType.Integer}},
                    {ConcreteSlotValueType.Float, new List<SlotValueType>{SlotValueType.Float}},
                    {ConcreteSlotValueType.Double, new List<SlotValueType>{SlotValueType.Double}},
                    {ConcreteSlotValueType.String, new List<SlotValueType>{SlotValueType.String}},
                    {ConcreteSlotValueType.CodeBranch, new List<SlotValueType>{SlotValueType.CodeBranch}},
                };
            }
            if(validConversions.TryGetValue(outputSlotValueType, out validSlotTypes))
            {
                return validSlotTypes.Contains(inputSlotValueType);
            }
            throw new ArgumentOutOfRangeException("Unknown Concrete Slot Type: " + outputSlotValueType);
        } 
    }
}