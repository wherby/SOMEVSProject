using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Security.AccessControl;
using System.Xml;
using System.Xml.XPath;

namespace MultiDeploy
{
    public partial class SplitterConfigure : Form
    {
        public SplitterConfigure()
        {
            InitializeComponent();
        }

        private void AddSplitterConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog fg=new OpenFileDialog();
            if (fg.ShowDialog() == DialogResult.OK)
            {
                string file = fg.FileName;
                SplitterConfigList.Items.Add(file);
            }

        }

        private void RemoveConfig_Click(object sender, EventArgs e)
        {
            int index = SplitterConfigList.SelectedIndex;
            SplitterConfigList.Items.RemoveAt(index);
        }

        private void SplitterConfigure_Load(object sender, EventArgs e)
        {
            string splitterString = GlobalData.splitterList;
            string[] splitterArray = Helper.StringSplit(splitterString);
            foreach (string temp in splitterArray)
            {
                SplitterConfigList.Items.Add(temp.Trim());
            }
        }

        private void TrySplitter_Click(object sender, EventArgs e)
        {
            string rootFolder = GlobalData.ROOTFOLDER;
            if (Directory.Exists(rootFolder))
            {
                Helper.ForceDeleteDirectory(rootFolder);            
            }
            Directory.CreateDirectory(rootFolder);
            string hoststring = GlobalData.hostNameList;
            string[] hostArray = Helper.StringSplit(hoststring);
            string folderString = GlobalData.folderList;
            string[] folderArray = Helper.StringSplit(folderString);
            foreach (string hostTemp in hostArray)
            {
                string desTemp = rootFolder + @"\" + hostTemp;
                foreach (string sourceTemp in folderArray)
                {
                    DirectoryInfo source = new DirectoryInfo(sourceTemp);
                    Helper.CopyTo(source, desTemp, true);

                }
            }

            foreach (object splitTemp in SplitterConfigList.Items)
            {
                ProcessSplitterConfig(splitTemp.ToString());
            }
            MessageBox.Show("Splitter finished");
        }

        private void SplitterConfigure_FormClosing(object sender, FormClosingEventArgs e)
        {
            string temp = Helper.RestoreSetting(SplitterConfigList);
            GlobalData.splitterList = temp;
        }

        private void ProcessSplitterConfig(string splitterFileName)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(splitterFileName);

            XmlNodeList nodeList = xmldoc.SelectNodes("//Exchange/test");
            foreach (XmlNode temp in nodeList)
            {
                foreach (XmlNode temp2 in temp.ChildNodes)
                {
                    if (temp2.Attributes["Name"].Value == "Block")
                    {
                        temp2.Attributes["Value"].Value = "CIFS";
                    }
                }
            }
            XmlWriter wr = XmlWriter.Create(GlobalData.ROOTFOLDER + @"\test.xml");
            xmldoc.Save(wr);
            wr.Close();
        }
    }
}
