namespace GalleryOfCScanImage
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
            this.TxtImageFolder = new System.Windows.Forms.TextBox();
            this.BtnSelectImageFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtOutputFolder = new System.Windows.Forms.TextBox();
            this.BtnSelectOutputFolder = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.DtpStart = new System.Windows.Forms.DateTimePicker();
            this.DtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ChkIsOpen = new System.Windows.Forms.CheckBox();
            this.TxtStatus = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ChkShowProcessDetails = new System.Windows.Forms.CheckBox();
            this.BtnThisMonth = new System.Windows.Forms.Button();
            this.BtnLastMonth = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "图片文件位置";
            // 
            // TxtImageFolder
            // 
            this.TxtImageFolder.Location = new System.Drawing.Point(121, 90);
            this.TxtImageFolder.Name = "TxtImageFolder";
            this.TxtImageFolder.ReadOnly = true;
            this.TxtImageFolder.Size = new System.Drawing.Size(461, 25);
            this.TxtImageFolder.TabIndex = 1;
            // 
            // BtnSelectImageFolder
            // 
            this.BtnSelectImageFolder.Location = new System.Drawing.Point(588, 90);
            this.BtnSelectImageFolder.Name = "BtnSelectImageFolder";
            this.BtnSelectImageFolder.Size = new System.Drawing.Size(77, 25);
            this.BtnSelectImageFolder.TabIndex = 2;
            this.BtnSelectImageFolder.Text = "选择";
            this.BtnSelectImageFolder.UseVisualStyleBackColor = true;
            this.BtnSelectImageFolder.Click += new System.EventHandler(this.BtnSelectImageFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "输出文件位置";
            // 
            // TxtOutputFolder
            // 
            this.TxtOutputFolder.Location = new System.Drawing.Point(121, 126);
            this.TxtOutputFolder.Name = "TxtOutputFolder";
            this.TxtOutputFolder.ReadOnly = true;
            this.TxtOutputFolder.Size = new System.Drawing.Size(461, 25);
            this.TxtOutputFolder.TabIndex = 1;
            // 
            // BtnSelectOutputFolder
            // 
            this.BtnSelectOutputFolder.Location = new System.Drawing.Point(588, 128);
            this.BtnSelectOutputFolder.Name = "BtnSelectOutputFolder";
            this.BtnSelectOutputFolder.Size = new System.Drawing.Size(77, 25);
            this.BtnSelectOutputFolder.TabIndex = 2;
            this.BtnSelectOutputFolder.Text = "选择";
            this.BtnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.BtnSelectOutputFolder.Click += new System.EventHandler(this.BtnSelectOutputFolder_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(569, 220);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(115, 34);
            this.BtnStart.TabIndex = 2;
            this.BtnStart.Text = "开始";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // DtpStart
            // 
            this.DtpStart.Location = new System.Drawing.Point(122, 162);
            this.DtpStart.Name = "DtpStart";
            this.DtpStart.Size = new System.Drawing.Size(143, 25);
            this.DtpStart.TabIndex = 4;
            this.DtpStart.ValueChanged += new System.EventHandler(this.DtpStart_ValueChanged);
            // 
            // DtpEnd
            // 
            this.DtpEnd.Location = new System.Drawing.Point(271, 162);
            this.DtpEnd.Name = "DtpEnd";
            this.DtpEnd.Size = new System.Drawing.Size(148, 25);
            this.DtpEnd.TabIndex = 4;
            this.DtpEnd.ValueChanged += new System.EventHandler(this.DtpEnd_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "绑定时间范围";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DtpEnd);
            this.groupBox1.Controls.Add(this.TxtImageFolder);
            this.groupBox1.Controls.Add(this.DtpStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BtnSelectImageFolder);
            this.groupBox1.Controls.Add(this.BtnLastMonth);
            this.groupBox1.Controls.Add(this.BtnThisMonth);
            this.groupBox1.Controls.Add(this.BtnSelectOutputFolder);
            this.groupBox1.Controls.Add(this.TxtOutputFolder);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 203);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本设置";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Yellow;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("宋体", 9F);
            this.label4.Location = new System.Drawing.Point(13, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(652, 48);
            this.label4.TabIndex = 0;
            this.label4.Text = "先从服务器下载全部绑定超声照片到本地,此工具会自动生成选定日期范围内[已完成]的230mm靶材的图集.\r\n";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChkIsOpen
            // 
            this.ChkIsOpen.AutoSize = true;
            this.ChkIsOpen.Checked = true;
            this.ChkIsOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIsOpen.Location = new System.Drawing.Point(453, 229);
            this.ChkIsOpen.Name = "ChkIsOpen";
            this.ChkIsOpen.Size = new System.Drawing.Size(104, 19);
            this.ChkIsOpen.TabIndex = 6;
            this.ChkIsOpen.Text = "生成后打开";
            this.ChkIsOpen.UseVisualStyleBackColor = true;
            // 
            // TxtStatus
            // 
            this.TxtStatus.Location = new System.Drawing.Point(13, 261);
            this.TxtStatus.MaxLength = 3276700;
            this.TxtStatus.Multiline = true;
            this.TxtStatus.Name = "TxtStatus";
            this.TxtStatus.ReadOnly = true;
            this.TxtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtStatus.Size = new System.Drawing.Size(670, 207);
            this.TxtStatus.TabIndex = 7;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.TSProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 471);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(699, 25);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(110, 20);
            this.toolStripStatusLabel1.Text = "需连接PMS服务";
            // 
            // TSProgressBar
            // 
            this.TSProgressBar.Name = "TSProgressBar";
            this.TSProgressBar.Size = new System.Drawing.Size(300, 19);
            // 
            // ChkShowProcessDetails
            // 
            this.ChkShowProcessDetails.AutoSize = true;
            this.ChkShowProcessDetails.Checked = true;
            this.ChkShowProcessDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkShowProcessDetails.Location = new System.Drawing.Point(283, 229);
            this.ChkShowProcessDetails.Name = "ChkShowProcessDetails";
            this.ChkShowProcessDetails.Size = new System.Drawing.Size(149, 19);
            this.ChkShowProcessDetails.TabIndex = 6;
            this.ChkShowProcessDetails.Text = "显示详细处理过程";
            this.ChkShowProcessDetails.UseVisualStyleBackColor = true;
            // 
            // BtnThisMonth
            // 
            this.BtnThisMonth.Location = new System.Drawing.Point(425, 162);
            this.BtnThisMonth.Name = "BtnThisMonth";
            this.BtnThisMonth.Size = new System.Drawing.Size(77, 25);
            this.BtnThisMonth.TabIndex = 2;
            this.BtnThisMonth.Text = "本月";
            this.BtnThisMonth.UseVisualStyleBackColor = true;
            this.BtnThisMonth.Click += new System.EventHandler(this.BtnThisMonth_Click);
            // 
            // BtnLastMonth
            // 
            this.BtnLastMonth.Location = new System.Drawing.Point(505, 162);
            this.BtnLastMonth.Name = "BtnLastMonth";
            this.BtnLastMonth.Size = new System.Drawing.Size(77, 25);
            this.BtnLastMonth.TabIndex = 2;
            this.BtnLastMonth.Text = "上月";
            this.BtnLastMonth.UseVisualStyleBackColor = true;
            this.BtnLastMonth.Click += new System.EventHandler(this.BtnLastMonth_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 496);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.TxtStatus);
            this.Controls.Add(this.ChkShowProcessDetails);
            this.Controls.Add(this.ChkIsOpen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "超声图片汇集工具 for 230mm V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtImageFolder;
        private System.Windows.Forms.Button BtnSelectImageFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtOutputFolder;
        private System.Windows.Forms.Button BtnSelectOutputFolder;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.DateTimePicker DtpStart;
        private System.Windows.Forms.DateTimePicker DtpEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkIsOpen;
        private System.Windows.Forms.TextBox TxtStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar TSProgressBar;
        private System.Windows.Forms.CheckBox ChkShowProcessDetails;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnLastMonth;
        private System.Windows.Forms.Button BtnThisMonth;
    }
}

