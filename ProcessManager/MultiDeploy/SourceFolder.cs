using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.IO;

namespace ProcessManager
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
            foreach (string temp in GlobalData.allPSFiles)
            {
                AllPSFiles.Items.Add(temp);
            }
        }

        private void SourceFolder_FormClosing(object sender, FormClosingEventArgs e)
        {
            string temp = Helper.RestoreSetting(FolderList);
            GlobalData.folderList = temp;
        }

        private void AllPsFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FolderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalData.allPSFiles = new List<string>();
            for (int i = 0; i < FolderList.Items.Count; i++)
            {
                string [] tempFiles=Helper.GetAllFiles(FolderList.Items[i].ToString());
                GlobalData.allPSFiles.AddRange(tempFiles);
            }

            AllPSFiles.Items.Clear();
            foreach (string temp in GlobalData.allPSFiles)
            {
                AllPSFiles.Items.Add(temp);
            }
        }

        private void RunScript_Click(object sender, EventArgs e)
        {
            ScriptResult.Text = "";


            for (int i = 0; i < GlobalData.allPSFiles.Count; i++) 
            {
                string temp = GlobalData.allPSFiles[i];

                string[] allPSString = File.ReadAllLines(temp);
                foreach (string temp2 in allPSString)
                {
                    if (temp2.Trim() != string.Empty)
                    {
                        GlobalData.psMachine.RunScript(new List<string> { temp2 }, new List<PSParam>() { });
                    }
                }
                AllPSFiles.SetSelected(i, true);
                AllPSFiles.SetItemChecked(i, true);
            }
        }


    }
}
