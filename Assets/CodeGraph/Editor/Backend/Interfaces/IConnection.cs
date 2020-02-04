using System;

namespace CodeGraph {
    public interface IConnection : IEquatable<IConnection> {
        NodeSlot InputSlot { get; }
        NodeSlot OutputSlot { get; }
    }
}