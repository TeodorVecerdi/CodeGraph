using System;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public class TestBox {
        public Vector2 Position;
        public Vector2 Size;
        public String Title;
        public TestBox(Vector2 position, Vector2 size, string title) {
            Position = position;
            Size = size;
            Title = title;
        }
    }
}