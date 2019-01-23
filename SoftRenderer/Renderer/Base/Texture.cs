using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace SoftRenderer
{
    public enum TextureFilterMode
    {
        Point,
        Bilinear,
        Trilinear
    }

    public class Texture
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        Bitmap bitmap;

        private Texture()
        {
        }

        public static Texture FromFile(string filePath, int width = -1, int height = -1)
        {
            
            System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);
            Texture texture = new Texture();
            if(width <= 0|| height <= 0)
            {
                texture.Width = img.Width;
                texture.Height = img.Height;
            }
            else
            {
                texture.Width = width;
                texture.Height = height;
            }

            texture.bitmap = new Bitmap(img, texture.Width, texture.Height);
            return texture;
        }

        
        Color PointSample(Vector2 texcoord)
        {
            //纹理坐标左上角为原点,http://blog.sina.com.cn/s/blog_80cc3d870101lntk.html
            int uIndex = (int)(texcoord.X*Width);
            int vIndex = (int)(texcoord.Y*Height);
            uIndex = Utils.Clamp(uIndex, 0, Width - 1);
            vIndex = Utils.Clamp(vIndex, 0, Height - 1);
            Color col = bitmap.GetPixel(uIndex, vIndex);
            return col;
        }

        public Color RawTex2D(Vector2 texcoord, TextureFilterMode textureFilterMode)
        {
            if(textureFilterMode == TextureFilterMode.Point)
            {
                return PointSample(texcoord);
            }
            //TODO
            return PointSample(texcoord);
        }


        public Vector4 Tex2D(Vector2 texcoord, TextureFilterMode textureFilterMode)
        {
            Color col = RawTex2D(texcoord, textureFilterMode);
            return new Vector4(col.R/255f, col.G/255f, col.B/255f, col.A/255f);
        }
        
    }
}
