﻿namespace ProcessManager
{
    partial class mainframe
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerShellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(509, 276);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem,
            this.scriptToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(596, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceFolderToolStripMenuItem,
            this.preScriptToolStripMenuItem,
            this.postScriptToolStripMenuItem,
            this.configSystemToolStripMenuItem,
            this.runnerToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // sourceFolderToolStripMenuItem
            // 
            this.sourceFolderToolStripMenuItem.Name = "sourceFolderToolStripMenuItem";
            this.sourceFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sourceFolderToolStripMenuItem.Text = "SourceFolder";
            this.sourceFolderToolStripMenuItem.Click += new System.EventHandler(this.sourceFolderToolStripMenuItem_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cToolStripMenuItem,
            this.powerShellToolStripMenuItem});
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.scriptToolStripMenuItem.Text = "Script";
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.cToolStripMenuItem.Text = "C#";
            // 
            // powerShellToolStripMenuItem
            // 
            this.powerShellToolStripMenuItem.Name = "powerShellToolStripMenuItem";
            this.powerShellToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.powerShellToolStripMenuItem.Text = "PowerShell";
            // 
            // preScriptToolStripMenuItem
            // 
            this.preScriptToolStripMenuItem.Name = "preScriptToolStripMenuItem";
            this.preScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.preScriptToolStripMenuItem.Text = "PreScript";
            this.preScriptToolStripMenuItem.Click += new System.EventHandler(this.preScriptToolStripMenuItem_Click);
            // 
            // postScriptToolStripMenuItem
            // 
            this.postScriptToolStripMenuItem.Name = "postScriptToolStripMenuItem";
            this.postScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.postScriptToolStripMenuItem.Text = "PostScript";
            this.postScriptToolStripMenuItem.Click += new System.EventHandler(this.postScriptToolStripMenuItem_Click);
            // 
            // configSystemToolStripMenuItem
            // 
            this.configSystemToolStripMenuItem.Name = "configSystemToolStripMenuItem";
            this.configSystemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.configSystemToolStripMenuItem.Text = "ConfigSystem";
            this.configSystemToolStripMenuItem.Click += new System.EventHandler(this.configSystemToolStripMenuItem_Click);
            // 
            // runnerToolStripMenuItem
            // 
            this.runnerToolStripMenuItem.Name = "runnerToolStripMenuItem";
            this.runnerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.runnerToolStripMenuItem.Text = "Run";
            this.runnerToolStripMenuItem.Click += new System.EventHandler(this.runnerToolStripMenuItem_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(26, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ESI@EMC";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // mainframe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainframe";
            this.Text = "MultiDeploy";
            this.Load += new System.EventHandler(this.mainframe_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerShellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runnerToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}

