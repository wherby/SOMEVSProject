namespace MultiDeploy
{
    partial class SourceFolder
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.AddFolder = new System.Windows.Forms.Button();
            this.RemoveFolder = new System.Windows.Forms.Button();
            this.FolderList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // AddFolder
            // 
            this.AddFolder.Location = new System.Drawing.Point(205, 205);
            this.AddFolder.Name = "AddFolder";
            this.AddFolder.Size = new System.Drawing.Size(75, 23);
            this.AddFolder.TabIndex = 0;
            this.AddFolder.Text = "AddFolder";
            this.AddFolder.UseVisualStyleBackColor = true;
            this.AddFolder.Click += new System.EventHandler(this.AddFolder_Click);
            // 
            // RemoveFolder
            // 
            this.RemoveFolder.Location = new System.Drawing.Point(205, 235);
            this.RemoveFolder.Name = "RemoveFolder";
            this.RemoveFolder.Size = new System.Drawing.Size(75, 23);
            this.RemoveFolder.TabIndex = 1;
            this.RemoveFolder.Text = "Remove";
            this.RemoveFolder.UseVisualStyleBackColor = true;
            this.RemoveFolder.Click += new System.EventHandler(this.RemoveFolder_Click);
            // 
            // FolderList
            // 
            this.FolderList.FormattingEnabled = true;
            this.FolderList.Location = new System.Drawing.Point(12, 60);
            this.FolderList.Name = "FolderList";
            this.FolderList.Size = new System.Drawing.Size(268, 94);
            this.FolderList.TabIndex = 2;
            // 
            // SourceFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.FolderList);
            this.Controls.Add(this.RemoveFolder);
            this.Controls.Add(this.AddFolder);
            this.Name = "SourceFolder";
            this.Text = "Source";
            this.Load += new System.EventHandler(this.SourceFolder_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button AddFolder;
        private System.Windows.Forms.Button RemoveFolder;
        private System.Windows.Forms.CheckedListBox FolderList;
    }
}