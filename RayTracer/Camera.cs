using RayTracer.Bodies;
using RayTracer.Geometry;
using RayTracer.Lights;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RayTracer
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
            var delta = options.ClipPlaneSize / options.Resolution;
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
                                (i - (options.Resolution.X / 2)) * delta.X,
                                options.DistanceToClipPlane,
                                (j - (options.Resolution.Y / 2)) * delta.Y*-1)
                                .RotateByQuaternion(rotation));
                            matrix[i, j] = RayTrace(ray,options.RefractBounces, options.ReflectBounces, options.MaxLength);
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

        private Color RayTrace(Ray ray, int refractBounces, int reflectBounces, float maxLength)
        {
            var result = scene.Ray(ray);
            if (result == null)
                return Color.White;
       
            var color = CalculateLightness(result, maxLength);

            if (reflectBounces < 0)
                return color;

            var reflect = ray.Direction.Reflect(result.Normal);
            var r = 1/result.Body.Material.RefractionIndex;
            var cos1 = -1 * result.Normal * ray.Direction.Normalized;
            var sin2 = r * (float)Math.Sqrt(1 - cos1 * cos1);
            var cos2 = (float)Math.Sqrt(1 - sin2 * sin2);
            var refractIn = r * ray.Direction.Normalized + (r * cos1 - cos2)*result.Normal;

            if (result.Body.Material.Reflect != null)
                color += result.Body.Material.Reflect * RayTrace(new Ray(result.Position, reflect),refractBounces, reflectBounces - 1, maxLength);

            if (result.Body.Material.Refract != null && refractBounces >= 0)
                color += result.Body.Material.Refract * Refract(result.Body, new Ray(result.Position, refractIn), refractBounces - 1, reflectBounces - 1, maxLength);
            return color;
        }

        private Color Refract(SolidObject body, Ray refractIn, int refractBounces, int reflectBounces, float maxLength)
        {
            var result = scene.Ray(refractIn, maxLength);
            if (result == null)
                return Color.White;
            if (result.Body != body)
                return Color.Black;
            else
            {
                var r = result.Body.Material.RefractionIndex;
                var cos1 = (float)Math.Max(1, -1 * result.Normal * refractIn.Direction.Normalized);
    
                var sin2 = (float)Math.Max(1,r * (float)Math.Sqrt(1 - cos1 * cos1));

                var cos2 = (float)Math.Max(1, (float)Math.Sqrt(1 - sin2 * sin2));
                var refractOut = r * refractIn.Direction.Normalized + (r * cos1 - cos2) * result.Normal;
                return RayTrace(new Ray(result.Position, refractOut), refractBounces, reflectBounces - 1, maxLength);
            }
        }

        private Color CalculateLightness(RaycastResult intersection, float maxLength)
        {
            var color = Color.Black;
            foreach (var light in scene.Lights)
            {
                if(light is AmbientLight)
                {
                    var ambient = light as AmbientLight;
                    color += new Color(
                        (int)((ambient.DiffuseColor.R + ambient.SpecularColor.R) / 2),
                         (int)((ambient.DiffuseColor.G + ambient.SpecularColor.G) / 2),
                          (int)((ambient.DiffuseColor.B + ambient.SpecularColor.B) / 2)) * intersection.Body.Material.AmbientReflection;
                }

                if (light is PointLight)
                {
                    var raycastToLight = (((PointLight)light).Position - intersection.Position).Normalized;
                    var result = scene.Ray(new Ray(intersection.Position, raycastToLight), maxLength);
                    if (result != null)
                        continue;

                    var reflected = raycastToLight.Reflect(intersection.Normal).Normalized;
                    var toViewer = (intersection.Position - position).Normalized;
                
                    var material = intersection.Body.Material;

                    color += material.Diffuse * (raycastToLight * intersection.Normal) * light.DiffuseColor;

                    color += material.Specularity * (float)Math.Pow(Math.Max(0,reflected * toViewer), material.Shininess) * light.SpecularColor;
                }
            }
            return color;
        }
    }
}
