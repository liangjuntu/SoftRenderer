using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoftRenderer;

namespace SoftRenderer
{
    public partial class Form1 : Form
    {

        Graphics form1Graphics;
        Renderer renderer;

        public Form1()
        {
            InitializeComponent();
            form1Graphics = this.CreateGraphics();
            renderer = new Renderer(form1Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (renderer != null)
            {
                form1Graphics = this.CreateGraphics();
                renderer.OnResize(form1Graphics);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //TODO 在这里绘制
            form1Graphics.Clear(Color.White);
            //renderer.Test_BresenhamDrawLine();
            renderer.Test_CohenSutherlandLineClip();
            Debug_DrawCoordinate();
        }

        void Debug_DrawCoordinate()
        {
            SoftRenderer.Utils.DrawCoordinateXY(form1Graphics);
            SoftRenderer.Utils.DrawCenterRect(form1Graphics);
        }


    }
}
