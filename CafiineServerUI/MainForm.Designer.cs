namespace CafiineServerUI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.bwkMain = new System.ComponentModel.BackgroundWorker();
            this.grbGameFiles = new System.Windows.Forms.GroupBox();
            this.lsvGameFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsGameFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.replaceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceSelectedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestSelectedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSelectedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsvLog = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.replaceSelectedFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.requestSelectedFiletoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbGameFiles.SuspendLayout();
            this.cmsGameFiles.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bwkMain
            // 
            this.bwkMain.WorkerReportsProgress = true;
            this.bwkMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwkMain_DoWork);
            this.bwkMain.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwkMain_ProgressChanged);
            // 
            // grbGameFiles
            // 
            this.grbGameFiles.Controls.Add(this.lsvGameFiles);
            this.grbGameFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbGameFiles.Location = new System.Drawing.Point(0, 0);
            this.grbGameFiles.Margin = new System.Windows.Forms.Padding(4);
            this.grbGameFiles.Name = "grbGameFiles";
            this.grbGameFiles.Padding = new System.Windows.Forms.Padding(4);
            this.grbGameFiles.Size = new System.Drawing.Size(599, 359);
            this.grbGameFiles.TabIndex = 2;
            this.grbGameFiles.TabStop = false;
            this.grbGameFiles.Text = "Game files (Not connected)";
            // 
            // lsvGameFiles
            // 
            this.lsvGameFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvGameFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvGameFiles.ContextMenuStrip = this.cmsGameFiles;
            this.lsvGameFiles.FullRowSelect = true;
            this.lsvGameFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvGameFiles.Location = new System.Drawing.Point(8, 23);
            this.lsvGameFiles.Margin = new System.Windows.Forms.Padding(4);
            this.lsvGameFiles.MultiSelect = false;
            this.lsvGameFiles.Name = "lsvGameFiles";
            this.lsvGameFiles.Size = new System.Drawing.Size(582, 327);
            this.lsvGameFiles.SmallImageList = this.imageList;
            this.lsvGameFiles.TabIndex = 3;
            this.lsvGameFiles.UseCompatibleStateImageBehavior = false;
            this.lsvGameFiles.View = System.Windows.Forms.View.Details;
            this.lsvGameFiles.SelectedIndexChanged += new System.EventHandler(this.lsvGameFiles_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 500;
            // 
            // cmsGameFiles
            // 
            this.cmsGameFiles.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsGameFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceFileToolStripMenuItem,
            this.requestFileToolStripMenuItem,
            this.openToolStripMenuItem});
            this.cmsGameFiles.Name = "cmsGameFiles";
            this.cmsGameFiles.Size = new System.Drawing.Size(147, 82);
            // 
            // replaceFileToolStripMenuItem
            // 
            this.replaceFileToolStripMenuItem.Enabled = false;
            this.replaceFileToolStripMenuItem.Image = global::CafiineServerUI.Properties.Resources.Copy;
            this.replaceFileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.replaceFileToolStripMenuItem.Name = "replaceFileToolStripMenuItem";
            this.replaceFileToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.replaceFileToolStripMenuItem.Text = "Replace...";
            this.replaceFileToolStripMenuItem.Click += new System.EventHandler(this.replaceFileToolStripMenuItem_Click);
            // 
            // requestFileToolStripMenuItem
            // 
            this.requestFileToolStripMenuItem.Enabled = false;
            this.requestFileToolStripMenuItem.Image = global::CafiineServerUI.Properties.Resources.DownloadDocument;
            this.requestFileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.requestFileToolStripMenuItem.Name = "requestFileToolStripMenuItem";
            this.requestFileToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.requestFileToolStripMenuItem.Text = "Request...";
            this.requestFileToolStripMenuItem.Click += new System.EventHandler(this.requestFileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Enabled = false;
            this.openToolStripMenuItem.Image = global::CafiineServerUI.Properties.Resources.openfolder_24;
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "infoBubble.bmp");
            this.imageList.Images.SetKeyName(1, "109_AllAnnotations_Error_16x16_72.png");
            this.imageList.Images.SetKeyName(2, "Copy.bmp");
            this.imageList.Images.SetKeyName(3, "DownloadDocument.bmp");
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(940, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceSelectedFileToolStripMenuItem,
            this.requestSelectedFileToolStripMenuItem,
            this.openSelectedFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // replaceSelectedFileToolStripMenuItem
            // 
            this.replaceSelectedFileToolStripMenuItem.Enabled = false;
            this.replaceSelectedFileToolStripMenuItem.Image = global::CafiineServerUI.Properties.Resources.Copy;
            this.replaceSelectedFileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.replaceSelectedFileToolStripMenuItem.Name = "replaceSelectedFileToolStripMenuItem";
            this.replaceSelectedFileToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.replaceSelectedFileToolStripMenuItem.Text = "Replace selected file...";
            this.replaceSelectedFileToolStripMenuItem.Click += new System.EventHandler(this.replaceFileToolStripMenuItem_Click);
            // 
            // requestSelectedFileToolStripMenuItem
            // 
            this.requestSelectedFileToolStripMenuItem.Enabled = false;
            this.requestSelectedFileToolStripMenuItem.Image = global::CafiineServerUI.Properties.Resources.DownloadDocument;
            this.requestSelectedFileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.requestSelectedFileToolStripMenuItem.Name = "requestSelectedFileToolStripMenuItem";
            this.requestSelectedFileToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.requestSelectedFileToolStripMenuItem.Text = "Request selected file...";
            this.requestSelectedFileToolStripMenuItem.Click += new System.EventHandler(this.requestFileToolStripMenuItem_Click);
            // 
            // openSelectedFileToolStripMenuItem
            // 
            this.openSelectedFileToolStripMenuItem.Enabled = false;
            this.openSelectedFileToolStripMenuItem.Image = global::CafiineServerUI.Properties.Resources.openfolder_24;
            this.openSelectedFileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.openSelectedFileToolStripMenuItem.Name = "openSelectedFileToolStripMenuItem";
            this.openSelectedFileToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.openSelectedFileToolStripMenuItem.Text = "Open selected file";
            this.openSelectedFileToolStripMenuItem.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(227, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsvLog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(337, 359);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // lsvLog
            // 
            this.lsvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lsvLog.FullRowSelect = true;
            this.lsvLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvLog.Location = new System.Drawing.Point(8, 23);
            this.lsvLog.Margin = new System.Windows.Forms.Padding(4);
            this.lsvLog.MultiSelect = false;
            this.lsvLog.Name = "lsvLog";
            this.lsvLog.Size = new System.Drawing.Size(320, 327);
            this.lsvLog.SmallImageList = this.imageList;
            this.lsvLog.TabIndex = 3;
            this.lsvLog.UseCompatibleStateImageBehavior = false;
            this.lsvLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 500;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceSelectedFileToolStripButton,
            this.requestSelectedFiletoolStripButton,
            this.openToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(940, 27);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // replaceSelectedFileToolStripButton
            // 
            this.replaceSelectedFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.replaceSelectedFileToolStripButton.Enabled = false;
            this.replaceSelectedFileToolStripButton.Image = global::CafiineServerUI.Properties.Resources.Copy;
            this.replaceSelectedFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.replaceSelectedFileToolStripButton.Name = "replaceSelectedFileToolStripButton";
            this.replaceSelectedFileToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.replaceSelectedFileToolStripButton.Text = "Replace selected file...";
            this.replaceSelectedFileToolStripButton.Click += new System.EventHandler(this.replaceFileToolStripMenuItem_Click);
            // 
            // requestSelectedFiletoolStripButton
            // 
            this.requestSelectedFiletoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.requestSelectedFiletoolStripButton.Enabled = false;
            this.requestSelectedFiletoolStripButton.Image = global::CafiineServerUI.Properties.Resources.DownloadDocument;
            this.requestSelectedFiletoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.requestSelectedFiletoolStripButton.Name = "requestSelectedFiletoolStripButton";
            this.requestSelectedFiletoolStripButton.Size = new System.Drawing.Size(24, 24);
            this.requestSelectedFiletoolStripButton.Text = "Request selected file...";
            this.requestSelectedFiletoolStripButton.Click += new System.EventHandler(this.requestFileToolStripMenuItem_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Enabled = false;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbGameFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(940, 359);
            this.splitContainer1.SplitterDistance = 599;
            this.splitContainer1.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 414);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Cafiine Server UI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grbGameFiles.ResumeLayout(false);
            this.cmsGameFiles.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwkMain;
        private System.Windows.Forms.GroupBox grbGameFiles;
        private System.Windows.Forms.ListView lsvGameFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lsvLog;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip cmsGameFiles;
        private System.Windows.Forms.ToolStripMenuItem replaceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestSelectedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem replaceSelectedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton replaceSelectedFileToolStripButton;
        private System.Windows.Forms.ToolStripButton requestSelectedFiletoolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSelectedFileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

