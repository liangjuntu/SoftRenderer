using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftRenderer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //TODO 在这里绘制
            Graphics g = this.CreateGraphics();//创建GDI对像
            g.Clear(Color.White);
            SoftRenderer.Renderer.Utils.DrawCoordinateXY(g);
            SoftRenderer.Renderer.Utils.DrawCenterRect(g);

        }
        
    }
}
