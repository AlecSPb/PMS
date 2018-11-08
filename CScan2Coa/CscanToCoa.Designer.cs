namespace ImportTargetPhotoIntoReport
{
    partial class CscanToCoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CscanToCoa));
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCoaFolder = new System.Windows.Forms.TextBox();
            this.BtnCoaFolderSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCscanFolder = new System.Windows.Forms.TextBox();
            this.BtnScanFolderSelect = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.PbValue = new System.Windows.Forms.ProgressBar();
            this.TxtStatus = new System.Windows.Forms.TextBox();
            this.chkOpenOutput = new System.Windows.Forms.CheckBox();
            this.ChkToPdf = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "COA报告文件夹";
            // 
            // TxtCoaFolder
            // 
            this.TxtCoaFolder.Location = new System.Drawing.Point(135, 12);
            this.TxtCoaFolder.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCoaFolder.Name = "TxtCoaFolder";
            this.TxtCoaFolder.ReadOnly = true;
            this.TxtCoaFolder.Size = new System.Drawing.Size(1005, 25);
            this.TxtCoaFolder.TabIndex = 1;
            // 
            // BtnCoaFolderSelect
            // 
            this.BtnCoaFolderSelect.Location = new System.Drawing.Point(1148, 8);
            this.BtnCoaFolderSelect.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCoaFolderSelect.Name = "BtnCoaFolderSelect";
            this.BtnCoaFolderSelect.Size = new System.Drawing.Size(100, 29);
            this.BtnCoaFolderSelect.TabIndex = 2;
            this.BtnCoaFolderSelect.Text = "浏览";
            this.BtnCoaFolderSelect.UseVisualStyleBackColor = true;
            this.BtnCoaFolderSelect.Click += new System.EventHandler(this.BtnCoaFolderSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "超声图片文件夹";
            // 
            // TxtCscanFolder
            // 
            this.TxtCscanFolder.Location = new System.Drawing.Point(135, 49);
            this.TxtCscanFolder.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCscanFolder.Name = "TxtCscanFolder";
            this.TxtCscanFolder.ReadOnly = true;
            this.TxtCscanFolder.Size = new System.Drawing.Size(1005, 25);
            this.TxtCscanFolder.TabIndex = 1;
            // 
            // BtnScanFolderSelect
            // 
            this.BtnScanFolderSelect.Location = new System.Drawing.Point(1148, 44);
            this.BtnScanFolderSelect.Margin = new System.Windows.Forms.Padding(4);
            this.BtnScanFolderSelect.Name = "BtnScanFolderSelect";
            this.BtnScanFolderSelect.Size = new System.Drawing.Size(100, 29);
            this.BtnScanFolderSelect.TabIndex = 2;
            this.BtnScanFolderSelect.Text = "浏览";
            this.BtnScanFolderSelect.UseVisualStyleBackColor = true;
            this.BtnScanFolderSelect.Click += new System.EventHandler(this.BtnScanFolderSelect_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(1061, 95);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(187, 45);
            this.BtnStart.TabIndex = 3;
            this.BtnStart.Text = "开始处理";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // PbValue
            // 
            this.PbValue.Location = new System.Drawing.Point(19, 102);
            this.PbValue.Margin = new System.Windows.Forms.Padding(4);
            this.PbValue.Name = "PbValue";
            this.PbValue.Size = new System.Drawing.Size(705, 29);
            this.PbValue.TabIndex = 4;
            // 
            // TxtStatus
            // 
            this.TxtStatus.Location = new System.Drawing.Point(19, 148);
            this.TxtStatus.Margin = new System.Windows.Forms.Padding(4);
            this.TxtStatus.Multiline = true;
            this.TxtStatus.Name = "TxtStatus";
            this.TxtStatus.ReadOnly = true;
            this.TxtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtStatus.Size = new System.Drawing.Size(1229, 636);
            this.TxtStatus.TabIndex = 5;
            // 
            // chkOpenOutput
            // 
            this.chkOpenOutput.AutoSize = true;
            this.chkOpenOutput.Checked = true;
            this.chkOpenOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenOutput.Location = new System.Drawing.Point(850, 108);
            this.chkOpenOutput.Name = "chkOpenOutput";
            this.chkOpenOutput.Size = new System.Drawing.Size(179, 19);
            this.chkOpenOutput.TabIndex = 7;
            this.chkOpenOutput.Text = "完成后打开目标文件夹";
            this.chkOpenOutput.UseVisualStyleBackColor = true;
            // 
            // ChkToPdf
            // 
            this.ChkToPdf.AutoSize = true;
            this.ChkToPdf.Location = new System.Drawing.Point(731, 108);
            this.ChkToPdf.Name = "ChkToPdf";
            this.ChkToPdf.Size = new System.Drawing.Size(113, 19);
            this.ChkToPdf.TabIndex = 7;
            this.ChkToPdf.Text = "同时生成pdf";
            this.ChkToPdf.UseVisualStyleBackColor = true;
            // 
            // CscanToCoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 792);
            this.Controls.Add(this.PbValue);
            this.Controls.Add(this.ChkToPdf);
            this.Controls.Add(this.chkOpenOutput);
            this.Controls.Add(this.TxtStatus);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.BtnScanFolderSelect);
            this.Controls.Add(this.TxtCscanFolder);
            this.Controls.Add(this.BtnCoaFolderSelect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtCoaFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CscanToCoa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CscanToCoa_FormClosing);
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
        private System.Windows.Forms.CheckBox chkOpenOutput;
        private System.Windows.Forms.CheckBox ChkToPdf;
    }
}

