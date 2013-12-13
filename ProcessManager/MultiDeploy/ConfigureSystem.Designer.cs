namespace ProcessManager
{
    partial class ConfigureSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureSystem));
            this.EditSystem = new System.Windows.Forms.Button();
            this.EditSystemConfig = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // EditSystem
            // 
            this.EditSystem.Location = new System.Drawing.Point(375, 47);
            this.EditSystem.Name = "EditSystem";
            this.EditSystem.Size = new System.Drawing.Size(73, 21);
            this.EditSystem.TabIndex = 0;
            this.EditSystem.Text = "EditSystem";
            this.EditSystem.UseVisualStyleBackColor = true;
            this.EditSystem.Click += new System.EventHandler(this.EditSystem_Click);
            // 
            // EditSystemConfig
            // 
            this.EditSystemConfig.Location = new System.Drawing.Point(118, 47);
            this.EditSystemConfig.Name = "EditSystemConfig";
            this.EditSystemConfig.Size = new System.Drawing.Size(251, 20);
            this.EditSystemConfig.TabIndex = 1;
            // 
            // ConfigureSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 273);
            this.Controls.Add(this.EditSystemConfig);
            this.Controls.Add(this.EditSystem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureSystem";
            this.Text = "ConfigureSystem";
            this.Load += new System.EventHandler(this.ConfigureSystem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EditSystem;
        private System.Windows.Forms.TextBox EditSystemConfig;
    }
}