using System;
using System.Globalization;

namespace RayTracer.Geometry
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vector3 Normalized  => this / this.Length; 
        public float Length  => (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        public static Vector3 Up => new Vector3(0, 0, 1);
        public static Vector3 Right => new Vector3(1, 0, 0);
        public static Vector3 Forward => new Vector3(0, 1, 0);
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3 Reflect(Vector3 normal)
        {
            float factor = -2F * normal * this;
            return new Vector3(
                factor * normal.X + this.X,
                factor * normal.Y + this.Y,
                factor * normal.Z + this.Z);
        }
        public Vector3 RotateAround(Vector3 axis, float angle)
        {
            var q = new Quaternion(axis.Normalized * (float)Math.Sin(angle / 2), (float)Math.Cos(angle / 2));
            var v = new Quaternion(this, 0);
            return (q*(v*q.Inverse)).V;
        }
        public Vector3 RotateByQuaternion(Quaternion quaternion)
        {
            var v = new Quaternion(this, 0);
            return (quaternion * (v * quaternion.Inverse)).V;
        }

        public override string ToString()
        {
            return $"({X.ToString(CultureInfo.InvariantCulture)},{Y.ToString(CultureInfo.InvariantCulture)},{Z.ToString(CultureInfo.InvariantCulture)})";
        }
        public float AngleFrom(Vector3 other) => (float)Math.Acos((this * other) / (this.Length*other.Length));

        public static Vector3 CrossProduct(Vector3 a, Vector3 b) => new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

        public static Vector3 operator +(Vector3 one, Vector3 other) => new Vector3(one.X + other.X, one.Y + other.Y, one.Z + other.Z);

        public static float operator *(Vector3 one, Vector3 other) => one.X * other.X + one.Y * other.Y + one.Z * other.Z;

        public static Vector3 operator -(Vector3 one, Vector3 other) => new Vector3(one.X - other.X, one.Y - other.Y, one.Z - other.Z);

        public static Vector3 operator /(Vector3 one, Vector3 other) => new Vector3(one.X / other.X, one.Y / other.Y, one.Z / other.Z);

        public static Vector3 operator *(Vector3 one, float other) => new Vector3(one.X * other, one.Y * other, one.Z * other);
        public static Vector3 operator *(float one, Vector3 other) => new Vector3(one * other.X, one * other.Y, one * other.Z);

        public static Vector3 operator /(Vector3 one, float other) => new Vector3(one.X / other, one.Y / other, one.Z / other);

        public static Vector3 operator *(Vector3 one, int other) => new Vector3(one.X * other, one.Y * other, one.Z * other);

        public static Vector3 operator *(int other, Vector3 one) => new Vector3(one.X * other, one.Y * other, one.Z * other);
    }
}
