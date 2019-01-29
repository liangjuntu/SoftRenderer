namespace SoftRenderer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.PositionX = new System.Windows.Forms.TextBox();
            this.LabelPosition = new System.Windows.Forms.Label();
            this.PositionY = new System.Windows.Forms.TextBox();
            this.PositionZ = new System.Windows.Forms.TextBox();
            this.CtrlPanel = new System.Windows.Forms.Panel();
            this.LabelLightCol = new System.Windows.Forms.Label();
            this.LightColB = new System.Windows.Forms.TextBox();
            this.LightColR = new System.Windows.Forms.TextBox();
            this.LightColG = new System.Windows.Forms.TextBox();
            this.LabelLightDir = new System.Windows.Forms.Label();
            this.LightDirZ = new System.Windows.Forms.TextBox();
            this.LightDirX = new System.Windows.Forms.TextBox();
            this.LightDirY = new System.Windows.Forms.TextBox();
            this.BtnShadeMode = new System.Windows.Forms.Button();
            this.BtnClippingMode = new System.Windows.Forms.Button();
            this.BtnWinding = new System.Windows.Forms.Button();
            this.BtnCullFrontEnd = new System.Windows.Forms.Button();
            this.BtnCullMode = new System.Windows.Forms.Button();
            this.BtnDrawMode = new System.Windows.Forms.Button();
            this.BtnTextureFilterMode = new System.Windows.Forms.Button();
            this.Fov = new System.Windows.Forms.TextBox();
            this.LabelFov = new System.Windows.Forms.Label();
            this.Far = new System.Windows.Forms.TextBox();
            this.LabelCamFar = new System.Windows.Forms.Label();
            this.Near = new System.Windows.Forms.TextBox();
            this.LabelCamNear = new System.Windows.Forms.Label();
            this.AutoRot = new System.Windows.Forms.Label();
            this.AutoRotZ = new System.Windows.Forms.TextBox();
            this.AutoRotX = new System.Windows.Forms.TextBox();
            this.AutoRotY = new System.Windows.Forms.TextBox();
            this.CamRot = new System.Windows.Forms.Label();
            this.CamRotZ = new System.Windows.Forms.TextBox();
            this.CamRotX = new System.Windows.Forms.TextBox();
            this.CamRotY = new System.Windows.Forms.TextBox();
            this.CamPos = new System.Windows.Forms.Label();
            this.CamPosZ = new System.Windows.Forms.TextBox();
            this.CamPosX = new System.Windows.Forms.TextBox();
            this.CamPosY = new System.Windows.Forms.TextBox();
            this.LabelScale = new System.Windows.Forms.Label();
            this.ScaleZ = new System.Windows.Forms.TextBox();
            this.ScaleX = new System.Windows.Forms.TextBox();
            this.ScaleY = new System.Windows.Forms.TextBox();
            this.LabelRotation = new System.Windows.Forms.Label();
            this.RotationZ = new System.Windows.Forms.TextBox();
            this.RotationX = new System.Windows.Forms.TextBox();
            this.RotationY = new System.Windows.Forms.TextBox();
            this.BtnCtrlPanel = new System.Windows.Forms.Button();
            this.LabelAmbient = new System.Windows.Forms.Label();
            this.AmbientB = new System.Windows.Forms.TextBox();
            this.AmbientR = new System.Windows.Forms.TextBox();
            this.AmbientG = new System.Windows.Forms.TextBox();
            this.CtrlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PositionX
            // 
            this.PositionX.Location = new System.Drawing.Point(82, -1);
            this.PositionX.Name = "PositionX";
            this.PositionX.Size = new System.Drawing.Size(27, 21);
            this.PositionX.TabIndex = 0;
            this.PositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelPosition
            // 
            this.LabelPosition.AutoSize = true;
            this.LabelPosition.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelPosition.Location = new System.Drawing.Point(13, 0);
            this.LabelPosition.Name = "LabelPosition";
            this.LabelPosition.Size = new System.Drawing.Size(63, 14);
            this.LabelPosition.TabIndex = 1;
            this.LabelPosition.Text = "Position";
            // 
            // PositionY
            // 
            this.PositionY.Location = new System.Drawing.Point(115, -1);
            this.PositionY.Name = "PositionY";
            this.PositionY.Size = new System.Drawing.Size(27, 21);
            this.PositionY.TabIndex = 2;
            this.PositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PositionZ
            // 
            this.PositionZ.Location = new System.Drawing.Point(148, -1);
            this.PositionZ.Name = "PositionZ";
            this.PositionZ.Size = new System.Drawing.Size(27, 21);
            this.PositionZ.TabIndex = 3;
            this.PositionZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CtrlPanel
            // 
            this.CtrlPanel.Controls.Add(this.AmbientB);
            this.CtrlPanel.Controls.Add(this.AmbientR);
            this.CtrlPanel.Controls.Add(this.AmbientG);
            this.CtrlPanel.Controls.Add(this.LabelAmbient);
            this.CtrlPanel.Controls.Add(this.LabelLightCol);
            this.CtrlPanel.Controls.Add(this.LightColB);
            this.CtrlPanel.Controls.Add(this.LightColR);
            this.CtrlPanel.Controls.Add(this.LightColG);
            this.CtrlPanel.Controls.Add(this.LabelLightDir);
            this.CtrlPanel.Controls.Add(this.LightDirZ);
            this.CtrlPanel.Controls.Add(this.LightDirX);
            this.CtrlPanel.Controls.Add(this.LightDirY);
            this.CtrlPanel.Controls.Add(this.BtnShadeMode);
            this.CtrlPanel.Controls.Add(this.BtnClippingMode);
            this.CtrlPanel.Controls.Add(this.BtnWinding);
            this.CtrlPanel.Controls.Add(this.BtnCullFrontEnd);
            this.CtrlPanel.Controls.Add(this.BtnCullMode);
            this.CtrlPanel.Controls.Add(this.BtnDrawMode);
            this.CtrlPanel.Controls.Add(this.BtnTextureFilterMode);
            this.CtrlPanel.Controls.Add(this.Fov);
            this.CtrlPanel.Controls.Add(this.LabelFov);
            this.CtrlPanel.Controls.Add(this.Far);
            this.CtrlPanel.Controls.Add(this.LabelCamFar);
            this.CtrlPanel.Controls.Add(this.Near);
            this.CtrlPanel.Controls.Add(this.LabelCamNear);
            this.CtrlPanel.Controls.Add(this.AutoRot);
            this.CtrlPanel.Controls.Add(this.AutoRotZ);
            this.CtrlPanel.Controls.Add(this.AutoRotX);
            this.CtrlPanel.Controls.Add(this.AutoRotY);
            this.CtrlPanel.Controls.Add(this.CamRot);
            this.CtrlPanel.Controls.Add(this.CamRotZ);
            this.CtrlPanel.Controls.Add(this.CamRotX);
            this.CtrlPanel.Controls.Add(this.CamRotY);
            this.CtrlPanel.Controls.Add(this.CamPos);
            this.CtrlPanel.Controls.Add(this.CamPosZ);
            this.CtrlPanel.Controls.Add(this.CamPosX);
            this.CtrlPanel.Controls.Add(this.CamPosY);
            this.CtrlPanel.Controls.Add(this.LabelScale);
            this.CtrlPanel.Controls.Add(this.ScaleZ);
            this.CtrlPanel.Controls.Add(this.ScaleX);
            this.CtrlPanel.Controls.Add(this.ScaleY);
            this.CtrlPanel.Controls.Add(this.LabelRotation);
            this.CtrlPanel.Controls.Add(this.RotationZ);
            this.CtrlPanel.Controls.Add(this.RotationX);
            this.CtrlPanel.Controls.Add(this.RotationY);
            this.CtrlPanel.Controls.Add(this.LabelPosition);
            this.CtrlPanel.Controls.Add(this.PositionZ);
            this.CtrlPanel.Controls.Add(this.PositionX);
            this.CtrlPanel.Controls.Add(this.PositionY);
            this.CtrlPanel.Location = new System.Drawing.Point(616, 23);
            this.CtrlPanel.Name = "CtrlPanel";
            this.CtrlPanel.Size = new System.Drawing.Size(185, 426);
            this.CtrlPanel.TabIndex = 4;
            // 
            // LabelLightCol
            // 
            this.LabelLightCol.AutoSize = true;
            this.LabelLightCol.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelLightCol.Location = new System.Drawing.Point(13, 340);
            this.LabelLightCol.Name = "LabelLightCol";
            this.LabelLightCol.Size = new System.Drawing.Size(63, 14);
            this.LabelLightCol.TabIndex = 43;
            this.LabelLightCol.Text = "LightCol";
            // 
            // LightColB
            // 
            this.LightColB.Location = new System.Drawing.Point(148, 339);
            this.LightColB.Name = "LightColB";
            this.LightColB.Size = new System.Drawing.Size(27, 21);
            this.LightColB.TabIndex = 45;
            this.LightColB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LightColR
            // 
            this.LightColR.Location = new System.Drawing.Point(82, 339);
            this.LightColR.Name = "LightColR";
            this.LightColR.Size = new System.Drawing.Size(27, 21);
            this.LightColR.TabIndex = 42;
            this.LightColR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LightColG
            // 
            this.LightColG.Location = new System.Drawing.Point(115, 339);
            this.LightColG.Name = "LightColG";
            this.LightColG.Size = new System.Drawing.Size(27, 21);
            this.LightColG.TabIndex = 44;
            this.LightColG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelLightDir
            // 
            this.LabelLightDir.AutoSize = true;
            this.LabelLightDir.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelLightDir.Location = new System.Drawing.Point(13, 311);
            this.LabelLightDir.Name = "LabelLightDir";
            this.LabelLightDir.Size = new System.Drawing.Size(63, 14);
            this.LabelLightDir.TabIndex = 39;
            this.LabelLightDir.Text = "LightDir";
            // 
            // LightDirZ
            // 
            this.LightDirZ.Location = new System.Drawing.Point(148, 310);
            this.LightDirZ.Name = "LightDirZ";
            this.LightDirZ.Size = new System.Drawing.Size(27, 21);
            this.LightDirZ.TabIndex = 41;
            this.LightDirZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LightDirX
            // 
            this.LightDirX.Location = new System.Drawing.Point(82, 310);
            this.LightDirX.Name = "LightDirX";
            this.LightDirX.Size = new System.Drawing.Size(27, 21);
            this.LightDirX.TabIndex = 38;
            this.LightDirX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LightDirY
            // 
            this.LightDirY.Location = new System.Drawing.Point(115, 310);
            this.LightDirY.Name = "LightDirY";
            this.LightDirY.Size = new System.Drawing.Size(27, 21);
            this.LightDirY.TabIndex = 40;
            this.LightDirY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BtnShadeMode
            // 
            this.BtnShadeMode.Location = new System.Drawing.Point(6, 285);
            this.BtnShadeMode.Name = "BtnShadeMode";
            this.BtnShadeMode.Size = new System.Drawing.Size(136, 23);
            this.BtnShadeMode.TabIndex = 37;
            this.BtnShadeMode.Text = "Shade:Tex";
            this.BtnShadeMode.UseVisualStyleBackColor = true;
            // 
            // BtnClippingMode
            // 
            this.BtnClippingMode.Location = new System.Drawing.Point(6, 266);
            this.BtnClippingMode.Name = "BtnClippingMode";
            this.BtnClippingMode.Size = new System.Drawing.Size(136, 23);
            this.BtnClippingMode.TabIndex = 36;
            this.BtnClippingMode.Text = "Clipping:SixPlane";
            this.BtnClippingMode.UseVisualStyleBackColor = true;
            // 
            // BtnWinding
            // 
            this.BtnWinding.Location = new System.Drawing.Point(6, 246);
            this.BtnWinding.Name = "BtnWinding";
            this.BtnWinding.Size = new System.Drawing.Size(136, 23);
            this.BtnWinding.TabIndex = 35;
            this.BtnWinding.Text = "Winding:CCV";
            this.BtnWinding.UseVisualStyleBackColor = true;
            // 
            // BtnCullFrontEnd
            // 
            this.BtnCullFrontEnd.Location = new System.Drawing.Point(71, 227);
            this.BtnCullFrontEnd.Name = "BtnCullFrontEnd";
            this.BtnCullFrontEnd.Size = new System.Drawing.Size(90, 22);
            this.BtnCullFrontEnd.TabIndex = 34;
            this.BtnCullFrontEnd.Text = "FrontEnd:On";
            this.BtnCullFrontEnd.UseVisualStyleBackColor = true;
            // 
            // BtnCullMode
            // 
            this.BtnCullMode.Location = new System.Drawing.Point(8, 227);
            this.BtnCullMode.Name = "BtnCullMode";
            this.BtnCullMode.Size = new System.Drawing.Size(68, 22);
            this.BtnCullMode.TabIndex = 33;
            this.BtnCullMode.Text = "Cull:Back";
            this.BtnCullMode.UseVisualStyleBackColor = true;
            // 
            // BtnDrawMode
            // 
            this.BtnDrawMode.Location = new System.Drawing.Point(8, 207);
            this.BtnDrawMode.Name = "BtnDrawMode";
            this.BtnDrawMode.Size = new System.Drawing.Size(136, 23);
            this.BtnDrawMode.TabIndex = 32;
            this.BtnDrawMode.Text = "DrawMode:Normal";
            this.BtnDrawMode.UseVisualStyleBackColor = true;
            // 
            // BtnTextureFilterMode
            // 
            this.BtnTextureFilterMode.Location = new System.Drawing.Point(8, 188);
            this.BtnTextureFilterMode.Name = "BtnTextureFilterMode";
            this.BtnTextureFilterMode.Size = new System.Drawing.Size(136, 23);
            this.BtnTextureFilterMode.TabIndex = 31;
            this.BtnTextureFilterMode.Text = "TexFilter:Bilinear";
            this.BtnTextureFilterMode.UseVisualStyleBackColor = true;
            // 
            // Fov
            // 
            this.Fov.Location = new System.Drawing.Point(147, 164);
            this.Fov.Name = "Fov";
            this.Fov.Size = new System.Drawing.Size(27, 21);
            this.Fov.TabIndex = 30;
            this.Fov.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelFov
            // 
            this.LabelFov.AutoSize = true;
            this.LabelFov.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelFov.Location = new System.Drawing.Point(122, 170);
            this.LabelFov.Name = "LabelFov";
            this.LabelFov.Size = new System.Drawing.Size(28, 14);
            this.LabelFov.TabIndex = 29;
            this.LabelFov.Text = "Fov";
            // 
            // Far
            // 
            this.Far.Location = new System.Drawing.Point(93, 165);
            this.Far.Name = "Far";
            this.Far.Size = new System.Drawing.Size(27, 21);
            this.Far.TabIndex = 28;
            this.Far.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelCamFar
            // 
            this.LabelCamFar.AutoSize = true;
            this.LabelCamFar.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelCamFar.Location = new System.Drawing.Point(68, 171);
            this.LabelCamFar.Name = "LabelCamFar";
            this.LabelCamFar.Size = new System.Drawing.Size(28, 14);
            this.LabelCamFar.TabIndex = 27;
            this.LabelCamFar.Text = "Far";
            // 
            // Near
            // 
            this.Near.Location = new System.Drawing.Point(35, 164);
            this.Near.Name = "Near";
            this.Near.Size = new System.Drawing.Size(27, 21);
            this.Near.TabIndex = 26;
            this.Near.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelCamNear
            // 
            this.LabelCamNear.AutoSize = true;
            this.LabelCamNear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelCamNear.Location = new System.Drawing.Point(5, 171);
            this.LabelCamNear.Name = "LabelCamNear";
            this.LabelCamNear.Size = new System.Drawing.Size(35, 14);
            this.LabelCamNear.TabIndex = 25;
            this.LabelCamNear.Text = "Near";
            // 
            // AutoRot
            // 
            this.AutoRot.AutoSize = true;
            this.AutoRot.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AutoRot.Location = new System.Drawing.Point(13, 85);
            this.AutoRot.Name = "AutoRot";
            this.AutoRot.Size = new System.Drawing.Size(56, 14);
            this.AutoRot.TabIndex = 21;
            this.AutoRot.Text = "AutoRot";
            // 
            // AutoRotZ
            // 
            this.AutoRotZ.Location = new System.Drawing.Point(148, 84);
            this.AutoRotZ.Name = "AutoRotZ";
            this.AutoRotZ.Size = new System.Drawing.Size(27, 21);
            this.AutoRotZ.TabIndex = 23;
            this.AutoRotZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AutoRotX
            // 
            this.AutoRotX.Location = new System.Drawing.Point(82, 84);
            this.AutoRotX.Name = "AutoRotX";
            this.AutoRotX.Size = new System.Drawing.Size(27, 21);
            this.AutoRotX.TabIndex = 20;
            this.AutoRotX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AutoRotY
            // 
            this.AutoRotY.Location = new System.Drawing.Point(115, 84);
            this.AutoRotY.Name = "AutoRotY";
            this.AutoRotY.Size = new System.Drawing.Size(27, 21);
            this.AutoRotY.TabIndex = 22;
            this.AutoRotY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CamRot
            // 
            this.CamRot.AutoSize = true;
            this.CamRot.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CamRot.Location = new System.Drawing.Point(13, 139);
            this.CamRot.Name = "CamRot";
            this.CamRot.Size = new System.Drawing.Size(49, 14);
            this.CamRot.TabIndex = 17;
            this.CamRot.Text = "CamRot";
            // 
            // CamRotZ
            // 
            this.CamRotZ.Location = new System.Drawing.Point(148, 138);
            this.CamRotZ.Name = "CamRotZ";
            this.CamRotZ.Size = new System.Drawing.Size(27, 21);
            this.CamRotZ.TabIndex = 19;
            this.CamRotZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CamRotX
            // 
            this.CamRotX.Location = new System.Drawing.Point(82, 138);
            this.CamRotX.Name = "CamRotX";
            this.CamRotX.Size = new System.Drawing.Size(27, 21);
            this.CamRotX.TabIndex = 16;
            this.CamRotX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CamRotY
            // 
            this.CamRotY.Location = new System.Drawing.Point(115, 138);
            this.CamRotY.Name = "CamRotY";
            this.CamRotY.Size = new System.Drawing.Size(27, 21);
            this.CamRotY.TabIndex = 18;
            this.CamRotY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CamPos
            // 
            this.CamPos.AutoSize = true;
            this.CamPos.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CamPos.Location = new System.Drawing.Point(13, 112);
            this.CamPos.Name = "CamPos";
            this.CamPos.Size = new System.Drawing.Size(49, 14);
            this.CamPos.TabIndex = 13;
            this.CamPos.Text = "CamPos";
            // 
            // CamPosZ
            // 
            this.CamPosZ.Location = new System.Drawing.Point(148, 111);
            this.CamPosZ.Name = "CamPosZ";
            this.CamPosZ.Size = new System.Drawing.Size(27, 21);
            this.CamPosZ.TabIndex = 15;
            this.CamPosZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CamPosX
            // 
            this.CamPosX.Location = new System.Drawing.Point(82, 111);
            this.CamPosX.Name = "CamPosX";
            this.CamPosX.Size = new System.Drawing.Size(27, 21);
            this.CamPosX.TabIndex = 12;
            this.CamPosX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CamPosY
            // 
            this.CamPosY.Location = new System.Drawing.Point(115, 111);
            this.CamPosY.Name = "CamPosY";
            this.CamPosY.Size = new System.Drawing.Size(27, 21);
            this.CamPosY.TabIndex = 14;
            this.CamPosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelScale
            // 
            this.LabelScale.AutoSize = true;
            this.LabelScale.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelScale.Location = new System.Drawing.Point(13, 54);
            this.LabelScale.Name = "LabelScale";
            this.LabelScale.Size = new System.Drawing.Size(42, 14);
            this.LabelScale.TabIndex = 9;
            this.LabelScale.Text = "Scale";
            // 
            // ScaleZ
            // 
            this.ScaleZ.Location = new System.Drawing.Point(148, 53);
            this.ScaleZ.Name = "ScaleZ";
            this.ScaleZ.Size = new System.Drawing.Size(27, 21);
            this.ScaleZ.TabIndex = 11;
            this.ScaleZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ScaleX
            // 
            this.ScaleX.Location = new System.Drawing.Point(82, 53);
            this.ScaleX.Name = "ScaleX";
            this.ScaleX.Size = new System.Drawing.Size(27, 21);
            this.ScaleX.TabIndex = 8;
            this.ScaleX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ScaleY
            // 
            this.ScaleY.Location = new System.Drawing.Point(115, 53);
            this.ScaleY.Name = "ScaleY";
            this.ScaleY.Size = new System.Drawing.Size(27, 21);
            this.ScaleY.TabIndex = 10;
            this.ScaleY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelRotation
            // 
            this.LabelRotation.AutoSize = true;
            this.LabelRotation.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelRotation.Location = new System.Drawing.Point(13, 27);
            this.LabelRotation.Name = "LabelRotation";
            this.LabelRotation.Size = new System.Drawing.Size(63, 14);
            this.LabelRotation.TabIndex = 5;
            this.LabelRotation.Text = "Rotation";
            // 
            // RotationZ
            // 
            this.RotationZ.Location = new System.Drawing.Point(148, 26);
            this.RotationZ.Name = "RotationZ";
            this.RotationZ.Size = new System.Drawing.Size(27, 21);
            this.RotationZ.TabIndex = 7;
            this.RotationZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RotationX
            // 
            this.RotationX.Location = new System.Drawing.Point(82, 26);
            this.RotationX.Name = "RotationX";
            this.RotationX.Size = new System.Drawing.Size(27, 21);
            this.RotationX.TabIndex = 4;
            this.RotationX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RotationY
            // 
            this.RotationY.Location = new System.Drawing.Point(115, 26);
            this.RotationY.Name = "RotationY";
            this.RotationY.Size = new System.Drawing.Size(27, 21);
            this.RotationY.TabIndex = 6;
            this.RotationY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BtnCtrlPanel
            // 
            this.BtnCtrlPanel.Location = new System.Drawing.Point(716, -1);
            this.BtnCtrlPanel.Name = "BtnCtrlPanel";
            this.BtnCtrlPanel.Size = new System.Drawing.Size(75, 18);
            this.BtnCtrlPanel.TabIndex = 5;
            this.BtnCtrlPanel.Text = "CtrlPanel";
            this.BtnCtrlPanel.UseVisualStyleBackColor = true;
            // 
            // LabelAmbient
            // 
            this.LabelAmbient.AutoSize = true;
            this.LabelAmbient.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelAmbient.Location = new System.Drawing.Point(13, 370);
            this.LabelAmbient.Name = "LabelAmbient";
            this.LabelAmbient.Size = new System.Drawing.Size(56, 14);
            this.LabelAmbient.TabIndex = 46;
            this.LabelAmbient.Text = "Ambient";
            // 
            // AmbientB
            // 
            this.AmbientB.Location = new System.Drawing.Point(148, 369);
            this.AmbientB.Name = "AmbientB";
            this.AmbientB.Size = new System.Drawing.Size(27, 21);
            this.AmbientB.TabIndex = 49;
            this.AmbientB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AmbientR
            // 
            this.AmbientR.Location = new System.Drawing.Point(82, 369);
            this.AmbientR.Name = "AmbientR";
            this.AmbientR.Size = new System.Drawing.Size(27, 21);
            this.AmbientR.TabIndex = 47;
            this.AmbientR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AmbientG
            // 
            this.AmbientG.Location = new System.Drawing.Point(115, 369);
            this.AmbientG.Name = "AmbientG";
            this.AmbientG.Size = new System.Drawing.Size(27, 21);
            this.AmbientG.TabIndex = 48;
            this.AmbientG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnCtrlPanel);
            this.Controls.Add(this.CtrlPanel);
            this.Name = "Form1";
            this.Text = "SoftRenderer by liangjuntu";
            this.CtrlPanel.ResumeLayout(false);
            this.CtrlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox PositionX;
        private System.Windows.Forms.Label LabelPosition;
        private System.Windows.Forms.TextBox PositionY;
        private System.Windows.Forms.TextBox PositionZ;
        private System.Windows.Forms.Panel CtrlPanel;
        private System.Windows.Forms.Label LabelScale;
        private System.Windows.Forms.TextBox ScaleZ;
        private System.Windows.Forms.TextBox ScaleX;
        private System.Windows.Forms.TextBox ScaleY;
        private System.Windows.Forms.Label LabelRotation;
        private System.Windows.Forms.TextBox RotationZ;
        private System.Windows.Forms.TextBox RotationX;
        private System.Windows.Forms.TextBox RotationY;
        private System.Windows.Forms.Button BtnCtrlPanel;
        private System.Windows.Forms.Label AutoRot;
        private System.Windows.Forms.TextBox AutoRotZ;
        private System.Windows.Forms.TextBox AutoRotX;
        private System.Windows.Forms.TextBox AutoRotY;
        private System.Windows.Forms.Label CamRot;
        private System.Windows.Forms.TextBox CamRotZ;
        private System.Windows.Forms.TextBox CamRotX;
        private System.Windows.Forms.TextBox CamRotY;
        private System.Windows.Forms.Label CamPos;
        private System.Windows.Forms.TextBox CamPosZ;
        private System.Windows.Forms.TextBox CamPosX;
        private System.Windows.Forms.TextBox CamPosY;
        private System.Windows.Forms.TextBox Far;
        private System.Windows.Forms.Label LabelCamFar;
        private System.Windows.Forms.TextBox Near;
        private System.Windows.Forms.Label LabelCamNear;
        private System.Windows.Forms.TextBox Fov;
        private System.Windows.Forms.Label LabelFov;
        private System.Windows.Forms.Button BtnTextureFilterMode;
        private System.Windows.Forms.Button BtnDrawMode;
        private System.Windows.Forms.Button BtnClippingMode;
        private System.Windows.Forms.Button BtnWinding;
        private System.Windows.Forms.Button BtnCullFrontEnd;
        private System.Windows.Forms.Button BtnCullMode;
        private System.Windows.Forms.Button BtnShadeMode;
        private System.Windows.Forms.Label LabelLightCol;
        private System.Windows.Forms.TextBox LightColB;
        private System.Windows.Forms.TextBox LightColR;
        private System.Windows.Forms.TextBox LightColG;
        private System.Windows.Forms.Label LabelLightDir;
        private System.Windows.Forms.TextBox LightDirZ;
        private System.Windows.Forms.TextBox LightDirX;
        private System.Windows.Forms.TextBox LightDirY;
        private System.Windows.Forms.TextBox AmbientB;
        private System.Windows.Forms.TextBox AmbientR;
        private System.Windows.Forms.TextBox AmbientG;
        private System.Windows.Forms.Label LabelAmbient;
    }
}

