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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //TODO 在这里绘制
            form1Graphics.Clear(Color.White);
            //TODO加Renderer
            renderer.Test_BresenhamDrawLine();
            Debug_DrawCoordinate();
        }

        void Debug_DrawCoordinate()
        {
            SoftRenderer.Utils.DrawCoordinateXY(form1Graphics);
            SoftRenderer.Utils.DrawCenterRect(form1Graphics);
        }


    }
}
