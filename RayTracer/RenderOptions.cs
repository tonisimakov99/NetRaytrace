using RayTracer.Geometry;
using System.Dynamic;

namespace RayTracer
{
    public class RenderOptions
    {
        public RenderOptions(Vector2Int resolution, int reflectBounces, int refractBounces, int threadCount, float maxLength, Vector2 clipPlaneSize, float distanceToClipPlane)
        {
            Resolution = resolution;
            ReflectBounces = reflectBounces;
            RefractBounces = refractBounces;
            ThreadCount = threadCount;
            MaxLength = maxLength;
            ClipPlaneSize = clipPlaneSize;
            DistanceToClipPlane = distanceToClipPlane;
        }

        public Vector2Int Resolution { get; set; }
        public int ReflectBounces { get; set; }
        public int RefractBounces { get; set; }
        public int ThreadCount { get; set; }
        public float MaxLength { get; set; }
        public Vector2 ClipPlaneSize { get; set; }
        public float  DistanceToClipPlane { get; set; }
    }
}