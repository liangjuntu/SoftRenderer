using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Numerics;
using Games;

namespace SoftRenderer
{
   

    public class Renderer
    {
        Graphics graphics;
        Context context;
        Rasterizer rasterizer;
        public Camera camera { protected set; get; }
       
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
            camera = new Camera();
            
        }

        void InitByGraphics(Graphics g)
        {
            Debug.Assert(g != null);
            graphics = g;
        }

        public void OnResize(Graphics g)
        {
            lock (context.frameBuffer)
            {
                InitByGraphics(g);
                RectangleF rect = graphics.VisibleClipBounds;
                Size frameSize = new Size((int)rect.Width, (int)rect.Height);
                //Debug.Print(string.Format("new size:{0}", frameSize));
                context.OnResize(frameSize);
            }
        }

        public void Clear()
        {
            context.statics.Clear();
            lock (context.frameBuffer)
            {
                context.ClearFrameBuffer();
            }
        }


        public void Present()
        {
            //画上去
            lock (context.frameBuffer)
            {
                graphics.DrawImage(context.frameBuffer, 0, 0);
            }
        }
        

        public void Test_BresenhamDrawLine()
        {
            Clear();
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

            

            Font font = new Font("Arial", 16);

            SolidBrush brush = new SolidBrush(Color.Black);

            graphics.DrawString("Test_BresenhamDrawLine", font, brush, 20, 20);
        }

        public void Test_CohenSutherlandLineClip()
        {
            Clear();
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


            Font font = new Font("Arial", 16);

            SolidBrush brush = new SolidBrush(Color.Black);

            graphics.DrawString("Test_CohenSutherlandLineClip", font, brush, 20, 20);

        }


        public void Test_BarycentricRasterizeTriangle()
        {
            context.clearColor = Color.White;
            Clear();
            //假设n = 1, far = 2
            //测试光栅化和插值
            //第一个三角形,v0红色在中上，v1绿和v2蓝在中间的左右，v0在far plane， v1,v2在near plane
            VSOutput v0 = new VSOutput();
            v0.position = new Vector4(0f, 1f, 1f, 2f);
            v0.color = new Vector4(1f, 0f, 0f, 1f);
            VSOutput v1 = new VSOutput();
            v1.position = new Vector4(-0.5f, -0.5f, -1f, 1f);
            v1.color = new Vector4(0f, 1f, 0f, 1f);
            VSOutput v2 = new VSOutput();
            v2.position = new Vector4(0.5f, -0.5f, -1f, 1f);
            v2.color = new Vector4(0f, 0f, 1f, 1f);
            
            v0 = Rasterizer.PerspectiveDivide(v0);
            v0 = rasterizer.ViewportTransform(v0);
            v1 = Rasterizer.PerspectiveDivide(v1);
            v1 = rasterizer.ViewportTransform(v1);
            v2 = Rasterizer.PerspectiveDivide(v2);
            v2 = rasterizer.ViewportTransform(v2);

            rasterizer.BarycentricRasterizeTriangle(v0, v1, v2);

            //测试depth test
            //第二个三角形，v0红在近平面，v1,v2在远
            v0.position = new Vector4(-0.5f, 0.5f, -1f, 1f);
            v1.position = new Vector4(-2f, -1f, 1f, 2f);
            v2.position = new Vector4(0f, -2f, 1f, 2f);
            v0 = Rasterizer.PerspectiveDivide(v0);
            v0 = rasterizer.ViewportTransform(v0);
            v1 = Rasterizer.PerspectiveDivide(v1);
            v1 = rasterizer.ViewportTransform(v1);
            v2 = Rasterizer.PerspectiveDivide(v2);
            v2 = rasterizer.ViewportTransform(v2);
            rasterizer.BarycentricRasterizeTriangle(v0, v1, v2);

            //第三个三角形，v1绿在近，其他在远平面
            v0.position = new Vector4(-2f, 0f, 1f, 2f);
            v1.position = new Vector4(-0.5f, -1f, -1f, 1f);
            v2.position = new Vector4(0f, -1f, 1f, 2f);
            v0 = Rasterizer.PerspectiveDivide(v0);
            v0 = rasterizer.ViewportTransform(v0);
            v1 = Rasterizer.PerspectiveDivide(v1);
            v1 = rasterizer.ViewportTransform(v1);
            v2 = Rasterizer.PerspectiveDivide(v2);
            v2 = rasterizer.ViewportTransform(v2);
            rasterizer.BarycentricRasterizeTriangle(v0, v1, v2);
        }

        bool BackfaceCulling(VSOutput v0, VSOutput v1, VSOutput v2)
        {
            if(context.cullMode == CullMode.None)
            {
                return true;
            }
            //cull back
            Vector2 p0 = new Vector2(v0.position.X, v0.position.Y);
            Vector2 p1 = new Vector2(v1.position.X, v1.position.Y);
            Vector2 p2 = new Vector2(v2.position.X, v2.position.Y);
            float area = Rasterizer.EdgeFunction(p0, p1, p2);
            //在Clip Space, area >= 0表示顺时针
            if (context.winding == Winding.Clockwise)
            {
                return area >= 0;
            }
            return area <= 0;
        }

        public void DrawGameObject(GameObject gameobject, Shader shader)
        {
            context.statics.meshCount += 1;

            //设置Shader相关
            Transform transform = gameobject.transform;
            Matrix4x4 modelMatrix = transform.ModelToWorld;
            shader.SetModelMatrix(modelMatrix);

            Texture texture = gameobject.texture;
            shader.texture = texture;
            rasterizer.shader = shader;

            WavefrontObject meshObj = gameobject.meshObj;

            foreach( var faceGroup in meshObj.Groups)
            {
                context.statics.submeshCount += 1;
                //faceGroup相当于submesh
                foreach( var face in faceGroup.Faces )
                {
                    //face为triangle fan;可能会有多个三角形
                    Debug.Assert(face.Vertices.Count > 2);
                    Vertex vertex0 = Vertex.FromWavefrontVertex(meshObj, face.Vertices[0]);
                    VSOutput vClip0 = shader.VertShader(vertex0);
                    //TODO 做Clipping
                    VSOutput vNDC0 = Rasterizer.PerspectiveDivide(vClip0);
                    if(vNDC0 == null)
                    {
                        continue;
                    }
                    VSOutput vScreen0 = rasterizer.ViewportTransform(vNDC0);
                    context.statics.vertexCount += 1;

                    for ( int i = 1; i+1 < face.Vertices.Count; i+=2)
                    {
                        context.statics.vertexCount += 2;
                        Vertex vertex1 = Vertex.FromWavefrontVertex(meshObj, face.Vertices[i]);
                        Vertex vertex2 = Vertex.FromWavefrontVertex(meshObj, face.Vertices[i+1]);

                        VSOutput vClip1 = shader.VertShader(vertex1);
                        VSOutput vClip2 = shader.VertShader(vertex2);
                        if (!BackfaceCulling(vClip0, vClip1, vClip2))
                        {
                            continue;
                        }
                        
                        VSOutput vNDC1 = Rasterizer.PerspectiveDivide(vClip1);
                        VSOutput vNDC2 = Rasterizer.PerspectiveDivide(vClip2);
                        if(vNDC1 == null || vNDC2 == null)
                        {
                            continue;
                        }
                        VSOutput vScreen1 = rasterizer.ViewportTransform(vNDC1);
                        VSOutput vScreen2 = rasterizer.ViewportTransform(vNDC2);
                        
                        context.statics.triangleCount += 1;
                        rasterizer.RasterizeTriangle(vScreen0, vScreen1, vScreen2);
                    } 
                }

            }

            rasterizer.shader = null;



        }

        public void DrawAll(List<GameObject> gameobjes)
        {
            lock (context.frameBuffer)
            {
                Matrix4x4 viewMatrix = camera.ViewMatrix;
                Matrix4x4 projectionMatrix = camera.ProjectionMatrix;
                ShaderContext shaderContext = new ShaderContext();

                shaderContext.SetViewProjectionMatrix(viewMatrix, projectionMatrix);

                shaderContext.textureFilterMode = context.textureFilterMode;

                Shader shader = new Shader(shaderContext);


                foreach (var gameobject in gameobjes)
                {
                    DrawGameObject(gameobject, shader);
                }
            }
        }

        public void SetUpCameraAndContext(DrawInfo drawInfo)
        {
            Transform transform = camera.transform;
            transform.position = drawInfo.cameraPosition;
            transform.eulerAngles = drawInfo.cameraEulerAngles;
            camera.Near = drawInfo.CameraNear;
            camera.Far = drawInfo.CameraFar;
            camera.Fov = drawInfo.CameraFov;
            camera.Aspect = (float)context.frameSize.Width / (float)context.frameSize.Height;

            context.clearColor = drawInfo.CameraClearColor;
            context.textureFilterMode = drawInfo.textureFilterMode;
            context.drawMode = drawInfo.drawMode;
            context.winding = drawInfo.winding;
            context.cullMode = drawInfo.cullMode;
        }

        public void DrawStatics()
        {
            Font font = new Font("Arial", 16);

            SolidBrush brush = new SolidBrush(Color.White);

            context.frameGraphics.DrawString(String.Format("Mesh:{0}-SubMesh:{1}",context.statics.meshCount,context.statics.submeshCount), font, brush, 20, 20);
            context.frameGraphics.DrawString(String.Format("vert:{0}-tri:{1}-frag:{2}",context.statics.vertexCount,context.statics.triangleCount, context.statics.fragmentCount), font, brush, 20, 40);
        }
       
    }
}
