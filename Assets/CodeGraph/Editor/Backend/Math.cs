using System;

namespace CodeGraph {
    
    public static class Math {
        /// <summary>
        ///     Returns <paramref name="value" /> mapped from one range [<paramref name="minA" />, <paramref name="maxA" />] to
        ///     another range [<paramref name="minB" />, <paramref name="maxB" />]
        /// </summary>
        public static float Map(float value, float minA, float maxA, float minB, float maxB) {
            return (value - minA) / (maxA - minA) * (maxB - minB) + minB;
        }

        /// <summary>
        ///     Returns <paramref name="value" /> mapped from one range [<paramref name="minA" />, <paramref name="maxA" />] to
        ///     another range [<paramref name="minB" />, <paramref name="maxB" />]
        /// </summary>
        /// <param name="_">This exists only to allow two methods with the same name since this method is a float extension</param>
        public static float Map(this float value, float minA, float maxA, float minB, float maxB, bool _ = false) {
            return (value - minA) / (maxA - minA) * (maxB - minB) + minB;
        }
        
        /// <summary>
        /// Checks if <paramref name="comp"/> is between <paramref name="min"/> (inclusive) and <paramref name="max"/> (exclusive).
        /// </summary>
        /// <param name="comp">The value to compare</param>
        /// <param name="min">The minimum value to compare against (inclusive)</param>
        /// <param name="max">The maximum value to compare against (exclusive)</param>
        /// <returns><value>true</value> if <para>comp</para>>=<para>min</para> and <para>comp</para><<para>max</para></returns>
        public static bool Between<TA>(this TA comp, TA min, TA max) where TA : IComparable {
            return comp.CompareTo(min) >= 0 && comp.CompareTo(max) < 0;
        }
    }
}
