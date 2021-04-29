namespace RayTracer.Geometry
{
    public class Ray
    {
        public Ray(Vector3 Origin, Vector3 Direction)
        {
            this.Origin = Origin;
            this.Direction = Direction;
        }
        public Vector3 Origin { get; set; }
        private Vector3 direction;
        public Vector3 Direction
        {
            get => direction;
            set
            {
                direction = value.Normalized;
            }
        }
    }
}
