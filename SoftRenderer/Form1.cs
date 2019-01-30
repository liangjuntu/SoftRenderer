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
        Light light;
        Vector4 ambient;
        bool isDrawing = false;
        //System.Timers.Timer timer;
        //用下面这个Timer，是单线程的，上面的是多线程
        System.Windows.Forms.Timer timer;

        Games.WavefrontReader wavefrontReader = new Games.WavefrontReader();

        bool isResize = false;

        float FPS = 30f;
        //对外显示的
        String meshPath = "";
        String texturePath = "../../TestData/texture.jpg";

        Vector3 objPosition = Vector3.Zero;
        Vector3 objRotation = Vector3.Zero;
        Vector3 objScale = Vector3.One;
        Vector3 objAutoRot = new Vector3(1f, 2f, 0);

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
            light = new Light();
            ambient = new Vector4(0.2f, 0.2f, 0.2f, 1f);

            timer = new System.Windows.Forms.Timer(); 
            timer.Enabled = true;
            timer.Interval = (int)(1000f / FPS);
            timer.Start();
            timer.Tick += FrameUpdate;

            InitDefault();
            CtrlPanel.Visible = false;
            InitBtns();
            
            RefreshUI();

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            isResize = true;
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
            /*
          
            */


            for ( int i = 0; i < gameObjects.Count; ++i )
            {
                GameObject gameObject = gameObjects[i];
                Transform transform = gameObject.transform;
                Vector3 eulerAngles = transform.eulerAngles;
                transform.position = objPosition;
                transform.eulerAngles = objRotation;
                transform.scale = objScale;
            }
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
            renderer.SetUPLight(light);
            renderer.SetUPAmbient(ambient);
            renderer.DrawAll(gameObjects);
            renderer.DrawStatics();
            renderer.Present();
            isDrawing = false;
        }

        void DoTest()
        {
            renderer.TestClipping();
        }

        void FrameUpdate(object sender, EventArgs e)
        {
            if(isResize)
            {
                isResize = false;
                form1Graphics = this.CreateGraphics();
                renderer.OnResize(form1Graphics);
            }
            UpdateUI();
            PreUpdate();
            DoUpdate();
            //DoTest();
        }

        void UpdateUI()
        {
            InputByUI();
            
            objRotation += objAutoRot;
            objRotation.X %= 360f;
            objRotation.Y %= 360f;
            objRotation.Z %= 360f;
            //检查UI输入合法性
            drawInfo.CheckParams();
            RefreshUI();
        }

        void InputByUI()
        {
            //物体位置,旋转，缩放
            objPosition.X = Utils.SafeToSingle(PositionX.Text);
            objPosition.Y = Utils.SafeToSingle(PositionY.Text);
            objPosition.Z = Utils.SafeToSingle(PositionZ.Text);

            objRotation.X = Utils.SafeToSingle(RotationX.Text);
            objRotation.Y = Utils.SafeToSingle(RotationY.Text);
            objRotation.Z = Utils.SafeToSingle(RotationZ.Text);

            objScale.X = Utils.SafeToSingle(ScaleX.Text);
            objScale.Y = Utils.SafeToSingle(ScaleY.Text);
            objScale.Z = Utils.SafeToSingle(ScaleZ.Text);

            objAutoRot.X = Utils.SafeToSingle(AutoRotX.Text);
            objAutoRot.Y = Utils.SafeToSingle(AutoRotY.Text);
            objAutoRot.Z = Utils.SafeToSingle(AutoRotZ.Text);


            //摄像机
            
            drawInfo.cameraPosition.X = Utils.SafeToSingle(CamPosX.Text);
            drawInfo.cameraPosition.Y = Utils.SafeToSingle(CamPosY.Text);
            drawInfo.cameraPosition.Z = Utils.SafeToSingle(CamPosZ.Text);

            drawInfo.cameraEulerAngles.X = Utils.SafeToSingle(CamRotX.Text);
            drawInfo.cameraEulerAngles.Y = Utils.SafeToSingle(CamRotY.Text);
            drawInfo.cameraEulerAngles.Z = Utils.SafeToSingle(CamRotZ.Text);

            drawInfo.CameraNear = Utils.SafeToSingle(Near.Text);
            drawInfo.CameraFar = Utils.SafeToSingle(Far.Text);
            drawInfo.CameraFov = Utils.SafeToSingle(Fov.Text);

            light.lightDir.X = Utils.SafeToSingle(LightDirX.Text);
            light.lightDir.Y = Utils.SafeToSingle(LightDirY.Text);
            light.lightDir.Z = Utils.SafeToSingle(LightDirZ.Text);

            light.lightColor.X = Utils.SafeToSingle(LightColR.Text);
            light.lightColor.Y = Utils.SafeToSingle(LightColG.Text);
            light.lightColor.Z = Utils.SafeToSingle(LightColB.Text);

            ambient.X = Utils.SafeToSingle(AmbientR.Text);
            ambient.Y = Utils.SafeToSingle(AmbientG.Text);
            ambient.Z = Utils.SafeToSingle(AmbientB.Text);
            
        }

        void RefreshUI()
        {
            PositionX.Text = objPosition.X.ToString();
            PositionY.Text = objPosition.Y.ToString();
            PositionZ.Text = objPosition.Z.ToString();

            RotationX.Text = objRotation.X.ToString();
            RotationY.Text = objRotation.Y.ToString();
            RotationZ.Text = objRotation.Z.ToString();

            ScaleX.Text = objScale.X.ToString();
            ScaleY.Text = objScale.Y.ToString();
            ScaleZ.Text = objScale.Z.ToString();

            AutoRotX.Text = objAutoRot.X.ToString();
            AutoRotY.Text = objAutoRot.Y.ToString();
            AutoRotZ.Text = objAutoRot.Z.ToString();

            //摄像机
            CamPosX.Text = drawInfo.cameraPosition.X.ToString();
            CamPosY.Text = drawInfo.cameraPosition.Y.ToString();
            CamPosZ.Text = drawInfo.cameraPosition.Z.ToString();
            
            CamRotX.Text = drawInfo.cameraEulerAngles.X.ToString();
            CamRotY.Text = drawInfo.cameraEulerAngles.Y.ToString();
            CamRotZ.Text = drawInfo.cameraEulerAngles.Z.ToString();

            Near.Text = drawInfo.CameraNear.ToString();
            Far.Text = drawInfo.CameraFar.ToString();
            Fov.Text = drawInfo.CameraFov.ToString();


            //btns
            BtnTextureFilterMode.Text = string.Format("TexFilter:{0}",drawInfo.textureFilterMode.ToString());
            BtnDrawMode.Text = String.Format("DrawMode:{0}", drawInfo.drawMode.ToString());
            BtnCullMode.Text = String.Format("Cull:{0}", drawInfo.cullMode.ToString());
            BtnCullFrontEnd.Text = String.Format("FrontEnd:{0}", drawInfo.frontEndCull.ToString());
            BtnWinding.Text = String.Format("Winding:{0}", drawInfo.winding.ToString());
            BtnClippingMode.Text = String.Format("Clipping:{0}", drawInfo.clippingMode.ToString());
            BtnShadeMode.Text = String.Format("Shade:{0}", drawInfo.shadeMode.ToString());

            LightDirX.Text = light.lightDir.X.ToString();
            LightDirY.Text = light.lightDir.Y.ToString();
            LightDirZ.Text = light.lightDir.Z.ToString();

            LightColR.Text = light.lightColor.X.ToString();
            LightColG.Text = light.lightColor.Y.ToString();
            LightColB.Text = light.lightColor.Z.ToString();

            AmbientR.Text = ambient.X.ToString();
            AmbientG.Text = ambient.Y.ToString();
            AmbientB.Text = ambient.Z.ToString();

            BtnInvUV.Text = drawInfo.invertTexture ? "InvUV" : "UV";

        }

        void OnClickBtnCtrlPanel(object sender, EventArgs e)
        {
            CtrlPanel.Visible = !CtrlPanel.Visible;
        }

        void OnClickBtnTextureFilterMode(object sender, EventArgs e)
        {
            int mode = (int)(drawInfo.textureFilterMode +1) % (int)(TextureFilterMode.Bilinear + 1);
            drawInfo.textureFilterMode = (TextureFilterMode)(mode);
        }

        void OnClickBtnDrawMode(object sender, EventArgs e)
        {
            int mode = (int)(drawInfo.drawMode + 1) % (int)(DrawMode.Depth + 1);
            drawInfo.drawMode = (DrawMode)(mode);
        }

        void OnClickBtnCullMode(object sender, EventArgs e)
        {
            int mode = (int)(drawInfo.cullMode + 1) % (int)(CullMode.Back + 1);
            drawInfo.cullMode = (CullMode)(mode);
        }

        void OnClickBtnFrontEndCull(object sender, EventArgs e)
        {
            int mode = (int)(drawInfo.frontEndCull + 1) % (int)(FrontEndCull.On + 1);
            drawInfo.frontEndCull = (FrontEndCull)(mode);
        }

        void OnClickBtnWinding(object sender, EventArgs e)
        {
            int mode = (int)(drawInfo.winding + 1) % (int)(Winding.CounterClockwise + 1);
            drawInfo.winding = (Winding)(mode);
        }

        void OnClickBtnClippingMode(object sender, EventArgs e)
        {
            int mode = (int)(drawInfo.clippingMode+ 1) % (int)(ClippingMode.OnlyNF + 1);
            drawInfo.clippingMode = (ClippingMode)(mode);
        }
        void OnClickBtnShadeMode(object sender, EventArgs e)
        {
            int mode = (int)(drawInfo.shadeMode + 1) % (int)(ShadeMode.NDotL + 1);
            drawInfo.shadeMode = (ShadeMode)mode;
        }

        void OnClickBtnOpenObj(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "Obj文件(*.obj)|*.obj";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            meshPath = dialog.FileName;
            System.IO.FileStream objFile = new System.IO.FileStream(meshPath, System.IO.FileMode.Open);
            Games.WavefrontObject meshObj = wavefrontReader.Read(objFile);
            //Console.WriteLine(String.Format("Pos:{0}", meshObj.Positions.Count));
            gameObjects[0].meshObj = meshObj;
            gameObjects[0].texture = null;
            
        }
        
        void OnClickBtnOpenTex(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "贴图文件(*.*)|*.*";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            texturePath = dialog.FileName;
            Texture texture = Texture.FromFile(texturePath);
            gameObjects[0].texture = texture;
        }

        void OnClickBtnInvUV(object sender, EventArgs e)
        {
            drawInfo.invertTexture = !drawInfo.invertTexture;
        }

        void InitBtns()
        {
            BtnCtrlPanel.Click += OnClickBtnCtrlPanel;
            BtnTextureFilterMode.Click += OnClickBtnTextureFilterMode;
            BtnDrawMode.Click += OnClickBtnDrawMode;
            BtnCullMode.Click += OnClickBtnCullMode;
            BtnCullFrontEnd.Click += OnClickBtnFrontEndCull;
            BtnWinding.Click += OnClickBtnWinding;
            BtnClippingMode.Click += OnClickBtnClippingMode;
            BtnShadeMode.Click += OnClickBtnShadeMode;
            BtnOpenObj.Click += OnClickBtnOpenObj;
            BtnOpenTex.Click += OnClickBtnOpenTex;
            BtnInvUV.Click += OnClickBtnInvUV;
        }

    }
}
