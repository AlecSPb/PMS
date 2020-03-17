namespace CSCANCalcualtor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtRangeA = new System.Windows.Forms.TextBox();
            this.TxtSpeedA = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtRangeB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtSpeedB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnCalculate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LVSoundSpeed = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.BtnDataFile = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(18, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "距离";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(18, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "声速";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtRangeA);
            this.groupBox1.Controls.Add(this.TxtSpeedA);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 15F);
            this.groupBox1.Location = new System.Drawing.Point(18, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 161);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "介质A(材料)";
            // 
            // TxtRangeA
            // 
            this.TxtRangeA.BackColor = System.Drawing.Color.LightYellow;
            this.TxtRangeA.Location = new System.Drawing.Point(89, 97);
            this.TxtRangeA.Name = "TxtRangeA";
            this.TxtRangeA.Size = new System.Drawing.Size(158, 36);
            this.TxtRangeA.TabIndex = 1;
            this.TxtRangeA.Text = "5";
            this.TxtRangeA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtSpeedA
            // 
            this.TxtSpeedA.BackColor = System.Drawing.Color.LightYellow;
            this.TxtSpeedA.Location = new System.Drawing.Point(89, 45);
            this.TxtSpeedA.Name = "TxtSpeedA";
            this.TxtSpeedA.Size = new System.Drawing.Size(158, 36);
            this.TxtSpeedA.TabIndex = 0;
            this.TxtSpeedA.Text = "3500";
            this.TxtSpeedA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtRangeB);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.TxtSpeedB);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 15F);
            this.groupBox2.Location = new System.Drawing.Point(18, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 161);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "介质B(水)";
            // 
            // TxtRangeB
            // 
            this.TxtRangeB.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TxtRangeB.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.TxtRangeB.Location = new System.Drawing.Point(89, 97);
            this.TxtRangeB.Name = "TxtRangeB";
            this.TxtRangeB.ReadOnly = true;
            this.TxtRangeB.Size = new System.Drawing.Size(158, 36);
            this.TxtRangeB.TabIndex = 1;
            this.TxtRangeB.Text = "?";
            this.TxtRangeB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(15, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "声速";
            // 
            // TxtSpeedB
            // 
            this.TxtSpeedB.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TxtSpeedB.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtSpeedB.Location = new System.Drawing.Point(89, 45);
            this.TxtSpeedB.Name = "TxtSpeedB";
            this.TxtSpeedB.ReadOnly = true;
            this.TxtSpeedB.Size = new System.Drawing.Size(158, 36);
            this.TxtSpeedB.TabIndex = 1;
            this.TxtSpeedB.Text = "1480";
            this.TxtSpeedB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(15, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "距离";
            // 
            // BtnCalculate
            // 
            this.BtnCalculate.Font = new System.Drawing.Font("宋体", 20F);
            this.BtnCalculate.Location = new System.Drawing.Point(18, 366);
            this.BtnCalculate.Name = "BtnCalculate";
            this.BtnCalculate.Size = new System.Drawing.Size(266, 78);
            this.BtnCalculate.TabIndex = 6;
            this.BtnCalculate.Text = "换算";
            this.BtnCalculate.UseVisualStyleBackColor = true;
            this.BtnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnRefresh);
            this.groupBox3.Controls.Add(this.BtnDataFile);
            this.groupBox3.Controls.Add(this.LVSoundSpeed);
            this.groupBox3.Location = new System.Drawing.Point(301, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(569, 529);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "常用介质声速-单击或上下箭头选择";
            // 
            // LVSoundSpeed
            // 
            this.LVSoundSpeed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.LVSoundSpeed.Font = new System.Drawing.Font("宋体", 10F);
            this.LVSoundSpeed.FullRowSelect = true;
            this.LVSoundSpeed.GridLines = true;
            this.LVSoundSpeed.Location = new System.Drawing.Point(16, 53);
            this.LVSoundSpeed.Name = "LVSoundSpeed";
            this.LVSoundSpeed.Size = new System.Drawing.Size(540, 462);
            this.LVSoundSpeed.TabIndex = 4;
            this.LVSoundSpeed.UseCompatibleStateImageBehavior = false;
            this.LVSoundSpeed.View = System.Windows.Forms.View.Details;
            this.LVSoundSpeed.SelectedIndexChanged += new System.EventHandler(this.LVSoundSpeed_SelectedIndexChanged);
            this.LVSoundSpeed.Click += new System.EventHandler(this.LVSoundSpeed_Click);
            this.LVSoundSpeed.DoubleClick += new System.EventHandler(this.LVSoundSpeed_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "介质";
            this.columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "声速";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label5.Location = new System.Drawing.Point(59, 455);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "可直接回车进行计算";
            // 
            // BtnDataFile
            // 
            this.BtnDataFile.Location = new System.Drawing.Point(270, 15);
            this.BtnDataFile.Name = "BtnDataFile";
            this.BtnDataFile.Size = new System.Drawing.Size(140, 33);
            this.BtnDataFile.TabIndex = 7;
            this.BtnDataFile.Text = "修改数据";
            this.BtnDataFile.UseVisualStyleBackColor = true;
            this.BtnDataFile.Click += new System.EventHandler(this.BtnDataFile_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(420, 15);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(134, 33);
            this.BtnRefresh.TabIndex = 7;
            this.BtnRefresh.Text = "刷新数据";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.BtnCalculate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.BtnCalculate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "介质中的超声焦点移动距离换算工具 designed by xs.zhou";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TxtRangeA;
        private System.Windows.Forms.TextBox TxtSpeedA;
        private System.Windows.Forms.TextBox TxtRangeB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtSpeedB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnCalculate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView LVSoundSpeed;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnDataFile;
        private System.Windows.Forms.Button BtnRefresh;
    }
}

