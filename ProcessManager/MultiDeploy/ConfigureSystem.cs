using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProcessManager
{
    public partial class ConfigureSystem : Form
    {
        public ConfigureSystem()
        {
            InitializeComponent();
        }

        private void EditSystem_Click(object sender, EventArgs e)
        {
            GlobalData.systemConfigString = EditSystemConfig.Text;

        }

        private void ConfigureSystem_Load(object sender, EventArgs e)
        {
            EditSystemConfig.Text = GlobalData.systemConfigString;
        }
    }
}
