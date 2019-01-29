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
            if(v.Color >= 0 && v.Color < obj.Colors.Count)
            {
                vertex.color = obj.Colors[v.Color];
            } else
            {
                vertex.color = Vector4.One;
            }
            return vertex;
        }
    }

    public class VSOutput
    {
        public Vector4 position { get; set; }
        public Vector3 normalWorld { get; set; }
        public Vector2 texcoord { get; set; }
        public Vector4 color { get; set; }

        public Vector3 posScreen { get; set; } //viewport坐标

        //Other Attributes
        public void Clone(VSOutput other)
        {
            this.position = other.position;
            this.normalWorld = other.normalWorld;
            this.texcoord = other.texcoord;
            this.color = other.color;
            this.posScreen = other.posScreen;
        }

        public static VSOutput Lerp(VSOutput A, VSOutput B, float amount)
        {
            VSOutput C = new VSOutput();
            C.position = Vector4.Lerp(A.position, B.position, amount);
            C.normalWorld = Vector3.Lerp(A.normalWorld, B.normalWorld, amount);
            C.texcoord = Vector2.Lerp(A.texcoord, B.texcoord, amount);
            C.color = Vector4.Lerp(A.color, B.color, amount);
            C.posScreen = Vector3.Lerp(A.posScreen, B.posScreen, amount);
            return C;

        }
    }

    public class PSOutput
    {
        public bool isDiscard { get; set; }
        public Vector4 color { get; set; }

        public PSOutput()
        {
            isDiscard = false;
            color = new Vector4(0, 0, 0, 1);
        }
    }
}
