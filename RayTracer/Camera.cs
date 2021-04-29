using PathTracer.Geometry;
using PathTracer.Lights;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PathTracer
{
    public class Camera
    {
        private readonly Vector3 position;
        private readonly Quaternion rotation;
        private readonly Scene scene;

        public Camera(Vector3 position, Scene scene, Quaternion rotation)
        {
            this.position = position;
            this.scene = scene;
            this.rotation = rotation;
        }
        public Color[,] Render(RenderOptions options)
        {
            var aspectRatio = (float)options.Resolution.X/(float)options.Resolution.Y;
            var matrix = new Color[options.Resolution.X, options.Resolution.Y];
            Enumerable.Range(0, options.ThreadCount).AsParallel().ForAll(
                (z)=>
                {
                    var start = options.Resolution.X / options.ThreadCount * z;
                    var end = options.Resolution.X / options.ThreadCount * (z + 1);
                    if (z + 1 == options.ThreadCount)
                        end = options.Resolution.X;

                    for (var i = start; i != end; i++)
                    {
                        for (var j = 0; j != options.Resolution.Y; j++)
                        {
                            var ray = new Ray(position, new Vector3(
                                (i - (options.Resolution.X / 2)) * options.ViewCoef,
                                1,
                                (j - (options.Resolution.Y / 2)) * options.ViewCoef*-1)
                                .RotateByQuaternion(rotation));
                            matrix[i, j] = RayTrace(ray, options.Bounces, options.MaxLength);
                        }
                    }
                });

            return matrix;
        }
        //public Color[,] Render(RenderOptions options)
        //{
        //    var matrix = new Color[options.Resolution.X, options.Resolution.Y];

        //    for (var i = 0; i != options.Resolution.X; i++)
        //    {
        //        for (var j = 0; j != options.Resolution.Y; j++)
        //        {
        //            var ray = new Ray()
        //            {
        //                Direction = new Vector3((i - (options.Resolution.X / 2)) * -0.01f, -4, (j - (options.Resolution.Y / 2)) * -0.01f),
        //                Origin = position
        //            };
        //            matrix[i, j] = RayTrace(ray, 0, 1000);
        //        }
        //    }
        //    return matrix;
        //}

        //private Color RayTrace(Ray ray, int maxBounces, float maxLength)
        //{
        //    var raycastResult = scene.Ray(ray);
        //    if (raycastResult != null)
        //        return raycastResult.Obj.Material.Color;
        //    return Color.Black;
        //}
        //private Color RayTrace(Ray ray, int maxBounces, float maxLength)
        //{
        //    var raycastResult = scene.Ray(ray);
        //    if (raycastResult != null)
        //        return new Color((int)((raycastResult.Triangle.Normal.X+1)*127), (int)((raycastResult.Triangle.Normal.Y+1) * 127), (int)((raycastResult.Triangle.Normal.Z+1) * 127));
        //    return Color.Black;
        //}
        //private Color RayTrace(Ray ray, int maxBounces, float maxLength)
        //{
        //    var bla = 2500f;
        //    var raycastResult = scene.Ray(ray);
        //    if (raycastResult != null)
        //        return new Color(255 - (int)((bla / 255f) * (raycastResult.Position - ray.Origin).Length), 255 - (int)((bla / 255f) * (raycastResult.Position - ray.Origin).Length), 255 - (int)((bla / 255f) * (raycastResult.Position - ray.Origin).Length));
        //    return Color.Black;
        //}

        private Color RayTrace(Ray ray, int bounces, float maxLength)
        {
            var result = scene.Ray(ray);
            if (result == null)
                return Color.Black;

            var color = result.Body.Material.Color*CalculateLightness(result, maxLength);

            if (bounces < 0 || result.Body.Material.Reflection <= 0)
                return color;

            var reflect = ray.Direction.Reflect(result.Normal);

            return color * (1 - result.Body.Material.Reflection) + RayTrace(new Ray(result.Position,reflect), bounces - 1, maxLength) * result.Body.Material.Reflection;
        }

        private Color CalculateLightness(RaycastResult intersection, float maxLength)
        {
            var color = Color.Black;
            foreach (var light in scene.Lights)
            {
                if (light is AmbientLight)
                    color += light.Color;

                if (light is PointLight)
                {
                    var raycastToLight = ((PointLight)light).Position - intersection.Position;
                    var result = scene.Ray(new Ray(intersection.Position, raycastToLight), maxLength);
                    if (result != null)
                        continue;

                    color += light.Color*Math.Abs(intersection.Normal.Normalized*raycastToLight.Normalized);

                    //if (intersection.Body.Material.Specularity != -1)
                    //{
                    //    var reflect = raycastToLight.Reflect(intersection.Normal);
                    //    if (reflect * intersection.Ray.Direction > 0)
                    //        color += light.Color * (float)Math.Pow(reflect * intersection.Ray.Direction / (intersection.Normal.Length * intersection.Ray.Direction.Length), intersection.Body.Material.Specularity);
                    //}
                }
            }
            return color;
        }
    }
}
