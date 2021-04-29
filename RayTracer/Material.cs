namespace RayTracer
{
    public class Material
    {
        public Material(float specularity, Color color, float opacity, float reflection)
        {
            Specularity = specularity;
            Color = color;
            Opacity = opacity;
            Reflection = reflection;
        }

        public float Specularity { get; set; }
        public Color Color { get; set; }
        public float Opacity { get; set; }
        public float Reflection { get; set; }
    }
}