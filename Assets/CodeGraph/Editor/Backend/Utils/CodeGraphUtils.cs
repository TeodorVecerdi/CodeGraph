using System;

namespace CodeGraph.Utils {
    public static class CodeGraphUtils {
        public static string GetCodeSafeName(string input) {
            var safeName = new string(Array.FindAll(input.ToCharArray(), char.IsLetterOrDigit));
            if (safeName.Length > 1 && char.IsDigit(safeName[0])) {
                safeName = $"var{safeName}";
            }
            return safeName;
        }
        
        public static string EncodeGuid(Guid guid) => $"{Convert.ToBase64String(guid.ToByteArray()).GetHashCode():X}";
    }
}