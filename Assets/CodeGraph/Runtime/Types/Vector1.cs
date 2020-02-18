using System;
using UnityEngine;

namespace CodeGraph.Types {
    public struct Vector1 : IEquatable<Vector1> {
        public float x;

        public Vector1(float x) {
            this.x = x;
        }

        public void Set(float x) {
            this.x = x;
        }

        public bool Equals(Vector1 other) {
            return x.Equals(other.x);
        }

        public override bool Equals(object obj) {
            return obj is Vector1 other && Equals(other);
        }

        public override int GetHashCode() {
            return x.GetHashCode();
        }
        
        public static Vector1 operator +(Vector1 a, Vector1 b)
        {
            return new Vector1(a.x + b.x);
        }

        public static Vector1 operator -(Vector1 a, Vector1 b)
        {
            return new Vector1(a.x - b.x);
        }

        public static Vector1 operator *(Vector1 a, Vector1 b)
        {
            return new Vector1(a.x * b.x);
        }

        public static Vector1 operator /(Vector1 a, Vector1 b)
        {
            return new Vector1(a.x / b.x);
        }

        public static Vector1 operator -(Vector1 a)
        {
            return new Vector1(-a.x);
        }

        public static Vector1 operator *(Vector1 a, float d)
        {
            return new Vector1(a.x * d);
        }

        public static Vector1 operator *(float d, Vector1 a)
        {
            return new Vector1(a.x * d);
        }

        public static Vector1 operator /(Vector1 a, float d)
        {
            return new Vector1(a.x / d);
        }

        public static bool operator ==(Vector1 lhs, Vector1 rhs)
        {
            float num1 = lhs.x - rhs.x;
            return (double) num1 * (double) num1 < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector1 lhs, Vector1 rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator Vector1(Vector3 v)
        {
            return new Vector1(v.x);
        }
        
        public static implicit operator Vector1(Vector2 v)
        {
            return new Vector1(v.x);
        }
        public static implicit operator Vector1(float v)
        {
            return new Vector1(v);
        }
        public static implicit operator float(Vector1 v) {
            return v.x;
        }
    }
}