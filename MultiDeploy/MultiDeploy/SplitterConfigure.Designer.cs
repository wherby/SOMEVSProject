namespace MultiDeploy
{
    partial class SplitterConfigure
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
            this.AddSplitterConfig = new System.Windows.Forms.Button();
            this.RemoveConfig = new System.Windows.Forms.Button();
            this.SplitterConfigList = new System.Windows.Forms.CheckedListBox();
            this.TrySplitter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddSplitterConfig
            // 
            this.AddSplitterConfig.Location = new System.Drawing.Point(196, 206);
            this.AddSplitterConfig.Name = "AddSplitterConfig";
            this.AddSplitterConfig.Size = new System.Drawing.Size(75, 23);
            this.AddSplitterConfig.TabIndex = 0;
            this.AddSplitterConfig.Text = "AddConfig";
            this.AddSplitterConfig.UseVisualStyleBackColor = true;
            this.AddSplitterConfig.Click += new System.EventHandler(this.AddSplitterConfig_Click);
            // 
            // RemoveConfig
            // 
            this.RemoveConfig.Location = new System.Drawing.Point(196, 238);
            this.RemoveConfig.Name = "RemoveConfig";
            this.RemoveConfig.Size = new System.Drawing.Size(75, 23);
            this.RemoveConfig.TabIndex = 1;
            this.RemoveConfig.Text = "RemoveConfig";
            this.RemoveConfig.UseVisualStyleBackColor = true;
            this.RemoveConfig.Click += new System.EventHandler(this.RemoveConfig_Click);
            // 
            // SplitterConfigList
            // 
            this.SplitterConfigList.FormattingEnabled = true;
            this.SplitterConfigList.Location = new System.Drawing.Point(12, 38);
            this.SplitterConfigList.Name = "SplitterConfigList";
            this.SplitterConfigList.Size = new System.Drawing.Size(259, 109);
            this.SplitterConfigList.TabIndex = 2;
            // 
            // TrySplitter
            // 
            this.TrySplitter.Location = new System.Drawing.Point(3, 238);
            this.TrySplitter.Name = "TrySplitter";
            this.TrySplitter.Size = new System.Drawing.Size(75, 23);
            this.TrySplitter.TabIndex = 3;
            this.TrySplitter.Text = "TrySplitter";
            this.TrySplitter.UseVisualStyleBackColor = true;
            this.TrySplitter.Click += new System.EventHandler(this.TrySplitter_Click);
            // 
            // SplitterConfigure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.TrySplitter);
            this.Controls.Add(this.SplitterConfigList);
            this.Controls.Add(this.RemoveConfig);
            this.Controls.Add(this.AddSplitterConfig);
            this.Name = "SplitterConfigure";
            this.Text = "Splitter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SplitterConfigure_FormClosing);
            this.Load += new System.EventHandler(this.SplitterConfigure_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddSplitterConfig;
        private System.Windows.Forms.Button RemoveConfig;
        private System.Windows.Forms.CheckedListBox SplitterConfigList;
        private System.Windows.Forms.Button TrySplitter;
    }
}