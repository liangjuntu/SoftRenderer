using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using Games;

namespace SoftRenderer
{
    public class Vertex
    {
        public Vector4 position;
        public Vector4 color;
        public Vector4 normal;
        public Vector2 texcoord;

        public static Vertex FromWavefrontVertex(WavefrontObject obj, WavefrontVertex v)
        {
            Vertex vertex = new Vertex();
            vertex.position = new Vector4(obj.Positions[v.Position], 1f);
            vertex.normal = new Vector4(obj.Normals[v.Normal], 0f);
            vertex.texcoord = obj.Texcoords[v.Texcoord];
            vertex.color = new Vector4(vertex.texcoord.X, vertex.texcoord.Y, 0, 1);
            //vertex.color = new Vector4(1, 1, 1, 1);
            return vertex;
        }
    }

    public class VSOutput
    {
        public Vector4 position { get; set; }
        public Vector3 normal { get; set; }
        public Vector2 texcoord { get; set; }
        public Vector4 color { get; set; }

        //Other Attributes
    }
}
