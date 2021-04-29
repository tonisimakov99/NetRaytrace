namespace RayTracer
{
    public class Material
    {
        public Material(float specularity, float shininess, Color diffuse, float ambientReflection)
        {
            Specularity = specularity;
            Shininess = shininess;
            Diffuse = diffuse;
            AmbientReflection = ambientReflection;
        }

        public float Specularity { get; set; }
        public float Shininess { get; set; }
        public Color Diffuse { get; set; }
        public float AmbientReflection { get; set; }
    }
}