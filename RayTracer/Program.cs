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
            var floor = new Parallelepiped(new Vector3(0, 0, -10), new Vector3(50, 50, 20), "floor", new Material(new Color(0, 0, 0), 0.9f, new Color(0, 255, 0), new Color(255, 255, 255), new Color(50,50,50)));
            var cube = new Parallelepiped(new Vector3(-2, 0, 0.5f), new Vector3(1, 1, 1), "cube", new Material(new Color(0,0, 0), 0.9f, new Color(0, 0, 255), new Color(255, 255, 255), new Color(0, 0, 0)));
            var mirror = new Plane(new Vector3(0, 6, 0), new Vector3(20, 20, 0), new Material(new Color(128, 128, 128), 0.9f, new Color(200, 200, 200), new Color(10, 10, 10), new Color(190, 190, 190)));
            mirror.FlipNormal();
            var sphere = new Sphere(new Vector3(0, 0, 1f), 1, "ss", new Material(new Color(128, 128, 128), 5f, new Color(255, 0, 0), new Color(255, 255, 255), new Color(0, 0, 0)));
            //scene.Objects.Add(mirror);
            scene.Objects.Add(floor);
            scene.Objects.Add(cube);
            scene.Objects.Add(sphere);
            var light = new PointLight(new Vector3(-6, -6, 40), Color.White, Color.White);
            var ambient = new AmbientLight(new Color(20, 20, 20), new Color(20, 20, 20));
            scene.Lights.Add(light);
            scene.Lights.Add(ambient);

            var camera = new Camera(new Vector3(-1, -12f, 5f), scene, Quaternion.Get(Vector3.Right, (float)(Math.PI / 180) * -15));

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
