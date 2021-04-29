using PathTracer.Geometry;
using System;
using System.Linq;

namespace PathTracer.Bodies
{
    public class Parallelepiped : SolidObject, IRotate
    {
        private readonly Plane[] faces = new Plane[6];
        Vector3 Size;

        public Parallelepiped(Vector3 position, Vector3 size, string tag, Material material)
        {
            this.Position = position;
            this.Size = size;
            Material = material;
            Tag = tag;

            faces[0] = new Plane(new Vector3(Position.X, Position.Y, Position.Z + size.Z / 2), new Vector3(size.X, size.Y, 0), material) { Owner = this };
            faces[1] = new Plane(new Vector3(Position.X, Position.Y, Position.Z - size.Z / 2), new Vector3(size.X, size.Y, 0), material) { Owner = this };
            faces[2] = new Plane(new Vector3(Position.X, Position.Y + size.Y / 2, Position.Z), new Vector3(size.X, size.Z, 0), material) { Owner = this };
            faces[3] = new Plane(new Vector3(Position.X, Position.Y - size.Y / 2, Position.Z), new Vector3(size.X, size.Z, 0), material) { Owner = this };
            faces[4] = new Plane(new Vector3(Position.X + size.X / 2, Position.Y, Position.Z), new Vector3(size.Z, size.Y, 0), material) { Owner = this };
            faces[5] = new Plane(new Vector3(Position.X - size.X / 2, Position.Y, Position.Z), new Vector3(size.Z, size.Y, 0), material) { Owner = this };
            faces[0].Rotate(Vector3.Right, (float)Math.PI / 2);
            faces[1].Rotate(Vector3.Right, -(float)Math.PI / 2);
            faces[2].Rotate(Vector3.Up, 0);
            faces[3].Rotate(Vector3.Up, (float)Math.PI);
            faces[4].Rotate(Vector3.Up, (float)Math.PI / 2);
            faces[5].Rotate(Vector3.Up, -(float)Math.PI / 2);
            faces[4].Rotate(Vector3.Right, (float)Math.PI / 2);
            faces[5].Rotate(Vector3.Right, -(float)Math.PI / 2);
        }

        public override void FlipNormal()
        {
            foreach (var face in faces)
                face.FlipNormal();
        }

        public override RaycastResult IntersectByRay(Ray ray, float maxLength)
        {
            var results = new RaycastResult[6];
            for (var i = 0; i != 6; i++)
                results[i] = faces[i].IntersectByRay(ray,maxLength);

            var intersections = results.Where(t => t != null);
            if (intersections.Count() == 0)
                return null;

            var raycast = intersections.OrderBy(t => (t.Position - ray.Origin).Length).First();
            raycast.Body = this;
            return raycast;
        }
        public void Rotate(Vector3 axis, float angle)
        {
            foreach (var face in faces)
                face.Rotate(axis, angle);
        }
    }
}
