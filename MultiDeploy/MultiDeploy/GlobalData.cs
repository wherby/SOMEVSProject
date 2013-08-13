using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiDeploy
{
    public static class GlobalData
    {
        public static string hostNameList=@"11
        22";

        public static string folderList = @"c:\local
    c:\user";
    }

    public static class Helper
    {
        private static string[] SPLITTER = new string[] { "\r\n" };

        public static string[] StringSplit(string stringInput)
        {
            string[] listString = stringInput.Split(SPLITTER, System.StringSplitOptions.RemoveEmptyEntries);
            return listString;
        }
    }
}
