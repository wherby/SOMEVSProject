namespace ProcessManager
{
    partial class PostConfig
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
            this.folderBrowserDialogPost = new System.Windows.Forms.FolderBrowserDialog();
            this.Addpost = new System.Windows.Forms.Button();
            this.RemovePost = new System.Windows.Forms.Button();
            this.PostConfigFolder = new System.Windows.Forms.CheckedListBox();
            this.PostConfigFile = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // Addpost
            // 
            this.Addpost.Location = new System.Drawing.Point(365, 27);
            this.Addpost.Name = "Addpost";
            this.Addpost.Size = new System.Drawing.Size(75, 23);
            this.Addpost.TabIndex = 0;
            this.Addpost.Text = "Add";
            this.Addpost.UseVisualStyleBackColor = true;
            this.Addpost.Click += new System.EventHandler(this.Addpost_Click);
            // 
            // RemovePost
            // 
            this.RemovePost.Location = new System.Drawing.Point(365, 56);
            this.RemovePost.Name = "RemovePost";
            this.RemovePost.Size = new System.Drawing.Size(75, 23);
            this.RemovePost.TabIndex = 1;
            this.RemovePost.Text = "Remove";
            this.RemovePost.UseVisualStyleBackColor = true;
            this.RemovePost.Click += new System.EventHandler(this.RemovePost_Click);
            // 
            // PostConfigFolder
            // 
            this.PostConfigFolder.FormattingEnabled = true;
            this.PostConfigFolder.Location = new System.Drawing.Point(27, 27);
            this.PostConfigFolder.Name = "PostConfigFolder";
            this.PostConfigFolder.Size = new System.Drawing.Size(260, 94);
            this.PostConfigFolder.TabIndex = 2;
            this.PostConfigFolder.SelectedIndexChanged += new System.EventHandler(this.PostConfigFolder_SelectedIndexChanged);
            // 
            // PostConfigFile
            // 
            this.PostConfigFile.FormattingEnabled = true;
            this.PostConfigFile.HorizontalScrollbar = true;
            this.PostConfigFile.Location = new System.Drawing.Point(27, 160);
            this.PostConfigFile.Name = "PostConfigFile";
            this.PostConfigFile.Size = new System.Drawing.Size(260, 94);
            this.PostConfigFile.TabIndex = 3;
            // 
            // PostConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 273);
            this.Controls.Add(this.PostConfigFile);
            this.Controls.Add(this.PostConfigFolder);
            this.Controls.Add(this.RemovePost);
            this.Controls.Add(this.Addpost);
            this.Name = "PostConfig";
            this.Text = "PostConfig";
            this.Load += new System.EventHandler(this.PostConfig_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogPost;
        private System.Windows.Forms.Button Addpost;
        private System.Windows.Forms.Button RemovePost;
        private System.Windows.Forms.CheckedListBox PostConfigFolder;
        private System.Windows.Forms.CheckedListBox PostConfigFile;
    }
}