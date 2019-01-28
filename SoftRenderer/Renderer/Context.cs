using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SoftRenderer
{
    public enum DrawMode
    {
        Normal = 0,
        Wireframe,
        Depth
    }
    
    public enum CullMode
    {
        None,
        Back,
        //Front
    }

    public enum FrontEndCull
    {
        Off,
        On
    }

    public enum Winding
    {
        Clockwise,
        CounterClockwise,
    }

    public class Context
    {
        public Statics statics { get; private set; }

        //Frame Buffer相关
        public Size frameSize;
        public Bitmap frameBuffer; //帧缓冲
        public Graphics frameGraphics; //frameBuffer对应的Graphics
        public float[,] depthBuffer;

        public Color clearColor = Color.Black;
        public DrawMode drawMode = DrawMode.Normal;
        public TextureFilterMode textureFilterMode { get; set; }
        public Color wireframeColor = Color.White;
        public Winding winding = Winding.CounterClockwise;
        public CullMode cullMode = CullMode.None;
        public FrontEndCull frontEndCull = FrontEndCull.Off;
        public ClippingMode clippingMode = ClippingMode.SixPlane;

        public Context(Size size)
        {
            frameSize = size;
            statics = new Statics();
            InitBySize();
        }

        void InitBySize()
        {
            frameBuffer = new Bitmap(frameSize.Width, frameSize.Height);
            frameGraphics = Graphics.FromImage(frameBuffer);
            depthBuffer = new float[frameSize.Width,frameSize.Height];
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

        public void ClearFrameBuffer()
        {
            Color col = drawMode == DrawMode.Depth ? Color.White : clearColor;
            frameGraphics.Clear(col);
            for (int i = 0; i < frameSize.Width; i++)
            {
                for(int j = 0; j < frameSize.Height; ++ j)
                {
                    depthBuffer[i, j] = float.MaxValue;
                }
            }
        }
    }
}
