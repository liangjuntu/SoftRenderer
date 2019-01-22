using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftRenderer
{

    public class ShaderContext
    {
        Matrix4x4 viewMatrix;
        Matrix4x4 projectionMatrix;
        Matrix4x4 vpMatrix;
        public Matrix4x4 ViewMatrix { get { return viewMatrix; } }
        public Matrix4x4 ProjectionMatrix { get { return projectionMatrix; } }
        public Matrix4x4 VP { get { return vpMatrix; } }

        public void SetViewProjectionMatrix(Matrix4x4 v, Matrix4x4 p)
        {
            viewMatrix = v;
            projectionMatrix = p;
            vpMatrix = v * p;
        }
    }

    public class Shader
    {
        ShaderContext shaderContext;
        Matrix4x4 modelMatrix;
        Matrix4x4 mvpMatrix;
        public Matrix4x4 ModelMatrix { get{ return modelMatrix; }}
        public Matrix4x4 MVP { get{ return mvpMatrix; }}

        public Shader( ShaderContext sc )
        {
            shaderContext = sc;
        }

        public void SetModelMatrix(Matrix4x4 m)
        {
            modelMatrix = m;
            mvpMatrix = m * shaderContext.VP;
        }

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
