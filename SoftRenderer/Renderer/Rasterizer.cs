using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Numerics;

namespace SoftRenderer
{
    public class Rasterizer
    {
        Context context;
        public Rasterizer(Context c)
        {
            context = c;
        }


        void DrawPixel(int x, int y, Color color)
        {
            context.frameBuffer.SetPixel(x, y, color);
        }

        //Bresenham画线算法
        public void BresenhamDrawLine(Vector2 p1, Vector2 p2, Color color)
        {
            Size frameSize = context.frameSize;
            Bitmap frameBuffer = context.frameBuffer;
            Debug.Assert(0 <= p1.X && p1.X < frameSize.Width);
            Debug.Assert(0 <= p1.Y && p1.Y < frameSize.Height);
            Debug.Assert(0 <= p2.X && p2.X < frameSize.Width);
            Debug.Assert(0 <= p2.Y && p2.Y < frameSize.Height);

            //由于是像素，所以把输入转成整数
            int x1 = (int)p1.X;
            int y1 = (int)p1.Y;
            int x2 = (int)p2.X;
            int y2 = (int)p2.Y;

            //BresenHam输入的是x和y是像素索引，有效范围是0<=x<width；0<=y<height
            x1 = Math.Max(0, Math.Min(frameSize.Width-1, x1));
            y1 = Math.Max(0, Math.Min(frameSize.Height-1, y1));
            x2 = Math.Max(0, Math.Min(frameSize.Width-1, x2));
            y2 = Math.Max(0, Math.Min(frameSize.Height-1, y2));
           

            //端点做开始
            int x = x1;
            int y = y1;

            int dx = x2 - x1;
            int dy = y2 - y1;
            //步进方向
            int stepX = 1;
            int stepY = 1;
            if (dx < 0)
            {
                stepX = -1;
                dx = Math.Abs(dx);
            }
            if (dy < 0)
            {
                stepY = -1;
                dy = Math.Abs(dy);
            }

            int dx2 = dx * 2;
            int dy2 = dy * 2;

            //沿着最长的那个轴前进
            if (dx > dy)
            {
                //斜率小于1,沿X轴
                int p = dy2 - dx;
                for (int i = 0; i < dx; ++i)
                {
                    frameBuffer.SetPixel(x, y, color);
                    if (p >= 0)
                    {
                        y += stepY;
                        x += stepX;
                        p += dy2 - dx2;
                    }
                    else
                    {
                        //下一个y不许变
                        x += stepX;
                        p += dy2;
                    }
                }
            }
            else
            {
                //沿着y轴，公式的x和y反过来
                int p = dx2 - dy;
                for (int i = 0; i < dy; ++i)
                {
                    frameBuffer.SetPixel(x, y, color);
                    if (p >= 0)
                    {
                        x += stepX;
                        y += stepY;
                        p += dx2 - dy2;
                    }
                    else
                    {
                        //x不变
                        y += stepY;
                        p += dx2;
                    }
                }
            }
            //TODO 最后的端点应该也要画上把？
            frameBuffer.SetPixel(x2, y2, color);
        }


        const int INSIDE = 0; //0000
        const int LEFT = 1;//0001
        const int RIGHT = 2;//0010
        const int BOTTOM = 4;//0100
        const int TOP = 8;//1000
        // Compute the bit code for a point (x, y) using the clip rectangle
        // bounded diagonally by (xmin, ymin), and (xmax, ymax)
        int ComputeOutCode(Vector2 p, Vector2 min, Vector2 max)
        {
            int code = INSIDE;// initialised as being inside of clip window

            if (p.X < min.X) // to the left of clip window
            {
                code |= LEFT;
            }
            else if (p.X > max.X) // to the right of clip window
            {
                code |= RIGHT;
            }

            if (p.Y < min.Y)// below the clip window
            {
                code |= BOTTOM;
            }
            else if (p.Y > max.Y)// above the clip window
            {
                code |= TOP;
            }


            return code;
        }

        //Cohen–Sutherland线段裁剪算法
        //参考维基https://en.wikipedia.org/wiki/Cohen%E2%80%93Sutherland_algorithm
        // Cohen–Sutherland clipping algorithm clips a line from
        // P0 = (x0, y0) to P1 = (x1, y1) against a rectangle with 
        // diagonal from (xmin, ymin) to (xmax, ymax).
        public bool CohenSutherlandLineClip(ref Vector2 p0, ref Vector2 p1, Vector2 min, Vector2 max)
        {

            float x0 = p0.X;
            float y0 = p0.Y;
            float x1 = p1.X;
            float y1 = p1.Y;

            int outcode0 = ComputeOutCode(p0, min, max);
            int outcode1 = ComputeOutCode(p1, min, max);
            bool accept = false;

            while (true)
            {
                if ((outcode0 | outcode1) == 0) //相或为0，接受并且退出循环
                {
                    accept = true;
                    break;
                }
                else if ((outcode0 & outcode1) != 0) // 相与为1，拒绝且退出循环
                {
                    break;
                }
                else
                {
                    float x = 0, y = 0;
                    //找出在界外的点
                    int outcodeOut = outcode0 != 0 ? outcode0 : outcode1;
                    // 找出和边界相交的点
                    // 使用点斜式 y = y0 + slope * (x - x0), x = x0 + (1 / slope) * (y - y0)
                    if ((outcodeOut & TOP) != 0)  // point is above the clip rectangle
                    {
                        x = x0 + (x1 - x0) * (max.Y - y0) / (y1 - y0);
                        y = max.Y;
                    }
                    else if ((outcodeOut & BOTTOM) != 0) // point is below the clip rectangle
                    {
                        x = x0 + (x1 - x0) * (min.Y - y0) / (y1 - y0);
                        y = min.Y;
                    }
                    else if ((outcodeOut & RIGHT) != 0)
                    {
                        y = y0 + (y1 - y0) * (max.X - x0) / (x1 - x0);
                        x = max.X;
                    }
                    else if ((outcodeOut & LEFT) != 0)
                    {
                        y = y0 + (y1 - y0) * (min.X - x0) / (x1 - x0);
                        x = min.X;
                    }

                    // Now we move outside point to intersection point to clip
                    // 为什么继续循环，两个端点都有可能在外面
                    if (outcodeOut == outcode0)
                    {
                        p0.X = x;
                        x0 = x;
                        p0.Y = y;
                        y0 = y;
                        outcode0 = ComputeOutCode(p0, min, max);
                    }
                    else
                    {
                        p1.X = x;
                        x1 = x;
                        p1.Y = y;
                        y1 = y;
                        outcode1 = ComputeOutCode(p1, min, max);
                    }
                }
            }

            return accept;

        }

        //默认的min和max分别是(0,0)和(width,height)
        public bool CohenSutherlandLineClip(ref Vector2 p0, ref Vector2 p1)
        {
            Vector2 min = new Vector2(0, 0);
            Vector2 max = new Vector2(context.frameSize.Width, context.frameSize.Height);
            return CohenSutherlandLineClip(ref p0, ref p1, min, max);
        }

        float EdgeFunction(Vector2 a, Vector2 b, Vector2 c)
        {
            return (c.X - a.X) * (b.Y - a.Y) - (c.Y - a.Y) * (b.X - a.X);
        }

        Vector3 BarycentricCoordinates(Vector2 p, Vector2 v0, Vector2 v1, Vector2 v2)
        {
            //如果三点共线，area = 0
            float area = EdgeFunction(v0, v1, v2);
            if( Math.Abs(area) < 0.000001)
            {
                return Vector3.One * (-1);
            }
            float w0 = EdgeFunction(v1, v2, p);
            float w1 = EdgeFunction(v2, v0, p);
            float w2 = EdgeFunction(v0, v1, p);
            w0 /= area;
            w1 /= area;
            w2 /= area;
            return new Vector3(w0, w1, w2);
        }

        //做透视除法和转到ViewPort变换
        public VSOutput PerspectiveDivideAndViewportTransformVertex(VSOutput v)
        {
            const float FLT_EPSILON = 1.192092896e-07F; // smallest such that 1.0+FLT_EPSILON != 1.0
            if (v.position.W <= FLT_EPSILON)
            {
                return v;
            }

            Vector4 position = v.position;

            float fInvW = 1 / position.W;
            position = new Vector4(position.X * fInvW, position.Y * fInvW, position.Z * fInvW, 1) ;
            //TODO viewport transform

            int width = context.frameBuffer.Width;
            int height = context.frameBuffer.Height;
            float x = (position.X + 1) * 0.5f;
            float y = (1 - position.Y) * 0.5f;

            v.position = new Vector4(x*width, y*height, position.Z , fInvW);

            //TODO 改成01的Color
            v.color *= fInvW;
            v.uv *= fInvW;
            v.normal *= fInvW;

            return v;
        }

        public void BarycentricRasterizeTriangle(VSOutput v0, VSOutput v1, VSOutput v2)
        {
            int width = context.frameSize.Width;
            int height = context.frameSize.Height;
            //求包围盒
            float xmin = Math.Min(Math.Min(v0.position.X, v1.position.X), v2.position.X);
            float xmax = Math.Max(Math.Max(v0.position.X, v1.position.X), v2.position.X);
            float ymin = Math.Min(Math.Min(v0.position.Y, v1.position.Y), v2.position.Y);
            float ymax = Math.Max(Math.Max(v0.position.Y, v1.position.Y), v2.position.Y);
            //像素坐标包围盒
            int x0 = Utils.Clamp((int)xmin, 0, width-1);
            int y0 = Utils.Clamp((int)ymin, 0, height-1);
            int x1 = Utils.Clamp((int)xmax, 0, width-1);
            int y1 = Utils.Clamp((int)ymax, 0, height-1);

            Vector2 xy0 = new Vector2(v0.position.X, v0.position.Y);
            Vector2 xy1 = new Vector2(v1.position.X, v1.position.Y);
            Vector2 xy2 = new Vector2(v2.position.X, v2.position.Y);

            for( int y = y0; y <= y1; ++y )
            {
                for( int x = x0; x <= x1; ++x )
                {
                    //加0.5取像素的中间位置的坐标
                    Vector2 pixel = new Vector2(x + 0.5f, y + 0.5f);
                    Vector3 w = BarycentricCoordinates(pixel, xy0, xy1, xy2);
                    float w0 = w.X; float w1 = w.Y; float w2 = w.Z;
                    if(!(w0 >=0 && w1 >=0 && w2 >=0))
                    {
                        //像素点不在三角形内
                        continue;
                    }

                    //深度测试
                    float depth = w0 * v0.position.Z + w1 * v1.position.Z + w2 * v2.position.Z;
                    if(context.depthBuffer[x,y] < depth)
                    {
                        continue;
                    }
                    context.depthBuffer[x, y] = depth;
                    
                    //插值UV等顶点属性
                    float fInvW = w0 * v0.position.W + w1 * v1.position.W + w2 * v2.position.W;
                    //由于position.W是1/W = 1/-Z_Eye
                    float Z_Eye = 1f / fInvW;
                    Vector2 uv = w0 * v0.uv + w1 * v1.uv + w2 * v2.uv;
                    uv *= Z_Eye;

                    Vector4 color = w0 * v0.color + w1 * v1.color + w2 * v2.color;
                    color *= Z_Eye;
                    Color col = Utils.VectorToColor(color);

                    DrawPixel(x, y, col);
                }

            }
      


        }
    }
}
