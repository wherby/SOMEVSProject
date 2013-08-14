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
    public partial class mainframe : Form
    {
        public mainframe()
        {
            InitializeComponent();
        }

        private void hostSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostSetting host = new HostSetting();
            host.ShowDialog();
        }

        private void sourceFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SourceFolder source = new SourceFolder();
            source.ShowDialog();
        }

        private void splitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplitterConfigure split = new SplitterConfigure();
            split.ShowDialog();
        }

    }
}
