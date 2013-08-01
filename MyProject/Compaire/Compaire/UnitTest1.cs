using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PowerShellTestTools;
using System.IO;

namespace Compaire
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string file1String = File.ReadAllText("text1.txt");
            string file2String = File.ReadAllText("text2.txt");
            List<SortedList<string, string>> tempFile1 = HelperAdapter.GenerateKeyValuePairsList(file1String);
            List<SortedList<string, string>> tempFile2 = HelperAdapter.GenerateKeyValuePairsList(file2String);
            List<SortedList<string, string>> tempFile3 = new List<SortedList<string, string>>();
            List<SortedList<string, string>> tempFile5 = HelperAdapter.GenerateKeyValuePairsList(file2String);
            foreach (SortedList<string, string> tempLun in tempFile1)
            {
                if ((tempLun["Description"] != "2-Way Mir") && (tempLun["Description"] != "VAULT") && (tempLun["Description"] != "RDF1+TDEV")
                    && (tempLun["Description"] != "Unprotected"))
                {
                    tempFile5.Add(tempLun);
                }
            }
            tempFile1 = tempFile5;
            foreach (SortedList<string, string> temp1 in tempFile1)
            {
                foreach (SortedList<string, string> temp2 in tempFile2)
                {
                    try
                    {
                        if (temp1.Keys.Contains("WWN"))
                        {
                            if (temp1["WWN"] == temp2["Wwn"])
                            {

                                tempFile3.Add(temp2);
                                break;
                            }
                        }
                        else
                        {
                            if (temp1["Wwn"] == temp2["Wwn"])
                            {

                                tempFile3.Add(temp2);
                                break;
                            }
                        }
                    }
                    catch(Exception e)
                    {
                    }
                }
            }
            foreach (SortedList<string, string> temp2 in tempFile3)
            { 
                tempFile2.Remove(temp2);
            }
            List<SortedList<string, string>> tempFile4 = tempFile2;
        }
    }
}
