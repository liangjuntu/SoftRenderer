using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

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

        public static float Clamp(float num, float min, float max)
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

        public static Vector4 ColorToVector(Color color)
        {
            float inv = 1 / 255f;
            float w = color.A * inv;
            float x = color.R * inv;
            float y = color.G * inv;
            float z = color.B * inv;
            return new Vector4(x, y, z, w);
        }

        public static float Degree2Radian(float degree)
        {
            return (float)((Math.PI / 180f) * degree);
        }

        public static float Radian2Degree(float radian)
        {
            float degree = (float) (180f / Math.PI) * radian;
            degree %= 360f;
            if(degree < 0)
            {
                degree += 360f;
            }
            return degree;
        }

        //angle是角度/不是弧度
        public static Vector3 QuaternionToEulerAngles(Quaternion q)
        {
            float x = q.X; float y = q.Y; float z = q.Z; float w = q.W;
            float a, b, r;

            const float Epsilon = 0.0009765625f;
            float minus_m23 = 2 * (x * w - y * z);
            //sin(beta) = -m23
            if( Math.Abs(minus_m23 - (-1)) <= Epsilon )
            {
                //beta = -pi/2的情况;sin(beta) = -1;cos(beta) = 0;
                b = (float) -Math.PI / 2;
                r = 0;
                float minus_m12 = -2 * (x * y - z * w);
                float m11 = 1 - 2 * (y * y + z * z);
                a = (float)Math.Atan2(minus_m12, m11);

            } else if( Math.Abs(minus_m23 - 1) <= Epsilon)
            {
                //beta = pi/2的情况;sin(beta) = 1;cos(beta) = 0;
                b = (float)Math.PI / 2;
                r = 0;
                float m12 = 2 * (x * y - z * w);
                float m11 = 1 - 2 * (y * y + z * z);
                a = (float)Math.Atan2(m12, m11);
            } else
            {
                a = (float)Math.Atan2(2 * (x * z + y * w), 1 - 2 * (x * x + y * y));
                b = (float)Math.Asin(2 * (x * w - y * z));
                r = (float)Math.Atan2(2 * (x * y + z * w), 1 - 2 * (x * x + z * z));
            }

            float angleX = Radian2Degree(b);
            float angleY = Radian2Degree(a);
            float angleZ = Radian2Degree(r);
            return new Vector3(angleX, angleY, angleZ);


        }
        
    }
}
