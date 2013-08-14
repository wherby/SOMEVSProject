namespace MultiDeploy
{
    partial class HostSetting
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
            this.AddHost = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.HostList = new System.Windows.Forms.CheckedListBox();
            this.HostIP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AddHost
            // 
            this.AddHost.Location = new System.Drawing.Point(195, 200);
            this.AddHost.Name = "AddHost";
            this.AddHost.Size = new System.Drawing.Size(75, 23);
            this.AddHost.TabIndex = 0;
            this.AddHost.Text = "AddHost";
            this.AddHost.UseVisualStyleBackColor = true;
            this.AddHost.Click += new System.EventHandler(this.button1_Click);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(195, 229);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(75, 23);
            this.Remove.TabIndex = 1;
            this.Remove.Text = "RemoveSelected";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // HostList
            // 
            this.HostList.FormattingEnabled = true;
            this.HostList.Location = new System.Drawing.Point(47, 41);
            this.HostList.Name = "HostList";
            this.HostList.Size = new System.Drawing.Size(120, 94);
            this.HostList.TabIndex = 2;
            // 
            // HostIP
            // 
            this.HostIP.Location = new System.Drawing.Point(47, 200);
            this.HostIP.Name = "HostIP";
            this.HostIP.Size = new System.Drawing.Size(100, 20);
            this.HostIP.TabIndex = 3;
            // 
            // HostSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.HostIP);
            this.Controls.Add(this.HostList);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.AddHost);
            this.Name = "HostSetting";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HostSetting_FormClosing);
            this.Load += new System.EventHandler(this.HostSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddHost;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.CheckedListBox HostList;
        private System.Windows.Forms.TextBox HostIP;
    }
}