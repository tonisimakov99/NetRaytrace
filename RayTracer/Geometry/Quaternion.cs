using System;

namespace RayTracer.Geometry
{
    public class Quaternion
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public Vector3 V => new Vector3(X, Y, Z);
        public float Norm => X * X + Y * Y + Z * Z + W * W;
        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        public Quaternion Conjugate => new Quaternion(-1 * this.V, W);
        public Quaternion Inverse => Conjugate * (1 / Norm);
        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Quaternion(Vector3 v, float w)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = w;
        }
        public Quaternion()
        {

        }
        public static Quaternion Get(Vector3 axis,float angle)=> new Quaternion(axis.Normalized * (float)Math.Sin(angle/2), (float)Math.Cos(angle/2));
        public static Quaternion operator +(Quaternion one, Quaternion other) => new Quaternion(one.X + other.X, one.Y + other.Y, one.Z + other.Z, one.W - other.W);
        public static Quaternion operator -(Quaternion one, Quaternion other) => new Quaternion(one.X - other.X, one.Y - other.Y, one.Z - other.Z, one.W - other.W);
        public static Quaternion operator *(Quaternion one, float scalar) => new Quaternion(one.X * scalar, one.Y * scalar, one.Z * scalar, one.W * scalar);
        public static Quaternion operator *(Quaternion one, Quaternion other) => new Quaternion(Vector3.CrossProduct(one.V, other.V) + (other.V * one.W) + (other.W * one.V), one.W * other.W - one.V * other.V);
    }
}
