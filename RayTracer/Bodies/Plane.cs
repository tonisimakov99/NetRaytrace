using PathTracer.Geometry;

namespace PathTracer.Bodies
{
    public class Plane:SolidObject,IRotate,IMove
    {
        public Vector3 Normal => first.Normal;

        private Triangle first;
        private Triangle second;
        public Plane(Vector3 position, Vector3 size, Material material)
        {
            Position = position;
            Material = material;
            var firstPoint = (position + new Vector3(-size.X / 2, 0, size.Y / 2));
            var secondPoint = (position + new Vector3(size.X / 2, 0, size.Y / 2));
            var thirdPoint = (position + new Vector3(size.X / 2, 0, -size.Y / 2));
            var forthPoint =(position + new Vector3(-size.X / 2, 0, -size.Y / 2));

            first = new Triangle(firstPoint, secondPoint, thirdPoint) { Material = material, Owner = this };
            second = new Triangle(thirdPoint, forthPoint, firstPoint) { Material = material, Owner = this };
        }
        public override RaycastResult IntersectByRay(Ray ray, float maxLength)
        {
            var firstResult = first.IntersectByRay(ray,maxLength);
            if (firstResult != null)
            {
                firstResult.Body = this;
                return firstResult;
            }

            var secondResult = second.IntersectByRay(ray,maxLength);
            if (secondResult != null)
            {
                secondResult.Body = this;
                return secondResult;
            }  

            return null;
        }

        public override string ToString()
        {
            return $"(1 = {first},2 ={second})";
        }

        public void Move(Vector3 delta)
        {
            first.Move(delta);
            second.Move(delta);
        }

        public override void FlipNormal()
        {
            first.FlipNormal();
            second.FlipNormal();
        }

        public void Rotate(Vector3 axis, float angle)
        {
            first.Move(-1 * Position);
            second.Move(-1 * Position);

            first.Rotate(axis, angle);
            second.Rotate(axis, angle);

            first.Move(Position);
            second.Move(Position);
        }
    }
}
