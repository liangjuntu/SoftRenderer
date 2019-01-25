using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace SoftRenderer
{
    public class DrawInfo
    {
        //public Vector3 cameraPosition = new Vector3(0, 1, -5f);
        public Vector3 cameraPosition = new Vector3(0, 0, -5f);
        public Vector3 cameraEulerAngles = new Vector3(0, 0, 0);
        public float CameraNear = 1f;
        public float CameraFar = 10f;
        public float CameraFov = 60f;
        public Color CameraClearColor = Color.Black;
        public TextureFilterMode textureFilterMode = TextureFilterMode.Point;
        public DrawMode drawMode = DrawMode.Normal;
        //public DrawMode drawMode = DrawMode.Wireframe;
        //public DrawMode drawMode = DrawMode.Depth;
        public CullMode cullMode = CullMode.None;
        //public CullMode cullMode = CullMode.Back;
        public Winding winding = Winding.CounterClockwise;
    }
}
