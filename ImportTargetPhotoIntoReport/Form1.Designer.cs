namespace ImportTargetPhotoIntoReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCoaFolder = new System.Windows.Forms.TextBox();
            this.BtnCoaFolderSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCscanFolder = new System.Windows.Forms.TextBox();
            this.BtnScanFolderSelect = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.PbValue = new System.Windows.Forms.ProgressBar();
            this.TxtStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "COA报告文件夹";
            // 
            // TxtCoaFolder
            // 
            this.TxtCoaFolder.Location = new System.Drawing.Point(101, 10);
            this.TxtCoaFolder.Name = "TxtCoaFolder";
            this.TxtCoaFolder.ReadOnly = true;
            this.TxtCoaFolder.Size = new System.Drawing.Size(525, 21);
            this.TxtCoaFolder.TabIndex = 1;
            // 
            // BtnCoaFolderSelect
            // 
            this.BtnCoaFolderSelect.Location = new System.Drawing.Point(632, 8);
            this.BtnCoaFolderSelect.Name = "BtnCoaFolderSelect";
            this.BtnCoaFolderSelect.Size = new System.Drawing.Size(75, 23);
            this.BtnCoaFolderSelect.TabIndex = 2;
            this.BtnCoaFolderSelect.Text = "浏览";
            this.BtnCoaFolderSelect.UseVisualStyleBackColor = true;
            this.BtnCoaFolderSelect.Click += new System.EventHandler(this.BtnCoaFolderSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "超声图片文件夹";
            // 
            // TxtCscanFolder
            // 
            this.TxtCscanFolder.Location = new System.Drawing.Point(101, 39);
            this.TxtCscanFolder.Name = "TxtCscanFolder";
            this.TxtCscanFolder.ReadOnly = true;
            this.TxtCscanFolder.Size = new System.Drawing.Size(525, 21);
            this.TxtCscanFolder.TabIndex = 1;
            // 
            // BtnScanFolderSelect
            // 
            this.BtnScanFolderSelect.Location = new System.Drawing.Point(632, 37);
            this.BtnScanFolderSelect.Name = "BtnScanFolderSelect";
            this.BtnScanFolderSelect.Size = new System.Drawing.Size(75, 23);
            this.BtnScanFolderSelect.TabIndex = 2;
            this.BtnScanFolderSelect.Text = "浏览";
            this.BtnScanFolderSelect.UseVisualStyleBackColor = true;
            this.BtnScanFolderSelect.Click += new System.EventHandler(this.BtnScanFolderSelect_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(535, 76);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(172, 36);
            this.BtnStart.TabIndex = 3;
            this.BtnStart.Text = "开始处理";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // PbValue
            // 
            this.PbValue.Location = new System.Drawing.Point(12, 118);
            this.PbValue.Name = "PbValue";
            this.PbValue.Size = new System.Drawing.Size(695, 23);
            this.PbValue.TabIndex = 4;
            // 
            // TxtStatus
            // 
            this.TxtStatus.Location = new System.Drawing.Point(14, 157);
            this.TxtStatus.Multiline = true;
            this.TxtStatus.Name = "TxtStatus";
            this.TxtStatus.ReadOnly = true;
            this.TxtStatus.Size = new System.Drawing.Size(693, 334);
            this.TxtStatus.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 503);
            this.Controls.Add(this.TxtStatus);
            this.Controls.Add(this.PbValue);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.BtnScanFolderSelect);
            this.Controls.Add(this.TxtCscanFolder);
            this.Controls.Add(this.BtnCoaFolderSelect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtCoaFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "COA报告超声照片导入程序 designed by xs.zhou";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCoaFolder;
        private System.Windows.Forms.Button BtnCoaFolderSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCscanFolder;
        private System.Windows.Forms.Button BtnScanFolderSelect;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.ProgressBar PbValue;
        private System.Windows.Forms.TextBox TxtStatus;
    }
}

