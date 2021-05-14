namespace RayTracer
{
    public class Material
    {
        public Material(Color specularity, float shininess, Color diffuse, Color ambientReflection, Color reflect)
        {
            Specularity = specularity;
            Shininess = shininess;
            Diffuse = diffuse;
            AmbientReflection = ambientReflection;
            Reflect = reflect;
        }

        public Color Specularity { get; set; }
        public float Shininess { get; set; }
        public Color Diffuse { get; set; }
        public Color AmbientReflection { get; set; }
        public Color Reflect { get; set; }
    }
}