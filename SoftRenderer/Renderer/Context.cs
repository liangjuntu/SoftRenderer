using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SoftRenderer
{
    public class Context
    {
        //Frame Buffer相关
        public Size frameSize;
        public Bitmap frameBuffer; //帧缓冲
        public Graphics frameGraphics; //frameBuffer对应的Graphics

        public Color ambientColor = Color.White;

        public Context(Size size)
        {
            frameSize = size;
            InitBySize();
        }

        void InitBySize()
        {
            frameBuffer = new Bitmap(frameSize.Width, frameSize.Height);
            frameGraphics = Graphics.FromImage(frameBuffer);
        }

        public void OnResize(Size size)
        {
            if(frameSize.Width == size.Width && frameSize.Height == size.Height)
            {
                return;
            }
            frameSize = size;
            InitBySize();
        }
    }
}
