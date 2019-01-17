using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace SoftRenderer
{
    public class Renderer
    {
        Graphics graphics;
        //Frame Buffer相关
        Size frameSize;
        Bitmap frameBuffer; //帧缓冲
        Graphics frameGraphics; //frameBuffer对应的Graphics

        public Color ambientColor = Color.White;
        
        public Renderer(Graphics g)
        {
            Debug.Assert(g != null);
            graphics = g;
            Init();
        }

        //初始化
        void Init()
        {
            //初始化Frame Buffer
            RectangleF rect = graphics.VisibleClipBounds;
            frameSize = new Size((int)rect.Width, (int)rect.Height);
            frameBuffer = new Bitmap(frameSize.Width, frameSize.Height);
            frameGraphics = Graphics.FromImage(frameBuffer);
        }

        void ClearFrameBuffer()
        {
            frameGraphics.Clear(ambientColor);
        }

        void BresenhamDrawLine(Point p1, Point p2, Color color)
        {
            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;
            Debug.Assert(x1 >= 0);
            Debug.Assert(y1 >= 0);
            Debug.Assert(x2 >= 0);
            Debug.Assert(y2 >= 0);
            //端点做开始
            int x = x1;
            int y = y1;

            int dx = x2 - x1;
            int dy = y2 - y1;
            //步进方向
            int stepX = 1;
            int stepY = 1;
            if(dx < 0)
            {
                stepX = -1;
                dx = Math.Abs(dx);
            }
            if(dy < 0)
            {
                stepY = -1;
                dy = Math.Abs(dy);
            }

            int dx2 = dx * 2;
            int dy2 = dy * 2;
            
            //沿着最长的那个轴前进
            if (dx > dy) {
                //斜率小于1,沿X轴
                int p = dy2 - dx;
                for( int i = 0; i < dx; ++i)
                {
                    frameBuffer.SetPixel(x, y, color);
                    if(p >= 0)
                    {
                        y += stepY;
                        x += stepX;
                        p += dy2 - dx2;
                    } else
                    {
                        //下一个y不许变
                        x += stepX;
                        p += dy2;
                    }
                }
            } else
            {
                //沿着y轴，公式的x和y反过来
                int p = dx2 - dy;
                for( int i = 0; i < dy; ++i )
                {
                    frameBuffer.SetPixel(x, y, color);
                    if( p >= 0)
                    {
                        x += stepX;
                        y += stepY;
                        p += dx2 - dy2;
                    } else
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

        public void Test_BresenhamDrawLine()
        {
            ClearFrameBuffer();
            int width = frameSize.Width;
            int height = frameSize.Height;

            //红色线从左上角到右下角
            BresenhamDrawLine(new Point(0, 0), new Point(width-1, height-1), Color.Red);
            //绿色线从中间的左下到右上
            BresenhamDrawLine(new Point(width/4-1, height*3/4-1), new Point(width*3/4-1, height/4-1), Color.Green);
            //蓝色线从左上角附近到中间1/4处
            BresenhamDrawLine(new Point(20, 20), new Point(width/2-1, height/4-1), Color.Blue);

            //随机测试
            Random random = new Random();
            const int TEST_COUNT = 10;
            int maxX = width;
            int maxY = height;
            for(int i = 0; i < TEST_COUNT; i ++)
            {
                Color color = Color.Black;
                int x1 = random.Next(0, maxX);
                int y1 = random.Next(0, maxY);
                int x2 = random.Next(0, maxX);
                int y2 = random.Next(0, maxY);
                Point p1 = new Point(x1, y1);
                Point p2 = new Point(x2, y2);
                BresenhamDrawLine(p1, p2, color);
            }

            //画上去
            graphics.DrawImage(frameBuffer, 0, 0);
        }

    }
}
