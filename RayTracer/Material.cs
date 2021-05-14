namespace RayTracer
{
    public class Material
    {
        public Material(Color specularity, float shininess, Color diffuse, Color ambientReflection, Color reflect, Color refract, float refractionIndex)
        {
            Specularity = specularity;
            Shininess = shininess;
            Diffuse = diffuse;
            AmbientReflection = ambientReflection;
            Reflect = reflect;
            Refract = refract;
            RefractionIndex = refractionIndex;
        }
        public Material(Color specularity, float shininess, Color diffuse, Color ambientReflection)
        {
            Specularity = specularity;
            Shininess = shininess;
            Diffuse = diffuse;
            AmbientReflection = ambientReflection;
        }
        public Material(Color specularity, float shininess, Color diffuse, Color ambientReflection, Color reflect)
        {
            Specularity = specularity;
            Shininess = shininess;
            Diffuse = diffuse;
            AmbientReflection = ambientReflection;
            Reflect = reflect;
        }
        public Material(Color specularity, float shininess, Color diffuse, Color ambientReflection, Color refract, float refractionIndex)
        {
            Specularity = specularity;
            Shininess = shininess;
            Diffuse = diffuse;
            AmbientReflection = ambientReflection;
            Refract = refract;
            RefractionIndex = refractionIndex;
        }

        public Color Specularity { get; set; }
        public float Shininess { get; set; }
        public Color Diffuse { get; set; }
        public Color AmbientReflection { get; set; }
        public Color Reflect { get; set; }
        public Color Refract { get; set; }
        public float RefractionIndex { get; set; }
    }
}