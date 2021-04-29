using RayTracer.Geometry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Geometry
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 Normalized => this / this.Length;
        public float Length => (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Forward => new Vector2(0, 1);
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2 Reflect(Vector2 normal)
        {
            float factor = -2F * normal * this;
            return new Vector2(
                factor * normal.X + this.X,
                factor * normal.Y + this.Y);
        }
        public Vector2 RotateAround(Vector3 axis, float angle)
        {
            var q = new Quaternion(axis.Normalized * (float)Math.Sin(angle / 2), (float)Math.Cos(angle / 2));
            var v = new Quaternion(new Vector3(this.X,this.Y,0), 0);
            var result = (q * (v * q.Inverse)).V;
            return new Vector2(result.X,result.Y);
        }
        public Vector2 RotateByQuaternion(Quaternion quaternion)
        {
            var v = new Quaternion(new Vector3(this.X, this.Y, 0), 0);
            var result = (quaternion * (v * quaternion.Inverse)).V;
            return new Vector2(result.X, result.Y);
        }

        public override string ToString()
        {
            return $"({X.ToString(CultureInfo.InvariantCulture)},{Y.ToString(CultureInfo.InvariantCulture)})";
        }
        public float AngleFrom(Vector2 other) => (float)Math.Acos((this * other) / (this.Length * other.Length));

        public static Vector2 operator +(Vector2 one, Vector2 other) => new Vector2(one.X + other.X, one.Y + other.Y);

        public static float operator *(Vector2 one, Vector2 other) => one.X * other.X + one.Y * other.Y;

        public static Vector2 operator -(Vector2 one, Vector2 other) => new Vector2(one.X - other.X, one.Y - other.Y);

        public static Vector2 operator /(Vector2 one, Vector2 other) => new Vector2(one.X / other.X, one.Y / other.Y);

        public static Vector2 operator *(Vector2 one, float other) => new Vector2(one.X * other, one.Y * other);
        public static Vector2 operator *(float one, Vector2 other) => new Vector2(one * other.X, one * other.Y);

        public static Vector2 operator /(Vector2 one, float other) => new Vector2(one.X / other, one.Y / other);

        public static Vector2 operator *(Vector2 one, int other) => new Vector2(one.X * other, one.Y * other);

        public static Vector2 operator *(int other, Vector2 one) => new Vector2(one.X * other, one.Y * other);
    }
}
