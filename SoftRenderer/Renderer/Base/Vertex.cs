using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace SoftRenderer
{
    public class Vertex
    {
        public Vector4 position;
        //public Vector3 color;
        public Color color;
    }

    public class VSOutput
    {
        public Vector4 position { get; set; }
        public Vector3 normal { get; set; }
        public Vector2 uv { get; set; }
        public Vector4 color { get; set; }

        //Other Attributes
    }
}
