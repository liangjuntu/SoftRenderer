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
            this.SuspendLayout();
            // 
            // PositionX
            // 
            this.PositionX.Location = new System.Drawing.Point(698, 16);
            this.PositionX.Name = "PositionX";
            this.PositionX.Size = new System.Drawing.Size(27, 21);
            this.PositionX.TabIndex = 0;
            this.PositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelPosition
            // 
            this.LabelPosition.AutoSize = true;
            this.LabelPosition.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelPosition.Location = new System.Drawing.Point(629, 17);
            this.LabelPosition.Name = "LabelPosition";
            this.LabelPosition.Size = new System.Drawing.Size(63, 14);
            this.LabelPosition.TabIndex = 1;
            this.LabelPosition.Text = "Position";
            // 
            // PositionY
            // 
            this.PositionY.Location = new System.Drawing.Point(731, 16);
            this.PositionY.Name = "PositionY";
            this.PositionY.Size = new System.Drawing.Size(27, 21);
            this.PositionY.TabIndex = 2;
            this.PositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PositionZ
            // 
            this.PositionZ.Location = new System.Drawing.Point(764, 16);
            this.PositionZ.Name = "PositionZ";
            this.PositionZ.Size = new System.Drawing.Size(27, 21);
            this.PositionZ.TabIndex = 3;
            this.PositionZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PositionZ);
            this.Controls.Add(this.PositionY);
            this.Controls.Add(this.LabelPosition);
            this.Controls.Add(this.PositionX);
            this.Name = "Form1";
            this.Text = "SoftRenderer by liangjuntu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PositionX;
        private System.Windows.Forms.Label LabelPosition;
        private System.Windows.Forms.TextBox PositionY;
        private System.Windows.Forms.TextBox PositionZ;
    }
}

