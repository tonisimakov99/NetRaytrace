using RayTracer.Geometry;
using System;

namespace RayTracer.Bodies
{
    public class Triangle:SolidObject,IRotate,IMove
    {
        private bool isLeftNormal = true;
        public Vector3 Normal
        {
            get
            {
                var x10 = second.X - first.X;
                var y10 = second.Y - first.Y;
                var z10 = second.Z - first.Z;
                var x20 = third.X - first.X;
                var y20 = third.Y - first.Y;
                var z20 = third.Z - first.Z;

                var result = new Vector3(
                     y10 * z20 - z10 * y20,
                     -(x10 * z20 - z10 * x20),
                     x10 * y20 - y10 * x20
                ).Normalized;

                if (isLeftNormal)
                    result = -1 * result;
                return result;
            }
        }
        private Vector3 first;
        private Vector3 second;
        private Vector3 third;

        public Triangle(Vector3 first, Vector3 second, Vector3 third)
        {
            this.first = first;
            this.second = second;
            this.third = third;
            Position = new Vector3(
                (first.X + second.X + third.X) / 3,
                (first.Y + second.Y + third.Y) / 3,
                (first.Z + second.Z + third.Z) / 3);
        }
        public override RaycastResult IntersectByRay(Ray ray, float maxLength)
        {
            if ((this.Position - ray.Origin).Length > maxLength)
                return null;

            if (Math.Abs(ray.Direction * Normal) < float.Epsilon)
                return null;

            var d = Normal * first;

            if (Math.Abs(Normal * ray.Origin - d) <= 0)
                return null;

            var t = (d - (ray.Origin * Normal)) / (ray.Direction * Normal);
           
            if (t < 0.0001)
                return null;

            var point = new Vector3(ray.Origin.X + t * ray.Direction.X, ray.Origin.Y + t * ray.Direction.Y, ray.Origin.Z + t * ray.Direction.Z);

            if (!PointInside(point))
                return null;

            return new RaycastResult(this, Normal, point,ray);
        }
        private bool PointInside(Vector3 point)
        {
            var normal = Normal;
            if (isLeftNormal)
                normal = -1 * normal;
            if (Vector3.CrossProduct(second - first, point - first) * normal < float.Epsilon)
                return false;
            if (Vector3.CrossProduct(point - first, third - first) * normal < float.Epsilon)
                return false;
            if (Vector3.CrossProduct(second - point, third- point) * normal < float.Epsilon)
                return false;
            return true;
        }

        public  void Move(Vector3 delta)
        {
            first += delta;
            second += delta;
            third += delta;
            Position += delta;
        }
        public override string ToString()
        {
            return $"({first},{second},{third})";
        }

        public override void FlipNormal()
        {
            isLeftNormal = !isLeftNormal;
        }

        public void Rotate(Vector3 axis, float angle)
        {
            var q = Quaternion.Get(axis, angle);
            first = first.RotateByQuaternion(q);
            second = second.RotateByQuaternion(q);
            third = third.RotateByQuaternion(q);
            Position = new Vector3(
                (first.X + second.X + third.X) / 3,
                (first.Y + second.Y + third.Y) / 3,
                (first.Z + second.Z + third.Z) / 3);
        }
    }
}
