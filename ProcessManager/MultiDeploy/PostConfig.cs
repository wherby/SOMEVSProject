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
    public partial class PostConfig : Form
    {
        public PostConfig()
        {
            InitializeComponent();
        }

        private void PostConfig_Load(object sender, EventArgs e)
        {
            foreach (string item in GlobalData.postfixFileString)
            {
                PostConfigFile.Items.Add(item);
            }
        }

        private void Addpost_Click(object sender, EventArgs e)
        {
            folderBrowserDialogPost.ShowDialog();
            string folder = folderBrowserDialogPost.SelectedPath;
            PostConfigFolder.Items.Add(folder);
        }

        private void RemovePost_Click(object sender, EventArgs e)
        {
            int index = PostConfigFolder.SelectedIndex;
            PostConfigFolder.Items.RemoveAt(index);
        }

        private void PostConfigFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalData.postfixFileString.Clear();
            for (int i = 0; i < PostConfigFolder.Items.Count; i++)
            {
                string[] tempFiles = Helper.GetAllFiles(PostConfigFolder.Items[i].ToString());
                GlobalData.postfixFileString.AddRange(tempFiles);
            }

            PostConfigFile.Items.Clear();
            foreach (string temp in GlobalData.postfixFileString)
            {
                PostConfigFile.Items.Add(temp);
            }
        }

    }
}
