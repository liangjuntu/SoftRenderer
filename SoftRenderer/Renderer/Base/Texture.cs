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
        //Trilinear
    }

    public class Texture
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        Bitmap bitmap;
        Vector4[,] colors;

        private Texture()
        {
        }

        void CacheColors()
        {
            colors = new Vector4[Width, Height];
            for(int x = 0; x < Width; ++x)
            {
                for(int y = 0; y < Height; y++)
                {
                    Color p = bitmap.GetPixel(x, y);
                    Vector4 c = Utils.ColorToVector(p);
                    colors[x, y] = c;
                }
            }
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
            texture.CacheColors();
            return texture;
        }

        
        Vector4 PointSample(Vector2 texcoord)
        {
            //u和v的范围是[0,width]和[0,height]
            //而像素索引x和y的范围分别是[0,width-1]和[0,Height-1]
            //对于位置为(x,y)的像素对应的应该是中心，即(u,v)=(x+0.5, y+0.5)的颜色值
            //也就是说对于点采样：uv范围[i,i+1)应该索引i的颜色
            float u = texcoord.X * Width;
            float v = texcoord.Y * Height;
            int uIdx = (int)Math.Floor(u);
            int vIdx = (int)Math.Round(v);

            //纹理坐标左上角为原点,http://blog.sina.com.cn/s/blog_80cc3d870101lntk.html
            uIdx = Utils.Clamp(uIdx, 0, Width - 1);
            vIdx = Utils.Clamp(vIdx, 0, Height - 1);
            Vector4 col = colors[uIdx, vIdx];
            return col;
            //Color col = bitmap.GetPixel(uIdx, vIdx);
            //return new Vector4(col.R / 255f, col.G / 255f, col.B / 255f, col.A / 255f);
        }

        Vector4 BilinearFiltering(Vector2 texcoord)
        {
            //u和v的范围是[0,width]和[0,height]
            //而像素索引x和y的范围分别是[0,width-1]和[0,Height-1]
            //对于位置为(x,y)的像素对应的应该是中心，即(u,v)=(x+0.5, y+0.5)的颜色值
            //也就是说对于双线性采样:[i+0.5, i+1.5)之间的uv在i和i+1之间线性插值
            float u = texcoord.X * Width;
            float v = texcoord.Y * Height;
            float uIdx = (float)Math.Floor(u - 0.5f);
            float vIdx = (float)Math.Floor(v - 0.5f);

            float du = u - (uIdx + 0.5f);
            float dv = v - (vIdx + 0.5f);

            int left = (int)uIdx;
            int right = left + 1;
            int top = (int)vIdx;
            int bottom = top + 1;

            left = Utils.Clamp(left, 0, Width - 1);
            right = Utils.Clamp(right, 0, Width - 1);
            top = Utils.Clamp(top, 0, Height - 1);
            bottom = Utils.Clamp(bottom, 0, Height - 1);

            /*
            Vector4 lt = Utils.ColorToVector(bitmap.GetPixel(left, top));
            Vector4 rt = Utils.ColorToVector(bitmap.GetPixel(right, top));
            Vector4 lb = Utils.ColorToVector(bitmap.GetPixel(left, bottom));
            Vector4 rb = Utils.ColorToVector(bitmap.GetPixel(right, bottom));
            */
            Vector4 lt = colors[left, top];
            Vector4 rt = colors[right, top];
            Vector4 lb = colors[left, bottom];
            Vector4 rb = colors[right, bottom];

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
