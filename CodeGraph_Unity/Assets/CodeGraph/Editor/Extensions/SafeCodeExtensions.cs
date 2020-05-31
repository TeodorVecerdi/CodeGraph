using System;
using System.Collections.Generic;

namespace CodeGraph.Editor {
    public static class SafeCodeExtensions {
        public static string ToSafeVariableName(this string variableName) {
            var arr = variableName.ToCharArray();
            arr = Array.FindAll(arr, c => char.IsLetterOrDigit(c) || c == '_');
            var safeName = new string(arr);
            if (safeName.Length > 1 && char.IsDigit(safeName[0])) {
                safeName = $"var{safeName} /* ERROR invalid variable name */";
            }
            return safeName;
        }
        public static string ToSafeTypeName(this string typeName) {
            var arr = typeName.ToCharArray();
            arr = Array.FindAll(arr, c => char.IsLetterOrDigit(c) || c == '_');
            var safeName = new string(arr);
            if (safeName.Length > 1 && char.IsDigit(safeName[0])) {
                safeName = $"_{safeName} /* ERROR invalid type name */";
            }
            return safeName;
        }

        public static string ToSafeGUID(this string guid) {
            return guid.Replace("-", "_");
        }

        public static string GenerateSafeName(this string name, string prefix = "variable", string suffix = "") {
            var guidStr = Guid.NewGuid().ToString().ToSafeGUID();
            return prefix + guidStr + suffix;
        }
    }
}