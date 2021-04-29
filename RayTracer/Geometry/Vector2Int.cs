namespace RayTracer.Geometry
{
    public class Vector2Int
    {
        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public static implicit operator Vector2(Vector2Int vector) => new Vector2(vector.X,vector.Y);
    }
}
