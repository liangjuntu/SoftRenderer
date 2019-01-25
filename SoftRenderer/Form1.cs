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

        void TestRotation()
        {
            Vector3 eulerAngles = new Vector3(30f, 40f, 50f);
            float radX = Utils.Degree2Radian(eulerAngles.X);
            float radY = Utils.Degree2Radian(eulerAngles.Y);
            float radZ = Utils.Degree2Radian(eulerAngles.Z);
            Quaternion q = Quaternion.CreateFromYawPitchRoll(radY, radX, radZ);
            Matrix4x4 m = Matrix4x4.CreateFromQuaternion(q);
            m = Matrix4x4.Transpose(m);
            Vector3 angles = Utils.QuaternionToEulerAngles(q);
            Console.WriteLine(String.Format("q:{0}", q));
            Console.WriteLine(String.Format("m:{0}", m));
            Console.WriteLine(String.Format("angles:{0}", angles));
        }

        void TestEulerAngles()
        {
            Vector3 eulerAngles = new Vector3(-90f, 50f, 20f);
            float radX = Utils.Degree2Radian(eulerAngles.X);
            float radY = Utils.Degree2Radian(eulerAngles.Y);
            float radZ = Utils.Degree2Radian(eulerAngles.Z);
            Quaternion q = Quaternion.CreateFromYawPitchRoll(radY, radX, radZ);
            Vector3 angles = Utils.QuaternionToEulerAngles(q);
            Console.WriteLine(String.Format("angles:{0}", angles));

        }

        void SetUpGameObjects()
        {
            //如果用欧拉角做旋转矩阵旋转可能会有gimbal lock(万向锁)的问题https://www.cnblogs.com/psklf/p/5656938.html
            rot += 1f;
            rot %= 360f;
            
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
            //TestRotation();
            //TestEulerAngles();
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
            renderer.DrawStatics();
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
