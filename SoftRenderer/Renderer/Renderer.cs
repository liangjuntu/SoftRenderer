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
    public class Renderer
    {
        Graphics graphics;
        Context context;
        Rasterizer rasterizer;
        
        public Renderer(Graphics g)
        {
            Init(g);
        }

        //初始化
        void Init(Graphics g)
        {
            InitByGraphics(g);
            //初始化Frame Buffer
            RectangleF rect = graphics.VisibleClipBounds;
            Size frameSize = new Size((int)rect.Width, (int)rect.Height);
            context = new Context(frameSize);
            rasterizer = new Rasterizer(context);
        }

        void InitByGraphics(Graphics g)
        {
            Debug.Assert(g != null);
            graphics = g;
        }

        public void OnResize(Graphics g)
        {
            InitByGraphics(g);
            RectangleF rect = graphics.VisibleClipBounds;
            Size frameSize = new Size((int)rect.Width, (int)rect.Height);
            //Debug.Print(string.Format("new size:{0}", frameSize));
            context.OnResize(frameSize);
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
            rasterizer.BresenhamDrawLine(new Vector2(0, 0), new Vector2(width-1, height-1), Color.Red);
            //绿色线从中间的左下到右上
            rasterizer.BresenhamDrawLine(new Vector2(width/4, height*3/4), new Vector2(width*3/4, height/4), Color.Green);
            //蓝色线从左上角附近到中间1/4处
            rasterizer.BresenhamDrawLine(new Vector2(20, 20), new Vector2(width/2, height/4), Color.Blue);
            //橘黄色从左边中间到右边中间
            rasterizer.BresenhamDrawLine(new Vector2(0, height/2), new Vector2(width-1, height/2), Color.Bisque);

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
                Vector2 p1 = new Vector2(x1, y1);
                Vector2 p2 = new Vector2(x2, y2);
                rasterizer.BresenhamDrawLine(p1, p2, color);
            }

            //画上去
            graphics.DrawImage(context.frameBuffer, 0, 0);

            Font font = new Font("Arial", 16);

            SolidBrush brush = new SolidBrush(Color.Black);

            graphics.DrawString("Test_BresenhamDrawLine", font, brush, 20, 20);
        }

        public void Test_CohenSutherlandLineClip()
        {
            ClearFrameBuffer();
            Size frameSize = context.frameSize;
            int width = frameSize.Width;
            int height = frameSize.Height;

            //区域
            Vector2 min = new Vector2(width / 4, height / 4 );
            Vector2 max = new Vector2(width / 4 + width/2, height / 4 + height/2);
            bool accept = false;
            Vector2 p0, p1;

            //红色线从左上角到右下角
            p0 = new Vector2(0, 0);
            p1 = new Vector2(width, height);
            accept = rasterizer.CohenSutherlandLineClip(ref p0, ref p1, min, max);
            if (accept)
            {
                rasterizer.BresenhamDrawLine(p0, p1, Color.Red);
            }

            //绿色线从左边中间到中心
            p0 = new Vector2(0, height / 2);
            p1 = new Vector2(width/2, height / 2 );
            accept = rasterizer.CohenSutherlandLineClip(ref p0, ref p1, min, max);
            if(accept)
            {
                rasterizer.BresenhamDrawLine(p0,p1, Color.Green);
            }

            //蓝色线左上附近到右下附近
            p0 = new Vector2(width/3 , 0);
            p1 = new Vector2(width*2/3 , height -1);
            accept = rasterizer.CohenSutherlandLineClip(ref p0, ref p1, min, max);
            if (accept)
            {
                rasterizer.BresenhamDrawLine(p0, p1, Color.Blue);
            }

            //橘黄色线在左上到做下，应该会被剔除掉
            p0 = new Vector2(width / 8 , 0);
            p1 = new Vector2(width /8 , height -1);
            accept = rasterizer.CohenSutherlandLineClip(ref p0, ref p1, min, max);
            if (accept)
            {
                rasterizer.BresenhamDrawLine(p0, p1, Color.Bisque);
            }

            //随机测试
            Random random = new Random();
            const int TEST_COUNT = 10;
            int maxX = width;
            int maxY = height;
            for (int i = 0; i < TEST_COUNT; i++)
            {
                Color color = Color.Black;
                int x1 = random.Next(0, maxX);
                int y1 = random.Next(0, maxY);
                int x2 = random.Next(0, maxX);
                int y2 = random.Next(0, maxY);
                Vector2 rp1 = new Vector2(x1, y1);
                Vector2 rp2 = new Vector2(x2, y2);
                accept = rasterizer.CohenSutherlandLineClip(ref rp1, ref rp2, min, max);
                if (accept)
                {
                    rasterizer.BresenhamDrawLine(rp1, rp2, color);
                }
            }

            //画上去
            graphics.DrawImage(context.frameBuffer, 0, 0);

            Font font = new Font("Arial", 16);

            SolidBrush brush = new SolidBrush(Color.Black);

            graphics.DrawString("Test_CohenSutherlandLineClip", font, brush, 20, 20);

        }


    }
}
