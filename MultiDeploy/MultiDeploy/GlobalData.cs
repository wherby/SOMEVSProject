using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MultiDeploy
{
    public static class GlobalData
    {
        public static string hostNameList=@"test-tao1
        srv-tao1
test-sandy2";

        public static string folderList = @"C:\Local\PowerShellAutomation\PSAutomation2.1\Config
   C:\Local\pro1\CaseGenerator\CaseGenerator\Helper
C:\Local\Temp";

        public static string splitterList = @"C:\Users\tes.xml";

        public static string ROOTFOLDER = @"c:\MultiDeploy";
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

    }
}
