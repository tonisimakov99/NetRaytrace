using RayTracer.Geometry;
using System.Dynamic;

namespace RayTracer
{
    public class RenderOptions
    {
        public RenderOptions(Vector2Int resolution, int bounces, int threadCount, float maxLength, Vector2 clipPlaneSize, float distanceToClipPlane)
        {
            Resolution = resolution;
            Bounces = bounces;
            ThreadCount = threadCount;
            MaxLength = maxLength;
            ClipPlaneSize = clipPlaneSize;
            DistanceToClipPlane = distanceToClipPlane;
        }

        public Vector2Int Resolution { get; set; }
        public int Bounces { get; set; }
        public int ThreadCount { get; set; }
        public float MaxLength { get; set; }
        public Vector2 ClipPlaneSize { get; set; }
        public float  DistanceToClipPlane { get; set; }
    }
}