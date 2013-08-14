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
    public partial class SourceFolder : Form
    {
        public SourceFolder()
        {
            InitializeComponent();
        }

        private void AddFolder_Click(object sender, EventArgs e)
        {

            folderBrowserDialog1.ShowDialog();
            string folder = folderBrowserDialog1.SelectedPath;
            FolderList.Items.Add(folder);
        }

        private void RemoveFolder_Click(object sender, EventArgs e)
        {
            int index = FolderList.SelectedIndex;
            FolderList.Items.RemoveAt(index);
        }

        private void SourceFolder_Load(object sender, EventArgs e)
        {
            string folderlistString = GlobalData.folderList;
            string[] folderArray = Helper.StringSplit(folderlistString);
            foreach (string temp in folderArray)
            {
                FolderList.Items.Add(temp.Trim());
            }
        }

        private void SourceFolder_FormClosing(object sender, FormClosingEventArgs e)
        {
            string temp = Helper.RestoreSetting(FolderList);
            GlobalData.folderList = temp;
        }
    }
}
