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
    public partial class PreConfig : Form
    {
        public PreConfig()
        {
            InitializeComponent();
        }

        private void Addprefix_Click(object sender, EventArgs e)
        {
            folderBrowserDialogPrefix.ShowDialog();
            string folder = folderBrowserDialogPrefix.SelectedPath;
            PrefixConfigFolder.Items.Add(folder);
        }

        private void Removeprefix_Click(object sender, EventArgs e)
        {
            int index = PrefixConfigFolder.SelectedIndex;
            PrefixConfigFolder.Items.RemoveAt(index);
        }

        private void PrefixConfigFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalData.prefixFileString.Clear();
            for (int i = 0; i < PrefixConfigFolder.Items.Count; i++)
            {
                string[] tempFiles = Helper.GetAllFiles(PrefixConfigFolder.Items[i].ToString());
                GlobalData.prefixFileString.AddRange(tempFiles);
            }

            PrefixFiles.Items.Clear();
            foreach (string temp in GlobalData.prefixFileString)
            {
                PrefixFiles.Items.Add(temp);
            }
        }

        private void PreConfig_Load(object sender, EventArgs e)
        {
            foreach (string item in GlobalData.prefixFileString)
            {
                PrefixFiles.Items.Add(item);
            }
        }
    }
}
