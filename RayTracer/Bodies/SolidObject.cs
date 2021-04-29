using PathTracer.Geometry;

namespace PathTracer.Bodies
{
    public abstract class SolidObject : SceneObject
    {
        public SolidObject Owner;
        public abstract RaycastResult IntersectByRay(Ray ray,float maxLength);
        public abstract void FlipNormal();
    }
}
