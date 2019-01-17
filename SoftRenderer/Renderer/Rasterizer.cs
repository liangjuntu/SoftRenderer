using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace SoftRenderer
{
    public class Rasterizer
    {
        Context context;
        public Rasterizer(Context c)
        {
            context = c;
        }

        //Bresenham画线算法
        public void BresenhamDrawLine(PointF p1, PointF p2, Color color)
        {
            Size frameSize = context.frameSize;
            Bitmap frameBuffer = context.frameBuffer;
            //BresenHam输入的是x和y是像素索引，范围是0<=x<width；0<=y<height
            Debug.Assert(0 <= p1.X && p1.X < frameSize.Width);
            Debug.Assert(0 <= p1.Y && p1.Y < frameSize.Height);
            Debug.Assert(0 <= p2.X && p2.X < frameSize.Width);
            Debug.Assert(0 <= p2.Y && p2.Y < frameSize.Height);

            //由于是像素，所以把输入转成整数
            int x1 = (int)p1.X;
            int y1 = (int)p1.Y;
            int x2 = (int)p2.X;
            int y2 = (int)p2.Y;

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
        int ComputeOutCode(PointF p, PointF min, PointF max)
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
        bool CohenSutherlandLineClip(ref PointF p0, ref PointF p1, PointF min, PointF max)
        {

            float x0 = p0.X;
            float y0 = p0.Y;
            float x1 = p1.X;
            float y1 = p1.X;

            int outcode0 = ComputeOutCode(p0, min, max);
            int outcode1 = ComputeOutCode(p1, min, max);
            bool accept = false;
            
            while(true)
            {
                if( (outcode0|outcode1) == 0) //相或为0，接受并且退出循环
                {
                    accept = true;
                    break;
                } else if((outcode0&outcode1) != 0) // 相与为1，拒绝且退出循环
                {
                    break;
                } else
                {
                    float x = 0, y = 0;
                    //找出在界外的点
                    int outcodeOut = outcode0 != 0 ? outcode0 : outcode1;
                    // 找出和边界相交的点
                    // 使用点斜式 y = y0 + slope * (x - x0), x = x0 + (1 / slope) * (y - y0)
                    if( (outcodeOut&TOP) != 0)  // point is above the clip rectangle
                    {
                        x = x0 + (x1 - x0)*(max.Y - y0) / (y1 - y0);
                        y = max.Y;
                    } else if((outcodeOut&BOTTOM) != 0) // point is below the clip rectangle
                    { 
                        x = x0 + (x1 - x0)*(min.Y - y0) / (y1 - y0);
                        y = min.Y;
                    } else if((outcodeOut&RIGHT) != 0)
                    {
                        y = y0 + (y1 - y0) * (max.X - x0) / (x1 - x0);
                        x = max.X;
                    } else if ((outcodeOut&LEFT) != 0)
                    {
                        y = y0 + (y1 - y0) * (min.X - x0) / (x1 - x0);
                        x = min.X;
                    }

                    // Now we move outside point to intersection point to clip
                    // 为什么继续循环，两个端点都有可能在外面
                    if(outcodeOut == outcode0)
                    {
                        p0.X = x;
                        x0 = x;
                        p0.Y = y;
                        y0 = y;
                        outcode0 = ComputeOutCode(p0, min, max);
                    } else
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
    }
}
