using System.Security.Cryptography;
using UnityEngine.Rendering;

namespace Nodes {
    public class NodeConnection {
        public OutputNode From;
        public InputNode To;

        public static NodeConnection Create(OutputNode from, InputNode to) {
            return new NodeConnection {From = @from, To = to};
        }
    }
}