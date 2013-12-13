namespace ProcessManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceFolder));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.AddFolder = new System.Windows.Forms.Button();
            this.RemoveFolder = new System.Windows.Forms.Button();
            this.FolderList = new System.Windows.Forms.CheckedListBox();
            this.AllPSFiles = new System.Windows.Forms.CheckedListBox();
            this.ScriptResult = new System.Windows.Forms.RichTextBox();
            this.RunScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddFolder
            // 
            this.AddFolder.Location = new System.Drawing.Point(500, 12);
            this.AddFolder.Name = "AddFolder";
            this.AddFolder.Size = new System.Drawing.Size(75, 23);
            this.AddFolder.TabIndex = 0;
            this.AddFolder.Text = "AddFolder";
            this.AddFolder.UseVisualStyleBackColor = true;
            this.AddFolder.Click += new System.EventHandler(this.AddFolder_Click);
            // 
            // RemoveFolder
            // 
            this.RemoveFolder.Location = new System.Drawing.Point(500, 41);
            this.RemoveFolder.Name = "RemoveFolder";
            this.RemoveFolder.Size = new System.Drawing.Size(75, 23);
            this.RemoveFolder.TabIndex = 1;
            this.RemoveFolder.Text = "Remove";
            this.RemoveFolder.UseVisualStyleBackColor = true;
            this.RemoveFolder.Click += new System.EventHandler(this.RemoveFolder_Click);
            // 
            // FolderList
            // 
            this.FolderList.Location = new System.Drawing.Point(12, 12);
            this.FolderList.Name = "FolderList";
            this.FolderList.Size = new System.Drawing.Size(325, 94);
            this.FolderList.TabIndex = 0;
            this.FolderList.SelectedIndexChanged += new System.EventHandler(this.FolderList_SelectedIndexChanged);
            // 
            // AllPSFiles
            // 
            this.AllPSFiles.FormattingEnabled = true;
            this.AllPSFiles.HorizontalScrollbar = true;
            this.AllPSFiles.Location = new System.Drawing.Point(12, 127);
            this.AllPSFiles.Name = "AllPSFiles";
            this.AllPSFiles.Size = new System.Drawing.Size(325, 199);
            this.AllPSFiles.TabIndex = 2;
            // 
            // ScriptResult
            // 
            this.ScriptResult.Location = new System.Drawing.Point(374, 127);
            this.ScriptResult.Name = "ScriptResult";
            this.ScriptResult.Size = new System.Drawing.Size(201, 199);
            this.ScriptResult.TabIndex = 3;
            this.ScriptResult.Text = "";
            // 
            // RunScript
            // 
            this.RunScript.Location = new System.Drawing.Point(500, 82);
            this.RunScript.Name = "RunScript";
            this.RunScript.Size = new System.Drawing.Size(75, 23);
            this.RunScript.TabIndex = 4;
            this.RunScript.Text = "Run";
            this.RunScript.UseVisualStyleBackColor = true;
            this.RunScript.Click += new System.EventHandler(this.RunScript_Click);
            // 
            // SourceFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 389);
            this.Controls.Add(this.RunScript);
            this.Controls.Add(this.ScriptResult);
            this.Controls.Add(this.AllPSFiles);
            this.Controls.Add(this.FolderList);
            this.Controls.Add(this.RemoveFolder);
            this.Controls.Add(this.AddFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SourceFolder";
            this.Text = "Source";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SourceFolder_FormClosing);
            this.Load += new System.EventHandler(this.SourceFolder_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button AddFolder;
        private System.Windows.Forms.Button RemoveFolder;
        private System.Windows.Forms.CheckedListBox FolderList;
        private System.Windows.Forms.CheckedListBox AllPSFiles;
        private System.Windows.Forms.RichTextBox ScriptResult;
        private System.Windows.Forms.Button RunScript;
    }
}