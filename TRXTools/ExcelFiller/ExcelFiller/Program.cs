using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;


using Excel = Microsoft.Office.Interop.Excel; 


namespace ExcelFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allcmd = File.ReadAllLines("passedCMD.txt");
            List<string> allcmdList = allcmd.ToList<string>();
            FillTheXmL(allcmdList);
        }

        private static void FillTheXmL(List<string> Passed)
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
            DirectoryInfo d3 = new DirectoryInfo(Dn2);
            if (!d3.Exists)
            {
                d3.Create();
            }

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
                string[] tps = tp.Split('=');

                string fileCMD = tps[0];
                for (int i = startNum; i <= endNum; i++)
                {
                    string cmdName = ((Microsoft.Office.Interop.Excel.Range)oSheetSrc.Cells[i, 1]).Value;
                    if (cmdName.Replace("-", "") == fileCMD)
                    {
                        oSheetSrc.Cells[i, 8] = tps[1];
                    }
                }
            }
            //  excelWorkbookSrc.Close();
            excelAppSrc.Visible = true;
        }

    }
}
