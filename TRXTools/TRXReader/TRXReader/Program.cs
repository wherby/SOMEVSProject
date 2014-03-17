using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;


using Excel = Microsoft.Office.Interop.Excel; 


namespace TRXReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileFolder = Environment.CurrentDirectory;
            string[] trxFiles= Directory.GetFiles(fileFolder, "*.trx");
            List<string> result = trxFiles.ToList<string>();
            List<string> NotPassed = result.FindAll(res => !AnalyisResult(res));
            List<string> AllPassed = result.FindAll(res => AnalyisResult(res));
            CPToNewFolder(NotPassed);
            LogToSuccess(AllPassed);
           // FillTheXmL(AllPassed);
        }

        private static bool AnalyisResult(string trxFiles)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlTextReader reader = new XmlTextReader(trxFiles);
            xmlDoc.Load(reader);
            reader.Close();
            XmlNode root = xmlDoc.DocumentElement;
            XmlNodeList ele = root.SelectNodes("/*/*");

            var n2 = ele[2];
            var pass = n2.FirstChild.Attributes["passed"].Value;
            var total = n2.FirstChild.Attributes["total"].Value;
            var executed = n2.FirstChild.Attributes["executed"].Value;

            string allMessage = n2.FirstChild.OuterXml;
            return executed == pass;
        }

        private static void CPToNewFolder(List<string> NotPassed)
        {
            if (NotPassed.Count == 0)
            {
                return;
            }
            FileInfo fileinfo = new FileInfo(NotPassed[0]);
            string Dn=fileinfo.DirectoryName;
            FileInfo fileinfo2 = new FileInfo(Dn);
            string Dn2 = fileinfo2.DirectoryName+@"\NotPassed";
            DirectoryInfo d3 = new DirectoryInfo(Dn2);
            if (!d3.Exists)
            {
                d3.Create();
            }
            foreach (string npf in NotPassed)
            {
                FileInfo tf=new FileInfo(npf);
                File.Copy(npf, Dn2 +@"\"+ tf.Name,true);
            }
        }

        private static void LogToSuccess(List<string> passed)
        {
            if (passed.Count == 0)
            {
                return;
            }
            FileInfo fileinfo = new FileInfo(passed[0]);
            string Dn = fileinfo.DirectoryName;
            FileInfo fileinfo2 = new FileInfo(Dn);
            string Dn2 = fileinfo2.DirectoryName + @"\NotPassed";
            string passedLog=Dn2+@"\passedCMD.txt";
            if (!File.Exists(passedLog))
            {
                FileStream fs= File.Create(passedLog);
                fs.Close();
            }
  
            foreach(string ps in passed)
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlTextReader reader = new XmlTextReader(ps);
                xmlDoc.Load(reader);
                reader.Close();
                XmlNode root = xmlDoc.DocumentElement;
                XmlNodeList ele = root.SelectNodes("/*/*");

                var n2 = ele[2];
                var pass = n2.FirstChild.Attributes["passed"].Value;
                FileInfo tpFI = new FileInfo(ps);
                string fileCMD = tpFI.Name.Replace(".trx", "");
                File.AppendAllLines(passedLog, new string[] { string.Format(fileCMD + "=" + pass) });
            }
        }

        private  void FillTheXmL(List<string> Passed)
        {
            Excel.Application excelAppSrc;
            excelAppSrc = new Excel.Application();
            
            excelAppSrc.Visible = false;
            Excel._Worksheet oSheetSrc;

            string workbookPathSrc = Environment.CurrentDirectory + @"\Template.xls";
            if (!File.Exists(workbookPathSrc))
            {
                return;
            }

            FileInfo fileinfo = new FileInfo(Passed[0]);
            string Dn = fileinfo.DirectoryName;
            FileInfo fileinfo2 = new FileInfo(Dn);
            string Dn2 = fileinfo2.DirectoryName + @"\NotPassed";
            string workbookPathSrc2 = Dn2 + @"\Template.xls";
            File.Copy(workbookPathSrc, workbookPathSrc2, true);


            Excel.Workbook excelWorkbookSrc = excelAppSrc.Workbooks.Open(workbookPathSrc2,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
            oSheetSrc = (Excel._Worksheet)excelWorkbookSrc.Sheets[1];


            int startNum = 0;
            int endNum = 1000;
            for (int j = 1; j < 100; j++)
            {
                string temp = ((Microsoft.Office.Interop.Excel.Range)oSheetSrc.Cells[j, 1]).Value;
                if (temp == "Cmdlets")
                {
                    startNum = j;
                }
            }
            for (int j = 1; j < 1000; j++)
            {
                string temp = ((Microsoft.Office.Interop.Excel.Range)oSheetSrc.Cells[j, 1]).Value;
                if (temp == "Total")
                {
                    endNum = j;
                }
            }

            foreach (string tp in Passed)
            {
                FileInfo tpFI = new FileInfo(tp);
                string fileCMD = tpFI.Name.Replace(".trx", "");
                for (int i = startNum; i <= endNum; i++)
                {
                    string cmdName = ((Microsoft.Office.Interop.Excel.Range)oSheetSrc.Cells[i, 1]).Value;
                    if (cmdName.Replace("-", "") == fileCMD)
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlTextReader reader = new XmlTextReader(tp);
                        xmlDoc.Load(reader);
                        reader.Close();
                        XmlNode root = xmlDoc.DocumentElement;
                        XmlNodeList ele = root.SelectNodes("/*/*");

                        var n2 = ele[2];
                        var pass = n2.FirstChild.Attributes["passed"].Value;
                        oSheetSrc.Cells[i, 8] = pass;
                        
                    }
                }
            }
          //  excelWorkbookSrc.Close();
            excelAppSrc.Visible = true;
        }
        
    }
}
