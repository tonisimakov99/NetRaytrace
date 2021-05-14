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
            var blue = new Material(Color.Black, 0, Color.Blue, Color.White, new Color(30, 30, 30), null, 0);
            var green = new Material(Color.Black, 0, Color.Green, Color.White, new Color(30, 30, 30), null, 0);
            var red = new Material(Color.Black, 0, Color.Red, Color.White, new Color(30, 30, 30), null, 0);
            var glass = new Material(new Color(128,128,128), 4f, Color.Black, Color.White, new Color(60, 60, 60), new Color(180, 180, 180), 1.33f);
            var scene = new Scene();
            var floor = new Parallelepiped(new Vector3(0, 0, -10), new Vector3(50, 50, 20), "floor", green);
            var cube = new Parallelepiped(new Vector3(-2, 0, 0.5f), new Vector3(1, 1, 1), "cube", blue);
            var glasss = new Parallelepiped(new Vector3(1, 0, 1), new Vector3(3, 0.4f, 2), "spar", glass);
            //var sphere = new Sphere(new Vector3(0, 0, 1f), 1, "ss", glass);
            var par = new Parallelepiped(new Vector3(0, 6, 1), new Vector3(3, 1, 2), "par", red);
            scene.Objects.Add(floor);
            scene.Objects.Add(cube);
            scene.Objects.Add(glasss);
            // scene.Objects.Add(sphere);
            scene.Objects.Add(par);
            var light = new PointLight(new Vector3(-6, -6, 40), Color.White, Color.White);
            var ambient = new AmbientLight(new Color(20, 20, 20), new Color(20, 20, 20));
            scene.Lights.Add(light);
            scene.Lights.Add(ambient);

            var camera = new Camera(new Vector3(-1, -12f, 3.5f), scene, Quaternion.Get(Vector3.Right, (float)(Math.PI / 180) * -10));

            var matrix = camera.Render(new RenderOptions(new Vector2Int(1920*2,1080*2),32,6, 12, 2000,new Vector2(1.92f/2,1.08f/2),1));
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
