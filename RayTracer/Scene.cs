using RayTracer.Geometry;
using System.Collections.Generic;
using System.Linq;
using RayTracer.Lights;
using RayTracer.Bodies;

namespace RayTracer
{
    public class Scene
    {
        public List<SolidObject> Objects;
        public List<Light> Lights;
        
        public Scene()
        {
            Objects = new List<SolidObject>();
            Lights = new List<Light>();
        }
        
        public RaycastResult Ray(Vector3 origin, Vector3 direction, float maxLength)
        {
            var ray = new Ray(origin, direction);
            var results = new List<RaycastResult>();
            foreach (var _object in Objects)
                results.Add(_object.IntersectByRay(ray, maxLength));

            var nonNullResult = results.Where(t => t != null);
            if (nonNullResult.Count() == 0)
                return null;

            var raycast = nonNullResult.OrderBy(t => (t.Position - ray.Origin).Length).First();
            return raycast;
        }
        public RaycastResult Ray(Ray ray, float maxLength)
        {
            return Ray(ray.Origin, ray.Direction,maxLength);
        }
        public RaycastResult Ray(Ray ray)
        {
            return Ray(ray.Origin, ray.Direction, float.MaxValue);
        }
    }
}
