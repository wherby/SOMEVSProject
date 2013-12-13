namespace ProcessManager
{
    partial class Runner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Runner));
            this.Run = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PreScriptFile = new System.Windows.Forms.CheckedListBox();
            this.SystemConfig = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PostScriptFile = new System.Windows.Forms.CheckedListBox();
            this.ScriptFile = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ScriptGenerator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(509, 397);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 0;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "System";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // PreScriptFile
            // 
            this.PreScriptFile.FormattingEnabled = true;
            this.PreScriptFile.HorizontalScrollbar = true;
            this.PreScriptFile.Location = new System.Drawing.Point(365, 22);
            this.PreScriptFile.Name = "PreScriptFile";
            this.PreScriptFile.Size = new System.Drawing.Size(236, 94);
            this.PreScriptFile.TabIndex = 3;
            // 
            // SystemConfig
            // 
            this.SystemConfig.FormattingEnabled = true;
            this.SystemConfig.HorizontalScrollbar = true;
            this.SystemConfig.Location = new System.Drawing.Point(83, 22);
            this.SystemConfig.Name = "SystemConfig";
            this.SystemConfig.Size = new System.Drawing.Size(184, 94);
            this.SystemConfig.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "prefixFiles";
            // 
            // PostScriptFile
            // 
            this.PostScriptFile.FormattingEnabled = true;
            this.PostScriptFile.HorizontalScrollbar = true;
            this.PostScriptFile.Location = new System.Drawing.Point(365, 192);
            this.PostScriptFile.Name = "PostScriptFile";
            this.PostScriptFile.Size = new System.Drawing.Size(219, 94);
            this.PostScriptFile.TabIndex = 6;
            // 
            // ScriptFile
            // 
            this.ScriptFile.FormattingEnabled = true;
            this.ScriptFile.HorizontalScrollbar = true;
            this.ScriptFile.Location = new System.Drawing.Point(83, 192);
            this.ScriptFile.Name = "ScriptFile";
            this.ScriptFile.Size = new System.Drawing.Size(184, 94);
            this.ScriptFile.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "postFile";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "ScriptFile";
            // 
            // ScriptGenerator
            // 
            this.ScriptGenerator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ScriptGenerator.Location = new System.Drawing.Point(509, 360);
            this.ScriptGenerator.Name = "ScriptGenerator";
            this.ScriptGenerator.Size = new System.Drawing.Size(75, 23);
            this.ScriptGenerator.TabIndex = 10;
            this.ScriptGenerator.Text = "ScriptGen";
            this.ScriptGenerator.UseVisualStyleBackColor = true;
            this.ScriptGenerator.Click += new System.EventHandler(this.ScriptGenerator_Click);
            // 
            // Runner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 451);
            this.Controls.Add(this.ScriptGenerator);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ScriptFile);
            this.Controls.Add(this.PostScriptFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SystemConfig);
            this.Controls.Add(this.PreScriptFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Run);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Runner";
            this.Text = "Runner";
            this.Load += new System.EventHandler(this.Runner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox PreScriptFile;
        private System.Windows.Forms.CheckedListBox SystemConfig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox PostScriptFile;
        private System.Windows.Forms.CheckedListBox ScriptFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ScriptGenerator;
    }
}