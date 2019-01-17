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
        Context context;
        Rasterizer rasterizer;
        
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
            Size frameSize = new Size((int)rect.Width, (int)rect.Height);
            context = new Context(frameSize);
            rasterizer = new Rasterizer(context);
        }

        void ClearFrameBuffer()
        {
            context.frameGraphics.Clear(context.ambientColor);
        }

        

        public void Test_BresenhamDrawLine()
        {
            ClearFrameBuffer();
            Size frameSize = context.frameSize;
            int width = frameSize.Width;
            int height = frameSize.Height;

            //红色线从左上角到右下角
            rasterizer.BresenhamDrawLine(new PointF(0, 0), new PointF(width-1, height-1), Color.Red);
            //绿色线从中间的左下到右上
            rasterizer.BresenhamDrawLine(new PointF(width/4-1, height*3/4-1), new PointF(width*3/4-1, height/4-1), Color.Green);
            //蓝色线从左上角附近到中间1/4处
            rasterizer.BresenhamDrawLine(new PointF(20, 20), new PointF(width/2-1, height/4-1), Color.Blue);
            //橘黄色从左边中间到右边中间
            rasterizer.BresenhamDrawLine(new PointF(0, height/2-1), new PointF(width-1, height/2-1), Color.Bisque);

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
                rasterizer.BresenhamDrawLine(p1, p2, color);
            }

            //画上去
            graphics.DrawImage(context.frameBuffer, 0, 0);

            Font font = new Font("Arial", 16);

            SolidBrush brush = new SolidBrush(Color.Black);

            graphics.DrawString("Test_BresenhamDrawLine", font, brush, 20, 20);
        }

        
    }
}
