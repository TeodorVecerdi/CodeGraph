using System;
using UnityEngine;

namespace CodeGraph {
    [Serializable]
    public struct GraphFileNode {
        [SerializeField] public float PositionX;
        [SerializeField] public float PositionY;
        [SerializeField] public float SizeX;
        [SerializeField] public float SizeY;
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
        [SerializeField] public string Title;
        [SerializeField] public Guid Guid;

        public GraphFileNode(float positionX, float positionY, float sizeX, float sizeY, string title, Guid guid) {
            PositionX = positionX;
            PositionY = positionY;
            SizeX = sizeX;
            SizeY = sizeY;
            Title = title;
            Guid = guid;
        }

        public GraphFileNode(Vector2 position, Vector2 size, string title, Guid guid) {
            PositionX = position.x;
            PositionY = position.y;
            SizeX = size.x;
            SizeY = size.y;
            Title = title;
            Guid = guid;
        }
    }
}