using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SoftRenderer
{
    public class Utils
    {
        public static void DrawCoordinateXY(Graphics g)
        {
            RectangleF rect = g.VisibleClipBounds;
            int width = (int)rect.Width;
            int height= (int)rect.Height;
            int penWidth = 3;
            Pen xPen = new Pen(Brushes.Red, penWidth);
            Pen yPen = new Pen(Brushes.Blue, penWidth);

            int originX = 0;
            int originY = 0;
            Point origin = new Point(originX, originY);
            Point xEnd = new Point(width, 0);
            Point yEnd = new Point(0, height);

            g.DrawLine(xPen, origin, xEnd);
            g.DrawLine(yPen, origin, yEnd);

            Font font= new Font("Arial", 16);

            SolidBrush brush = new SolidBrush(Color.Black);

            g.DrawString("O", font,brush, originX, originY);
            g.DrawString("X", font,brush, xEnd.X-50, xEnd.Y);
            g.DrawString("Y", font,brush, yEnd.X, yEnd.Y-50);
            
        }

        public static void DrawCenterRect(Graphics g)
        {
            //画屏幕中央, 边界1/4大小的方形
            RectangleF fullRect = g.VisibleClipBounds;
            int fullWidth = (int)fullRect.Width;
            int fullHeight = (int)fullRect.Height;
            int width = fullWidth/2;
            int height = fullHeight/2;
            int topLeftX = fullWidth/4;
            int topLeftY = fullHeight/4;
            Rectangle rect = new Rectangle(topLeftX, topLeftY, width, height);
            int penWidth = 2;
            Pen pen = new Pen(Brushes.Black, penWidth);
            g.DrawRectangle(pen, rect);
        }

       
        public static int Clamp(int num, int min, int max)
        {
            return Math.Max(min, Math.Min(max, num));
        }

        public static Color VectorToColor(System.Numerics.Vector4 color)
        {
            int a = Clamp((int)Math.Round(color.W * 255), 0,255);
            int r = Clamp((int)Math.Round(color.X * 255), 0,255);
            int g = Clamp((int)Math.Round(color.Y * 255), 0,255);
            int b = Clamp((int)Math.Round(color.Z * 255), 0,255);
            Color col = Color.FromArgb(a, r, g, b);
            return col;
        }
        
    }
}
