using PathTracer.Geometry;

namespace PathTracer.Lights
{
    public class PointLight : Light
    {
        public PointLight(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }
        public Vector3 Position { get; set; }
    }
}