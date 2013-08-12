/////////////////////////////////////////////////////////////////////
///InforTracer Project Created by Tao Zhou @ 3/30/2012
///Any feedback contact tao.zhou@emc.com
/////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;

namespace InfoTracer
{
    public partial class Form1 : Form
    {
        string filterString = "";
        string logFolderString = "";
        string countString = "";
        int count = 1;
        bool isSingle = false;
        int numFile = 100000000;
        bool isRaw = false;
        string pageString = "1";
        PowershellMachine psMachine;
        public Form1()
        {
            InitializeComponent();
            logFolderString=HelperAdapter.GetProperty("DefaultFolder");
            filterString = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileFilter = HelperAdapter.GetProperty("FileFilter");
            string log = logFolder.Text;
            FileInfo lastfile;
            string contents="";
            try
            {
                if (logFolderString == string.Empty)
                {
                    logFolderString = HelperAdapter.GetProperty("DefaultFolder");
                }
                LogResult.Text = string.Format(logFolderString);
                DirectoryInfo directory = new DirectoryInfo(logFolderString);
                FileInfo[] files = directory.GetFiles(fileFilter);
                
                lastfile = files[0];
                foreach (FileInfo file in files)
                {
                    if (file.LastWriteTime > lastfile.LastWriteTime)
                    {
                        lastfile = file;
                    }
                }
                contents = lastfile.OpenText().ReadToEnd();
                if (isSingle == true)
                {
                    SearchFile(lastfile,true);
                }
                else
                {
                    if (files.Count<FileInfo>() > numFile)
                    {
                        Array.Sort(files, (f2, f1) => f1.LastWriteTime.CompareTo(f2.LastWriteTime));
                        FileInfo[] temp=new FileInfo[numFile];
                        Array.Copy(files, temp, numFile);
                        files = temp;
                    }
                    foreach (FileInfo tempfile in files)
                    {
                        SearchFile(tempfile);
                    }
                }

            }
            catch
            {
                try
                {
                    FileInfo fileSingle = new FileInfo(logFolderString);
                    SearchFile(fileSingle,true);

                }
                catch (Exception ex1)
                {
                    LogResult.Text += string.Format("LogFile Error /n{0}", ex1.Message);
                }
            }


        }

        private void GetRawData()
        {
            if (isRaw == true)
            {
                string resultStringTotal = LogResult.Text;
                string[] lines = resultStringTotal.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> lineStrs = new List<string>();
                lineStrs.Capacity = 100000;
                string pattern = HelperAdapter.GetProperty("MessagePattern");


                string temp = null;
                for (int i = 0; i < lines.Count<string>(); i++)
                {
                    Match Matches = Regex.Match(lines[i], pattern);


                    if (lines[i].StartsWith("=="))
                    {
                        continue;
                    }
                    if (temp != null)
                    {
                        temp = temp + "\r\n" + lines[i];
                    }
                    else
                    {
                        temp = lines[i];
                    }
                    if (Matches.Success == true)
                    {
                        if (temp != null)
                        {
                            lineStrs.Add(temp);
                            temp = null;
                        }
                    }

                }
                string tempResult = null;
                foreach (string templine in lineStrs)
                {
                    string[] templines = templine.Split(new string[] { "]:" }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (templines.Count<string>() == 2)
                    {
                        if (tempResult != null)
                        {
                            tempResult = tempResult + "\r\n" + templines[1];
                        }
                        else
                        {
                            tempResult = templines[1];
                        }
                    }
                }
                LogResult.Text = tempResult;
            }
        }

        //private void SearchFile(FileInfo file,bool isUseMaxium=false)
        //{
        //    LogResult.Text += "\r\n" + file.FullName +"\r\n"+ "+++++++++++++++++++++++++++++++++++++"+"\r\n";
        //    int MAXNUM = 10240;
        //    if (isUseMaxium == true)
        //    {
        //        MAXNUM = (int)file.Length;
        //    }

        //    int number = MAXNUM;
            
        //    using (StreamReader fStream = file.OpenText())
        //    {
        //        while (number == MAXNUM)
        //        {
        //            char[] buffer1 = new char[MAXNUM];
        //            number = fStream.Read(buffer1, 0, MAXNUM);
        //            string contents = new string(buffer1);
        //            List<string> strLines = GetStringLines(contents);
                    
        //            if (filterString == string.Empty)
        //            {
        //                LogResult.Text += contents;
        //            }
        //            else
        //            {
        //                SearchString(strLines);
        //            }
                    
        //        }
  
        //    }
           
        //}

        private void SearchFile(FileInfo file, bool isUseMaxium = false)
        {
            LogResult.Text += "\r\n" + file.FullName + "\r\n" + "+++++++++++++++++++++++++++++++++++++" + "\r\n";
            int MAXNUM = 10240;
            //if (isUseMaxium == true)
            //{
            //    MAXNUM = (int)file.Length;
            //}

            int number = MAXNUM;

            int pageSet = int.Parse(pageString);


            using (StreamReader fStream = file.OpenText())
            {
                //while (number == MAXNUM)
                //{
                //    char[] buffer1 = new char[MAXNUM];
                //    number = fStream.Read(buffer1, 0, MAXNUM);

                //    string contents = new string(buffer1);
                //    List<string> strLines = GetStringLines(contents);

                //if (filterString == string.Empty)
                //{
                //    LogResult.Text += contents;
                //}
                //else
                //{
                //    SearchString(strLines);
                //}

                //}
                int i = 0;
                List<string> lines = new List<string>();
                List<string> linesToSearch = new List<string>();
                while (fStream.Peek() >= 0)
                {
                    i++;

                    string lineToProcess = fStream.ReadLine();

                    lines.Add(lineToProcess);
                    linesToSearch.Add(lineToProcess);

                    int PagePerLines = 500;

                    if (isRaw == true)
                    {
                        PagePerLines = 100000000;
                    }

                    if (i == i / PagePerLines * PagePerLines)
                    {
                        if ((i == pageSet * PagePerLines) || (filterString != string.Empty))
                        {
                            string contents = null;
                            foreach (string line in lines)
                            {
                                if (contents == null)
                                {
                                    contents = line;
                                }
                                else
                                {
                                    contents = contents + "\r\n" + line;
                                }

                            }
                            if (filterString == string.Empty)
                            {
                                LogResult.Text += contents;
                                break;
                            }
                            else
                            {
                                LogResult.Text += "\r\n" + "==========***Page " + i / PagePerLines + "*****====";
                                SearchString(lines);
                            }
                            lines.Clear();
                                
                        }
                        lines.Clear();
                    }

                }
                if (lines.Count != 0)
                {
                    string contents = null;
                    foreach (string line in lines)
                    {
                        if (contents == null)
                        {
                            contents = line;
                        }
                        else
                        {
                            contents = contents + "\r\n" + line;
                        }
                    }
                    if (filterString == string.Empty)
                    {
                        LogResult.Text += contents;
                    }
                    else
                    {
                        SearchString(lines);
                    }
                    lines.Clear();
                }
            }
            GetRawData();
        }

        private void SearchString(List<string> strLines)
        {
            List<string> tempList = new List<string>();
            tempList.Capacity = 100000;
            if (countString != string.Empty)
            {
                try
                {
                    count = int.Parse(countString);
                }
                catch (Exception ex)
                {
                    LogResult.Text += "\r\n" + "Count input error" + ex.Message;
                    return;
                }
            }
            for (int i = 0; i < strLines.Count; i++)
            {
                tempList.Add(strLines[i]);
                if (tempList.Count > count)
                {
                    tempList.RemoveAt(0);
                }
                if (strLines[i].IndexOf(filterString) >= 0)
                {
                    LogResult.Text += "\r\n" + "=========================================================";
                    string tempResult = "";
                    foreach (string tempLine in tempList)
                    {
                        tempResult += "\r\n" + tempLine;
                    }
                    for (int j = 0; j < count-1; j++)
                    {
                        if (i + j < strLines.Count)
                        {
                            tempResult += "\r\n" + strLines[i + j];
                        }
                    }
                    LogResult.Text += "\r\n" + tempResult;
                    tempList.Clear();
                }
               
            }
            tempList.Clear();
        }

        private List<string> GetStringLines(string content)
        {
            string[] lines = content.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
            List<string> lineStrs = new List<string>();
            lineStrs.Capacity = 100000;
            string pattern = HelperAdapter.GetProperty("MessagePattern");

            
            string temp = null;
            for (int i = 0; i < lines.Count<string>(); i++)
            {
                Match Matches = Regex.Match(lines[i], pattern);
                
                if (Matches.Success ==true)
                {
                    if (temp != null)
                    {
                        lineStrs.Add(temp);
                        temp = null;
                    }
                }
                if (temp != null)
                {
                    temp = temp + "\r\n" + lines[i];
                }
                else
                {
                    temp = lines[i];
                }
            }
            return lineStrs;
        }

        private void logFolder_TextChanged(object sender, EventArgs e)
        {
            logFolderString = logFolder.Text;
        }

        private void CountLines_TextChanged(object sender, EventArgs e)
        {
            countString = CountLines.Text;
        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            filterString = filter.Text;
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Zhou.Tao@emc.com", "Send To",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Question);


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isSingle == true)
            {
                isSingle = false;
            }
            else
            {
                isSingle = true;
            }
        }

        private void numForFiles_TextChanged(object sender, EventArgs e)
        {
            string numString = numForFiles.Text;
            try
            {
                numFile = int.Parse(numString);
            }
            catch (Exception ex)
            {
                LogResult.Text += ex.Message;
                numFile = 1000000;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (isRaw == false)
            {
                isRaw = true;
            }
            else
            {
                isRaw = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void page_TextChanged(object sender, EventArgs e)
        {
            pageString = page.Text;
            if (page.Text.Trim() == string.Empty)
            {
                pageString = "1";
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
                        openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    logFolderString=openFileDialog1.FileName;
                    logFolder.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PowershellMachine psMachine = PowershellMachine.GetInstance();
            string text = LogResult.Text;
            string[] textSplits = text.Split(new string[] { "~BP" }, 2, StringSplitOptions.RemoveEmptyEntries);
            if(textSplits.Count<string>()==2)
            {
                LogResult.Text = textSplits[1];
            }
            string[] cmdStrings = textSplits[0].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> scripts=new List<string>(cmdStrings);
            psMachine.RunScript(scripts, new List<PSParam>());
        }











    }
}
