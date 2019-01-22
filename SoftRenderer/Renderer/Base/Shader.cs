using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftRenderer
{
    public class Shader
    {
        public Matrix4x4 modelMatrix;
        public Matrix4x4 viewMatrix;
        public Matrix4x4 projectionMatrix;
        public Matrix4x4 MVP;

        public VSOutput VertShader(Vertex v)
        {
            VSOutput OUT = new VSOutput();
            return OUT;
        }

        public bool FragShader()
        {
            return true;
        }
    }
}
