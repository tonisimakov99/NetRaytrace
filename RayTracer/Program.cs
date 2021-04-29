using RayTracer.Bodies;
using RayTracer.Geometry;
using RayTracer.Lights;
using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            var scene = new Scene();
            var floor = new Parallelepiped(new Vector3(0, 0, -10), new Vector3(50, 50, 20), "floor", new Material(128f,128f,new Color(10,128,10),10f));
            var mirror = new Plane(new Vector3(0, -4, 0), new Vector3(20, 20, 0), new Material(128, 128, new Color(10, 10, 10), 0));
            var sphere = new Sphere(new Vector3(0, 0, 1), 1, "ss", new Material(128f, 128f, new Color(128, 10, 10), 10f));
            scene.Objects.Add(mirror);
            scene.Objects.Add(floor);
            scene.Objects.Add(sphere);
            var light = new PointLight(new Vector3(-7, -7, 40), Color.White,Color.White);
            var ambient = new AmbientLight(Color.White, Color.White);
            scene.Lights.Add(light);
            scene.Lights.Add(ambient);

            var camera = new Camera(new Vector3(0, -12f, 5f), scene, Quaternion.Get(Vector3.Right, (float)(Math.PI / 180) * -15));

            var matrix = camera.Render(new RenderOptions(new Vector2Int(1920,1080),16, 12, 2000,new Vector2(1.92f/2,1.08f/2),1));
            var bitmap = new Bitmap(matrix.GetLength(0), matrix.GetLength(1));
            for (var i = 0; i != bitmap.Width; i++)
            {
                for (var j = 0; j != bitmap.Height; j++)
                {
                    bitmap.SetPixel(i, j, System.Drawing.Color.FromArgb(matrix[i, j].R, matrix[i, j].G, matrix[i, j].B));
                }
            }
            bitmap.Save($"file.jpg", ImageFormat.Jpeg);
            bitmap.Dispose();
        }
    }
}
