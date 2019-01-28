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

        
        Vector4 PointSample(Vector2 texcoord)
        {
            //纹理坐标左上角为原点,http://blog.sina.com.cn/s/blog_80cc3d870101lntk.html
            int uIndex = (int)Math.Round(texcoord.X*Width, MidpointRounding.AwayFromZero);
            int vIndex = (int)Math.Round(texcoord.Y*Height, MidpointRounding.AwayFromZero);
            uIndex = Utils.Clamp(uIndex, 0, Width - 1);
            vIndex = Utils.Clamp(vIndex, 0, Height - 1);
            Color col = bitmap.GetPixel(uIndex, vIndex);
            return new Vector4(col.R / 255f, col.G / 255f, col.B / 255f, col.A / 255f);
        }

        Vector4 BilinearFiltering(Vector2 texcoord)
        {
            float u = texcoord.X * Width;
            float v = texcoord.Y * Height;
            int left = (int)Math.Floor(u);
            int top = (int)Math.Floor(v);
            left = Utils.Clamp(left, 0, Width - 1);
            top = Utils.Clamp(top, 0, Height - 1);

            float du = Utils.Clamp(u - left, 0, 1);
            float dv = Utils.Clamp(v - top, 0, 1);

            int right = Utils.Clamp(left+ 1, 0, Width-1);
            int bottom = Utils.Clamp(top + 1, 0, Height-1);
            Vector4 lt = Utils.ColorToVector(bitmap.GetPixel(left, top));
            Vector4 rt = Utils.ColorToVector(bitmap.GetPixel(right, top));
            Vector4 lb = Utils.ColorToVector(bitmap.GetPixel(left, bottom));
            Vector4 rb = Utils.ColorToVector(bitmap.GetPixel(right, bottom));

            float wlt = (1 - du) * (1 - dv);
            float wrt = (du) * (1 - dv);
            float wlb = (1-du) * (dv);
            float wrb = (du) * (dv);
            Vector4 col = wlt * lt + wrt * rt + wlb * lb + wrb * rb;
            return col;
        }

        public Vector4 Tex2D(Vector2 texcoord, TextureFilterMode textureFilterMode)
        {
            if(textureFilterMode == TextureFilterMode.Point)
            {
                return PointSample(texcoord);
            }
            return BilinearFiltering(texcoord);
            //TODO trilinear
        }

    }
}
