using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Lights
{
    public class AmbientLight:Light
    {
        public AmbientLight(Color color)
        {
            Color = color;
        }
    }
}
