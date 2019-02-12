using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftRenderer
{

    public enum ShadeMode
    {
        VertextColor,
        Texture,
        Lighting,
        NDotL,
    }

    public class ShaderContext
    {
        Matrix4x4 viewMatrix;
        Matrix4x4 projectionMatrix;
        Matrix4x4 vpMatrix;
        public Matrix4x4 ViewMatrix { get { return viewMatrix; } }
        public Matrix4x4 ProjectionMatrix { get { return projectionMatrix; } }
        public Matrix4x4 VP { get { return vpMatrix; } }

        public TextureFilterMode textureFilterMode { get;  set; }

        public ShadeMode shadeMode = ShadeMode.Texture;
        public Light light { get; private set; }
        public Vector4 ambient = Vector4.Zero;
        public bool invertTexture = false;

        public void SetViewProjectionMatrix(Matrix4x4 v, Matrix4x4 p)
        {
            viewMatrix = v;
            projectionMatrix = p;
            vpMatrix = v * p;
        }

        public void SetLight(Light l)
        {
            light = l;
            light.InitForShader();
        }
    }

    public class Shader
    {
        public ShaderContext shaderContext { get; private set; }
        Matrix4x4 modelMatrix;
        Matrix4x4 mvpMatrix;
        public Matrix4x4 ModelMatrix { get{ return modelMatrix; }}
        public Matrix4x4 MVP { get{ return mvpMatrix; }}
        Matrix4x4 worldToModel;

        public Texture texture { get; set; }

        public Shader( ShaderContext sc )
        {
            shaderContext = sc;
        }

        public void SetModelMatrix(Matrix4x4 m)
        {
            modelMatrix = m;
            mvpMatrix = m * shaderContext.VP;

            Matrix4x4.Invert(m, out worldToModel);
        }


        public VSOutput VertShader(Vertex v)
        {
            VSOutput OUT = new VSOutput();
            OUT.position = Vector4.Transform( v.position, MVP);
            Vector4 normalWorld = Vector4.Transform(v.normal, Matrix4x4.Transpose(worldToModel) );
            OUT.normalWorld = new Vector3(normalWorld.X, normalWorld.Y, normalWorld.Z);
            OUT.texcoord = v.texcoord;
            OUT.color = v.color;
            return OUT;
        }

        public PSOutput FragShader(VSOutput v)
        {
            //invert uv
            if (shaderContext.invertTexture)
            {
                //v.texcoord = new Vector2(1 - v.texcoord.X, 1 - v.texcoord.Y);
                v.texcoord = new Vector2(v.texcoord.X, 1 - v.texcoord.Y);
                //v.texcoord = new Vector2(1 - v.texcoord.X, v.texcoord.Y);
            }

            PSOutput OUT = new PSOutput();
            Vector4 col = v.color;
            if((shaderContext.shadeMode == ShadeMode.Lighting ||
                shaderContext.shadeMode == ShadeMode.NDotL )
                && shaderContext.light != null)
            {
                Vector3 normalWorld = v.normalWorld;
                normalWorld = Vector3.Normalize(normalWorld);
                Vector3 lightDir = shaderContext.light.lightDirForShader;

                //Lambert Lighting
                float NDotL = Vector3.Dot(normalWorld, lightDir);
                NDotL = Utils.Clamp(NDotL, 0, 1);
                if(shaderContext.shadeMode == ShadeMode.Lighting && texture != null)
                {
                    Vector4 tex = texture.Tex2D(v.texcoord, shaderContext.textureFilterMode);
                    col = tex * (NDotL * shaderContext.light.lightColor + shaderContext.ambient);
                }
                else
                {
                    col = new Vector4(NDotL, NDotL, NDotL, 1f);
                }

            } else if(shaderContext.shadeMode == ShadeMode.Texture && texture != null )
            {
                col = texture.Tex2D(v.texcoord, shaderContext.textureFilterMode);
            }
            //col = new Vector4(v.texcoord.X, v.texcoord.Y, 0, 1 );
            OUT.color = col;
            return OUT;
        }
    }
}
