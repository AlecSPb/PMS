namespace ImportTargetPhotoIntoReport
{
    partial class CscanMarker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CscanMarker));
            this.label1 = new System.Windows.Forms.Label();
            this.TxtPhotos = new System.Windows.Forms.TextBox();
            this.BtnFolder = new System.Windows.Forms.Button();
            this.PbValue = new System.Windows.Forms.ProgressBar();
            this.CkProductID = new System.Windows.Forms.CheckBox();
            this.CKComposition = new System.Windows.Forms.CheckBox();
            this.CKWeldingRation = new System.Windows.Forms.CheckBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.TxtStatus = new System.Windows.Forms.TextBox();
            this.chkOpenOutput = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "CSCAN图像文件夹";
            // 
            // TxtPhotos
            // 
            this.TxtPhotos.Location = new System.Drawing.Point(135, 11);
            this.TxtPhotos.Name = "TxtPhotos";
            this.TxtPhotos.ReadOnly = true;
            this.TxtPhotos.Size = new System.Drawing.Size(656, 25);
            this.TxtPhotos.TabIndex = 1;
            // 
            // BtnFolder
            // 
            this.BtnFolder.Location = new System.Drawing.Point(806, 7);
            this.BtnFolder.Name = "BtnFolder";
            this.BtnFolder.Size = new System.Drawing.Size(86, 32);
            this.BtnFolder.TabIndex = 2;
            this.BtnFolder.Text = "浏览";
            this.BtnFolder.UseVisualStyleBackColor = true;
            this.BtnFolder.Click += new System.EventHandler(this.BtnFolder_Click);
            // 
            // PbValue
            // 
            this.PbValue.Location = new System.Drawing.Point(12, 91);
            this.PbValue.Name = "PbValue";
            this.PbValue.Size = new System.Drawing.Size(880, 23);
            this.PbValue.TabIndex = 3;
            // 
            // CkProductID
            // 
            this.CkProductID.AutoSize = true;
            this.CkProductID.Checked = true;
            this.CkProductID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CkProductID.Location = new System.Drawing.Point(61, 51);
            this.CkProductID.Name = "CkProductID";
            this.CkProductID.Size = new System.Drawing.Size(75, 19);
            this.CkProductID.TabIndex = 4;
            this.CkProductID.Text = "产品ID";
            this.CkProductID.UseVisualStyleBackColor = true;
            this.CkProductID.CheckedChanged += new System.EventHandler(this.CkProductID_CheckedChanged);
            // 
            // CKComposition
            // 
            this.CKComposition.AutoSize = true;
            this.CKComposition.Checked = true;
            this.CKComposition.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CKComposition.Location = new System.Drawing.Point(142, 51);
            this.CKComposition.Name = "CKComposition";
            this.CKComposition.Size = new System.Drawing.Size(59, 19);
            this.CKComposition.TabIndex = 4;
            this.CKComposition.Text = "成分";
            this.CKComposition.UseVisualStyleBackColor = true;
            this.CKComposition.CheckedChanged += new System.EventHandler(this.CKComposition_CheckedChanged);
            // 
            // CKWeldingRation
            // 
            this.CKWeldingRation.AutoSize = true;
            this.CKWeldingRation.Checked = true;
            this.CKWeldingRation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CKWeldingRation.Location = new System.Drawing.Point(207, 51);
            this.CKWeldingRation.Name = "CKWeldingRation";
            this.CKWeldingRation.Size = new System.Drawing.Size(74, 19);
            this.CKWeldingRation.TabIndex = 4;
            this.CKWeldingRation.Text = "焊合率";
            this.CKWeldingRation.UseVisualStyleBackColor = true;
            this.CKWeldingRation.CheckedChanged += new System.EventHandler(this.CKWeldingRation_CheckedChanged);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(695, 40);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(197, 39);
            this.BtnStart.TabIndex = 5;
            this.BtnStart.Text = "开始处理";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // TxtStatus
            // 
            this.TxtStatus.Location = new System.Drawing.Point(13, 121);
            this.TxtStatus.Margin = new System.Windows.Forms.Padding(4);
            this.TxtStatus.Multiline = true;
            this.TxtStatus.Name = "TxtStatus";
            this.TxtStatus.ReadOnly = true;
            this.TxtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtStatus.Size = new System.Drawing.Size(879, 464);
            this.TxtStatus.TabIndex = 6;
            // 
            // chkOpenOutput
            // 
            this.chkOpenOutput.AutoSize = true;
            this.chkOpenOutput.Checked = true;
            this.chkOpenOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenOutput.Location = new System.Drawing.Point(498, 50);
            this.chkOpenOutput.Name = "chkOpenOutput";
            this.chkOpenOutput.Size = new System.Drawing.Size(179, 19);
            this.chkOpenOutput.TabIndex = 8;
            this.chkOpenOutput.Text = "完成后打开目标文件夹";
            this.chkOpenOutput.UseVisualStyleBackColor = true;
            this.chkOpenOutput.CheckedChanged += new System.EventHandler(this.chkOpenOutput_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "选项";
            // 
            // CscanMarker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 598);
            this.Controls.Add(this.chkOpenOutput);
            this.Controls.Add(this.TxtStatus);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.CKWeldingRation);
            this.Controls.Add(this.CKComposition);
            this.Controls.Add(this.CkProductID);
            this.Controls.Add(this.PbValue);
            this.Controls.Add(this.BtnFolder);
            this.Controls.Add(this.TxtPhotos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CscanMarker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPhotos;
        private System.Windows.Forms.Button BtnFolder;
        private System.Windows.Forms.ProgressBar PbValue;
        private System.Windows.Forms.CheckBox CkProductID;
        private System.Windows.Forms.CheckBox CKComposition;
        private System.Windows.Forms.CheckBox CKWeldingRation;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.TextBox TxtStatus;
        private System.Windows.Forms.CheckBox chkOpenOutput;
        private System.Windows.Forms.Label label2;
    }
}