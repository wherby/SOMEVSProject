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
                ParseSplitterConfigFile(splitTemp.ToString());
            }
            MessageBox.Show("Splitter finished");
        }

        private void SplitterConfigure_FormClosing(object sender, FormClosingEventArgs e)
        {
            string temp = Helper.RestoreSetting(SplitterConfigList);
            GlobalData.splitterList = temp;
        }

        private void ParseSplitterConfigFile(string splitterFileName)
        {
            string hoststring = GlobalData.hostNameList;
            string[] hostArray = Helper.StringSplit(hoststring);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(splitterFileName);
            ProcessChangeSplitter(hostArray, xmldoc);
            ProcessReplaceSplitter(hostArray, xmldoc);
            MoveToRemoteMachine(hostArray);
        }

        #region ChangeSplitter and replace splitter
        private void ProcessChangeSplitter(string[] hostArray, XmlDocument xmldoc)
        {

            XmlNodeList nodeList = xmldoc.SelectNodes("//Change");

            string rootFolder = GlobalData.ROOTFOLDER;

            foreach (XmlNode temp in nodeList)
            {
                foreach (XmlNode temp2 in temp.ChildNodes)
                {
                    string fileName = temp2.Attributes["FileName"].Value;
                    string xpath = temp2.Attributes["XPath"].Value;
                    string key = temp2.Attributes["Key"].Value;
                    string values = temp2.Attributes["Value"].Value;
                    string[] valueArray = values.Split('#');
                    int i = 0;
                    foreach (string hostTemp in hostArray)
                    {
                        string fileNameTemp = rootFolder + @"\" + hostArray[i % hostArray.Length] + @"\" + fileName;
                        ProcessSplitterConfig(fileNameTemp, xpath, key, valueArray[i % hostArray.Length]);
                        i++;
                    }
                }
            }
        }

        private void ProcessSplitterConfig(string splitterFileName,string XPath,string key,string val)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(splitterFileName);

            XmlNodeList nodeList = xmldoc.SelectNodes(XPath);
            foreach (XmlNode temp in nodeList)
            {
                foreach (XmlNode temp2 in temp.ChildNodes)
                {
                    if (temp2.Attributes["Name"].Value == key)
                    {
                        temp2.Attributes["Value"].Value =val;
                    }
                }
            }
            //FileInfo file = new FileInfo(splitterFileName);
            //string fileName = file.Name;
            XmlWriter wr = XmlWriter.Create(splitterFileName);
            xmldoc.Save(wr);
            wr.Close();
        }

        private void ProcessReplaceSplitter(string[] hostArray, XmlNode xmldoc)
        {
            XmlNodeList nodeList = xmldoc.SelectNodes("//Replace");

            string rootFolder = GlobalData.ROOTFOLDER;

            foreach (XmlNode temp in nodeList)
            {
                foreach (XmlNode temp2 in temp.ChildNodes)
                {
                    string fileName = temp2.Attributes["FileName"].Value;
                    string replaceCandidateString = temp2.Attributes["ReplaceCandidate"].Value;
                    string[] replaceArray = replaceCandidateString.Split('#');
                    int i = 0;
                    foreach (string tempReplace in replaceArray)
                    {
                        string tempFilePath = rootFolder + @"\" + hostArray[i] + @"\";
                        FileInfo tempFile = new FileInfo(tempFilePath + replaceArray[i]);
                        if (File.Exists(tempFilePath + fileName) && File.Exists(tempFilePath + replaceArray[i]))
                        {
                            tempFile.Replace(tempFilePath + fileName, "aaa.bak");
                        }
                        i++;
                    }
                }
            }
        }

        #endregion

        private void MoveToRemoteMachine(string[] hostArray)
        {

            string rootFolder = GlobalData.ROOTFOLDER;
            foreach (string hosttemp in hostArray)
            {
                string tempRoot = @"\\" + hosttemp + @"\c$\Deploy";
                DirectoryInfo dir = new DirectoryInfo(tempRoot);
                if (dir.Exists)
                {
                    Helper.ForceDeleteDirectory(tempRoot);
                }
                dir.Create();
                string source = rootFolder + @"\" + hosttemp;
                DirectoryInfo sourceDir = new DirectoryInfo(source);
                Helper.CopyTo(sourceDir, tempRoot, true);
            }

        }

    }
}
