using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiDeploy
{
    public partial class HostSetting : Form
    {
        public HostSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HostList.Items.Add(HostIP.Text);
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            int index = HostList.SelectedIndex;
            HostList.Items.RemoveAt(index);
        }

        private void HostSetting_Load(object sender, EventArgs e)
        {
            string hostliststring=GlobalData.hostNameList;
            string[] hostArray = Helper.StringSplit(hostliststring);
            foreach (string temp in hostArray)
            {
                HostList.Items.Add(temp.Trim());
            }
        }
    }
}
