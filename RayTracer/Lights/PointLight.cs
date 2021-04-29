using RayTracer.Geometry;

namespace RayTracer.Lights
{
    public class PointLight : Light
    {
        public PointLight(Vector3 position, Color specularColor,Color diffuseColor)
        {
            Position = position;
            SpecularColor = specularColor;
            DiffuseColor = diffuseColor;
        }
        public Vector3 Position { get; set; }
    }
}