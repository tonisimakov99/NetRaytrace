using RayTracer.Geometry;

namespace RayTracer.Bodies
{
    public abstract class SolidObject : SceneObject
    {
        public SolidObject Owner;
        public abstract RaycastResult IntersectByRay(Ray ray,float maxLength);
        public abstract void FlipNormal();
    }
}
