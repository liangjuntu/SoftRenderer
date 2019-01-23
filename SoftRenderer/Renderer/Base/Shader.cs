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

        public TextureFilterMode textureFilterMode { get;  set; }
       

        public void SetViewProjectionMatrix(Matrix4x4 v, Matrix4x4 p)
        {
            viewMatrix = v;
            projectionMatrix = p;
            vpMatrix = v * p;
        }

    }

    public class Shader
    {
        public ShaderContext shaderContext { get; private set; }
        Matrix4x4 modelMatrix;
        Matrix4x4 mvpMatrix;
        public Matrix4x4 ModelMatrix { get{ return modelMatrix; }}
        public Matrix4x4 MVP { get{ return mvpMatrix; }}

        public Texture texture { get; set; }

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
            OUT.position = Vector4.Transform( v.position, MVP);
            //OUT.normal = Vector4.Transform(v.normal, MVP);
            OUT.texcoord = v.texcoord;
            OUT.color = v.color;
            return OUT;
        }

        public PSOutput FragShader(VSOutput v)
        {
            PSOutput OUT = new PSOutput();
            Vector4 col = v.color;
            if(texture != null)
            {
                col = texture.Tex2D(v.texcoord, shaderContext.textureFilterMode);
                OUT.color = col;
            }
            return OUT;
        }
    }
}
