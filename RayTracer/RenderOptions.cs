using PathTracer.Geometry;
using System.Dynamic;

namespace PathTracer
{
    public class RenderOptions
    {
        public RenderOptions(Vector2Int resolution, int bounces, float viewCoef, int threadCount, float maxLength)
        {
            Resolution = resolution;
            Bounces = bounces;
            ViewCoef = viewCoef;
            ThreadCount = threadCount;
            MaxLength = maxLength;
        }

        public Vector2Int Resolution { get; set; }
        public int Bounces { get; set; }
        public float ViewCoef { get; set; }
        public int ThreadCount { get; set; }
        public float MaxLength { get; set; }
    }
}