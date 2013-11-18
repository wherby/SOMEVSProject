/*=================================================
#* ProjectName: ProcessManager PowerShellScript WorkFlow.
#* Created: [11/05/2013]
#* Author: Tao Zhou
#*==================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Configuration;
using System.Diagnostics;

namespace ProcessManager
{
    public static class GlobalData
    {
        public static string hostNameList=@"test-tao1
        srv-tao1
test-sandy2";

        public static string folderList = "";

        public static string splitterList = @"C:\Users\tes.xml";

        public static string ROOTFOLDER = @"c:\MultiDeploy";

        public static List<String> folderLists = new List<string>();

        public static List<string> allPSFiles = new List<string>() { @"C:\Users\zhout3\clean.ps1" };

        public static TestLog log;
        public static PowershellMachine psMachine;


        public static string systemConfigString = "VNX-Block,VNXe";
        public static List<string> prefixFileString = new List<string>() { @"C:\ProcessManager\prefixScript\setVNXBlock.ps1" };
        public static List<string> postfixFileString = new List<string>() { @"C:\ProcessManager\postScript\RemvoeAll.ps1" };
        public static string pathCurrent = AppDomain.CurrentDomain.BaseDirectory;
    }

    public static class Helper
    {
        private static string[] SPLITTER = new string[] { "\r\n" };

        public static string[] StringSplit(string stringInput)
        {
            string[] listString = stringInput.Split(SPLITTER, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i=0;i<listString.Count<string>();i++)
            {
                listString[i] = listString[i].Trim();
            }
            return listString;
        }

        public static string RestoreSetting(CheckedListBox boxlist)
        {
            string tempString = "";
            foreach (object temp in boxlist.Items)
            {
                tempString += temp.ToString() + SPLITTER[0];
            }
            return tempString;
        }


        // Copies all files from one directory to another.
        public static void CopyTo( DirectoryInfo source, string destDirectory, bool recursive)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destDirectory == null)
                throw new ArgumentNullException("destDirectory");

            // If the source doesn't exist, we have to throw an exception.
            if (!source.Exists)
                throw new DirectoryNotFoundException("Source directory not found: " + source.FullName);
            // Compile the target.
            DirectoryInfo target = new DirectoryInfo(destDirectory);
            // If the target doesn't exist, we create it.
            if (!target.Exists)
                target.Create();
            // Get all files and copy them over.
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
            // Return if no recursive call is required.
            if (!recursive)
                return;
            // Do the same for all sub directories.
            foreach (DirectoryInfo directory in source.GetDirectories())
            {
                CopyTo(directory, Path.Combine(target.FullName, directory.Name), recursive);
            }
        }

        public static void ForceDeleteDirectory(string path)
        {
            var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };

            foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
            {
                info.Attributes = FileAttributes.Normal;
            }

            directory.Delete(true);
        }

        public static string[] GetAllFiles(string path)
        {
            var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };
            FileInfo [] allPSFiles= directory.GetFiles ("*.ps1",SearchOption.TopDirectoryOnly);
            List<string> tempFilePath = new List<string>();
            foreach (FileInfo temp in allPSFiles)
            {
                tempFilePath.Add(temp.FullName);
            }
            return tempFilePath.ToArray() ;
        }




        /// <summary>
        /// The method is used to get value from configure file.
        /// </summary>
        /// <param name="propertyName">The name of property.</param>
        /// <returns>The value of property.</returns>
        public static string GetProperty(string propertyName)
        {
            string propertyValue = ConfigurationManager.AppSettings[propertyName];
            if (propertyValue == null)
            {
                Exception ex = new Exception(string.Format("The property {0} is not exist in configure file", propertyName));
                throw ex;
            }
            return propertyValue;
        }

        public static void KillProcess(string processName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                    MessageBox.Show("杀死" + processName + "成功！");
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("杀死" + processName + "失败！");
            }
        }



    }
}
