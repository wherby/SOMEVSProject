namespace ObjectBrowser
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.moduleName = new System.Windows.Forms.TextBox();
            this.methodName = new System.Windows.Forms.TextBox();
            this.Runner = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(499, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // moduleName
            // 
            this.moduleName.AcceptsReturn = true;
            this.moduleName.Location = new System.Drawing.Point(356, 89);
            this.moduleName.Multiline = true;
            this.moduleName.Name = "moduleName";
            this.moduleName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.moduleName.Size = new System.Drawing.Size(228, 148);
            this.moduleName.TabIndex = 2;
            // 
            // methodName
            // 
            this.methodName.Location = new System.Drawing.Point(360, 268);
            this.methodName.Name = "methodName";
            this.methodName.Size = new System.Drawing.Size(213, 20);
            this.methodName.TabIndex = 3;
            // 
            // Runner
            // 
            this.Runner.Location = new System.Drawing.Point(502, 332);
            this.Runner.Name = "Runner";
            this.Runner.Size = new System.Drawing.Size(75, 23);
            this.Runner.TabIndex = 4;
            this.Runner.Text = "Runner";
            this.Runner.UseVisualStyleBackColor = true;
            this.Runner.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 407);
            this.Controls.Add(this.Runner);
            this.Controls.Add(this.methodName);
            this.Controls.Add(this.moduleName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox moduleName;
        private System.Windows.Forms.TextBox methodName;
        private System.Windows.Forms.Button Runner;
    }
}

