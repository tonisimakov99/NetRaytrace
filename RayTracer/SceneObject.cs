using PathTracer.Geometry;

namespace PathTracer
{
    public class SceneObject
    {
        public Vector3 Position {get;set;}
        public Material Material { get; set; }
        public string Tag { get; set; }

        public override string ToString()
        {
            return $"{Tag}, {Position}";
        }
    }
}