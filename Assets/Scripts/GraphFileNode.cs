using System;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public class GraphFileNode {
        public float PositionX;
        public float PositionY;
        public float SizeX;
        public float SizeY;
        public Vector2 Position {
            get => new Vector2(PositionX, PositionY);
            set {
                PositionX = value.x;
                PositionY = value.y;
            }
        }
        public Vector2 Size{
            get => new Vector2(SizeX, SizeY);
            set {
                SizeX = value.x;
                SizeY = value.y;
            }
        }
        public string Title;
        public Guid Guid;

        public GraphFileNode(float positionX, float positionY, float sizeX, float sizeY, string title, Guid guid) {
            PositionX = positionX;
            PositionY = positionY;
            SizeX = sizeX;
            SizeY = sizeY;
            Title = title;
            Guid = guid;
        }

        public GraphFileNode(Vector2 position, Vector2 size, string title, Guid guid) {
            Position = position;
            Size = size;
            Title = title;
            Guid = guid;
        }
    }
}