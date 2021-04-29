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
            var floor = new Parallelepiped(new Vector3(0, 0, -10), new Vector3(50, 50, 20), "floor", new Material(0.5f, Color.Green, 0, 0.1f));
            var leftMirror = new Plane(new Vector3(0, 5, 1), new Vector3(10, 10, 0), new Material(0.5f, new Color(192,192,192), 0, 0.95f));
            var rightMirror = new Plane(new Vector3(0,-5, 1), new Vector3(10, 10, 0), new Material(0.5f, new Color(192, 192, 192), 0, 0.95f));
            var sphere = new Sphere(new Vector3(0, 0, 1), 1, "ss", new Material(0.5f, Color.Red, 0, 0.1f));
            scene.Objects.Add(floor);
            scene.Objects.Add(sphere);
            scene.Objects.Add(leftMirror);
            scene.Objects.Add(rightMirror);
            var light = new PointLight(new Vector3(0, -7, 40), Color.White);
            var ambient = new AmbientLight(new Color(60, 60, 60));
            scene.Lights.Add(light);
            scene.Lights.Add(ambient);

            var camera = new Camera(new Vector3(0, -4.8f, 5f), scene, Quaternion.Get(Vector3.Right, (float)(Math.PI / 180) * -15));

            var matrix = camera.Render(new RenderOptions(new Vector2Int(1920,1080),32, 12, 2000,new Vector2(1.92f/2,1.08f/2),1));
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
