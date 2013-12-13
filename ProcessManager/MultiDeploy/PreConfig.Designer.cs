namespace ProcessManager
{
    partial class PreConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreConfig));
            this.Addprefix = new System.Windows.Forms.Button();
            this.Removeprefix = new System.Windows.Forms.Button();
            this.PrefixConfigFolder = new System.Windows.Forms.CheckedListBox();
            this.PrefixFiles = new System.Windows.Forms.CheckedListBox();
            this.folderBrowserDialogPrefix = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // Addprefix
            // 
            this.Addprefix.Location = new System.Drawing.Point(429, 12);
            this.Addprefix.Name = "Addprefix";
            this.Addprefix.Size = new System.Drawing.Size(75, 23);
            this.Addprefix.TabIndex = 0;
            this.Addprefix.Text = "Add";
            this.Addprefix.UseVisualStyleBackColor = true;
            this.Addprefix.Click += new System.EventHandler(this.Addprefix_Click);
            // 
            // Removeprefix
            // 
            this.Removeprefix.Location = new System.Drawing.Point(429, 41);
            this.Removeprefix.Name = "Removeprefix";
            this.Removeprefix.Size = new System.Drawing.Size(75, 23);
            this.Removeprefix.TabIndex = 1;
            this.Removeprefix.Text = "Remove";
            this.Removeprefix.UseVisualStyleBackColor = true;
            this.Removeprefix.Click += new System.EventHandler(this.Removeprefix_Click);
            // 
            // PrefixConfigFolder
            // 
            this.PrefixConfigFolder.FormattingEnabled = true;
            this.PrefixConfigFolder.Location = new System.Drawing.Point(47, 12);
            this.PrefixConfigFolder.Name = "PrefixConfigFolder";
            this.PrefixConfigFolder.Size = new System.Drawing.Size(266, 94);
            this.PrefixConfigFolder.TabIndex = 2;
            this.PrefixConfigFolder.SelectedIndexChanged += new System.EventHandler(this.PrefixConfigFolder_SelectedIndexChanged);
            // 
            // PrefixFiles
            // 
            this.PrefixFiles.FormattingEnabled = true;
            this.PrefixFiles.Location = new System.Drawing.Point(47, 159);
            this.PrefixFiles.Name = "PrefixFiles";
            this.PrefixFiles.Size = new System.Drawing.Size(266, 109);
            this.PrefixFiles.TabIndex = 3;
            // 
            // PreConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 314);
            this.Controls.Add(this.PrefixFiles);
            this.Controls.Add(this.PrefixConfigFolder);
            this.Controls.Add(this.Removeprefix);
            this.Controls.Add(this.Addprefix);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreConfig";
            this.Text = "PreConfig";
            this.Load += new System.EventHandler(this.PreConfig_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Addprefix;
        private System.Windows.Forms.Button Removeprefix;
        private System.Windows.Forms.CheckedListBox PrefixConfigFolder;
        private System.Windows.Forms.CheckedListBox PrefixFiles;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogPrefix;
    }
}