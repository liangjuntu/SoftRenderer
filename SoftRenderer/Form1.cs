using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace SoftRenderer
{
    public partial class Form1 : Form
    {

        public enum MeshType
        {
            TestCube,
            WaveFrontObj,
        }

        Graphics form1Graphics;
        Renderer renderer;
        List<GameObject> gameObjects;
        DrawInfo drawInfo;
        bool isDrawing = false;
        //System.Timers.Timer timer;
        //用下面这个Timer，是单线程的，上面的是多线程
        System.Windows.Forms.Timer timer;

        

        float FPS = 30f;
        float rot = 0f;
        //对外显示的
        public String meshPath = "";
        public String texturePath = "../../TestData/texture.jpg";

        public Form1()
        {
            InitializeComponent();
            form1Graphics = this.CreateGraphics();
            renderer = new Renderer(form1Graphics);
            gameObjects = new List<GameObject>();
            //timer = new System.Timers.Timer(1000f / FPS);
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(FrameUpdate);
            //timer.AutoReset = true;
            //timer.Enabled = true;
            //timer.Start();

            drawInfo = new DrawInfo();
            timer = new System.Windows.Forms.Timer(); 
            timer.Enabled = true;
            timer.Interval = (int)(1000f / FPS);
            timer.Start();
            timer.Tick += FrameUpdate;

            InitDefault();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (renderer != null)
            {
                //form1Graphics = this.CreateGraphics();
                //renderer.OnResize(form1Graphics);
            }
        }

        /*
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //TODO 在这里绘制
            //form1Graphics.Clear(Color.White);
            //renderer.Test_BresenhamDrawLine();
            //renderer.Test_CohenSutherlandLineClip();
            //renderer.Test_BarycentricRasterizeTriangle();
            //renderer.Present();
            //Debug_DrawCoordinate();
        }
        */

        void Debug_DrawCoordinate()
        {
            SoftRenderer.Utils.DrawCoordinateXY(form1Graphics);
            SoftRenderer.Utils.DrawCenterRect(form1Graphics);
        }

        void SetUpGameObjects()
        {
            //rot += 0.05f;
            for( int i = 0; i < gameObjects.Count; ++i )
            {
                GameObject gameObject = gameObjects[i];
                Transform transform = gameObject.transform;
                Vector3 eulerAngles = transform.eulerAngles;
                eulerAngles.Y = rot;
                eulerAngles.X = rot;
                transform.eulerAngles = eulerAngles;
            }
        }

        void AddGameObject(string path)
        {
            //TODO
            GameObject gameObject = new GameObject();
            gameObjects.Add(gameObject);
        }
        
        void InitDefault()
        {
            //default GameObject
            GameObject gameObject = new GameObject();
            gameObjects.Add(gameObject);
            Transform transform = gameObject.transform;
            gameObject.transform.position = Vector3.Zero;
            gameObject.transform.eulerAngles = Vector3.Zero;
            gameObject.transform.scale = Vector3.One;
            gameObject.meshObj = Test.CubeTestData.ToWavefrontObject();
            Texture texture = Texture.FromFile(texturePath);
            gameObject.texture = texture;

            //default Camera

        }

        void PreUpdate()
        {
            SetUpGameObjects();
        }

        void DoUpdate()
        {
            if (isDrawing)
            {
                //TODO Log Error
                return;
            }
            isDrawing = true;
            renderer.Clear();
            renderer.SetUpCameraAndContext(drawInfo);
            renderer.DrawAll(gameObjects);
            renderer.Present();
            isDrawing = false;
        }

        void FrameUpdate(object sender, EventArgs e)
        {
            PreUpdate();
            DoUpdate();
        }

    }
}
