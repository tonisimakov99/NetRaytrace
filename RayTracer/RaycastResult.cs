using RayTracer.Bodies;
using RayTracer.Geometry;

namespace RayTracer
{
    public class RaycastResult
    {
        public RaycastResult(SolidObject body, Vector3 normal, Vector3 position, Ray ray)
        {
            Body = body;
            Normal = normal;
            Position = position;
            Ray = ray;
        }
        public SolidObject Body { get; internal set; }
        public Vector3 Normal { get; internal set; }
        public Vector3 Position { get; internal set; }
        public Ray Ray { get; set; }
    }
}