using RayTracer.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Bodies
{
    public class Sphere : SolidObject
    {
        private bool isLeftNormal = true;
        public Sphere(Vector3 position, float radius, string tag ,Material material)
        {
            Position = position;
            Radius = radius;
            Tag = tag;
            Material = material;
        }
        public float Radius { get; }

        public override void FlipNormal()
        {
            isLeftNormal = !isLeftNormal;
        }

        public override RaycastResult IntersectByRay(Ray ray, float maxLength)
        {
            if ((Position - ray.Origin).Length > maxLength)
                return null;
            var a = ray.Direction * ray.Direction;
            var b = 2 * (ray.Origin-Position)*ray.Direction;
            var c = (Position - ray.Origin) * (Position -ray.Origin) - Radius * Radius;
            var d = b * b - 4 * a * c;
            if (d < 0)
                return null;
            else
            {
                var ts = new[] { (-b + (float)Math.Sqrt(d)) / (2 * a), (-b - (float)Math.Sqrt(d)) / (2 * a) };

                if (ts[0] < 0.0001 && ts[1] < 0.0001)
                    return null;

                var t = ts.Where(z => z >= 0).Min();

                var p = new Vector3(t * ray.Direction.X + ray.Origin.X, t * ray.Direction.Y + ray.Origin.Y, t * ray.Direction.Z + ray.Origin.Z);
             
                return new RaycastResult(this, (p-Position), p, ray);
            }
        }
    }
}
