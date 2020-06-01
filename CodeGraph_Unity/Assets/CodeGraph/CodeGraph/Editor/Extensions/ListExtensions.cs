using System.Collections.Generic;

namespace CodeGraph.Editor {
    public static class ListExtensions {
        public static string Join<T>(this IEnumerable<T> list, string separator) {
            return list == null ? null : string.Join(separator, list);
        }
        public static string Join(this IEnumerable<string> list, string separator) {
            return list == null ? null : string.Join(separator, list);
        }
    }
}