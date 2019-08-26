namespace RecipeParsing
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPENDIRECTORYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sAVEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sAVEASToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mAKERECIPEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dataGridViewPara = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Parameter = new System.Windows.Forms.TabPage();
            this.Wiremap = new System.Windows.Forms.TabPage();
            this.dataGridViewWM = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPara)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Parameter.SuspendLayout();
            this.Wiremap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWM)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.oPENDIRECTORYToolStripMenuItem,
            this.sAVEToolStripMenuItem1,
            this.sAVEASToolStripMenuItem,
            this.eXITToolStripMenuItem,
            this.mAKERECIPEToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(40, 20);
            this.toolStripMenuItem1.Text = "FILE";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveToolStripMenuItem.Text = "OPEN";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // oPENDIRECTORYToolStripMenuItem
            // 
            this.oPENDIRECTORYToolStripMenuItem.Name = "oPENDIRECTORYToolStripMenuItem";
            this.oPENDIRECTORYToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.oPENDIRECTORYToolStripMenuItem.Text = "OPEN DIRECTORY";
            this.oPENDIRECTORYToolStripMenuItem.Click += new System.EventHandler(this.oPENDIRECTORYToolStripMenuItem_Click);
            // 
            // sAVEToolStripMenuItem1
            // 
            this.sAVEToolStripMenuItem1.Name = "sAVEToolStripMenuItem1";
            this.sAVEToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.sAVEToolStripMenuItem1.Text = "SAVE";
            this.sAVEToolStripMenuItem1.Click += new System.EventHandler(this.sAVEToolStripMenuItem1_Click);
            // 
            // sAVEASToolStripMenuItem
            // 
            this.sAVEASToolStripMenuItem.Name = "sAVEASToolStripMenuItem";
            this.sAVEASToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.sAVEASToolStripMenuItem.Text = "SAVE AS";
            this.sAVEASToolStripMenuItem.Click += new System.EventHandler(this.sAVEASToolStripMenuItem_Click);
            // 
            // eXITToolStripMenuItem
            // 
            this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            this.eXITToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.eXITToolStripMenuItem.Text = "EXIT";
            this.eXITToolStripMenuItem.Click += new System.EventHandler(this.eXITToolStripMenuItem_Click);
            // 
            // mAKERECIPEToolStripMenuItem
            // 
            this.mAKERECIPEToolStripMenuItem.Name = "mAKERECIPEToolStripMenuItem";
            this.mAKERECIPEToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.mAKERECIPEToolStripMenuItem.Text = "MAKE RECIPE";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1192, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dataGridViewPara
            // 
            this.dataGridViewPara.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPara.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPara.Location = new System.Drawing.Point(-4, 0);
            this.dataGridViewPara.Margin = new System.Windows.Forms.Padding(10);
            this.dataGridViewPara.Name = "dataGridViewPara";
            this.dataGridViewPara.RowTemplate.Height = 23;
            this.dataGridViewPara.Size = new System.Drawing.Size(1164, 550);
            this.dataGridViewPara.TabIndex = 2;
            this.dataGridViewPara.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPara_CellValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Location = new System.Drawing.Point(36, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1049, 43);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(923, 19);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 12);
            this.lblStatus.TabIndex = 4;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(249, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(168, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(87, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "삭제";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Parameter);
            this.tabControl1.Controls.Add(this.Wiremap);
            this.tabControl1.Location = new System.Drawing.Point(12, 76);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1168, 579);
            this.tabControl1.TabIndex = 4;
            // 
            // Parameter
            // 
            this.Parameter.Controls.Add(this.dataGridViewPara);
            this.Parameter.Location = new System.Drawing.Point(4, 22);
            this.Parameter.Name = "Parameter";
            this.Parameter.Padding = new System.Windows.Forms.Padding(3);
            this.Parameter.Size = new System.Drawing.Size(1160, 553);
            this.Parameter.TabIndex = 0;
            this.Parameter.Text = "Parameter";
            this.Parameter.UseVisualStyleBackColor = true;
            // 
            // Wiremap
            // 
            this.Wiremap.Controls.Add(this.dataGridViewWM);
            this.Wiremap.Location = new System.Drawing.Point(4, 22);
            this.Wiremap.Name = "Wiremap";
            this.Wiremap.Padding = new System.Windows.Forms.Padding(3);
            this.Wiremap.Size = new System.Drawing.Size(1160, 553);
            this.Wiremap.TabIndex = 1;
            this.Wiremap.Text = "Wiremap";
            this.Wiremap.UseVisualStyleBackColor = true;
            // 
            // dataGridViewWM
            // 
            this.dataGridViewWM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewWM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWM.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewWM.Margin = new System.Windows.Forms.Padding(10);
            this.dataGridViewWM.Name = "dataGridViewWM";
            this.dataGridViewWM.RowTemplate.Height = 23;
            this.dataGridViewWM.Size = new System.Drawing.Size(1160, 553);
            this.dataGridViewWM.TabIndex = 3;
            this.dataGridViewWM.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWM_CellValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 667);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPara)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Parameter.ResumeLayout(false);
            this.Wiremap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAVEToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView dataGridViewPara;
        private System.Windows.Forms.ToolStripMenuItem oPENDIRECTORYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mAKERECIPEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAVEASToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Parameter;
        private System.Windows.Forms.TabPage Wiremap;
        private System.Windows.Forms.DataGridView dataGridViewWM;
    }
}

