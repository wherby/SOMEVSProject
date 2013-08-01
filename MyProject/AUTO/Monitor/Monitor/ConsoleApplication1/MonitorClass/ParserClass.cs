using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.IO;

namespace EMC.ESI.PowerShell.Monitor
{
    public class BaseClass
    {
        public List<string> cmdStringList = new List<string>();
        public List<CmdletClass> cmdletCollection = new List<CmdletClass>();

        public BaseClass()
        {
            TestLog log = TestLog.GetInstance();
            string fileName = HelperAdapter.GetProperty("InputFile");
            if (!File.Exists(fileName))
            {
                log.LogError("File not exist");
            }
            string[] allCMDLines = File.ReadAllLines(fileName);
            string tempCmdString = string.Empty;
            for(int i=0;i<allCMDLines.Count<string>();i++)
            {
                string temp = allCMDLines[i];
                if ((temp.IndexOf("alternate") < 0) && (temp.IndexOf("mandatory") < 0) && (temp.IndexOf("optional") < 0))
                { 
                    if (tempCmdString != string.Empty)
                    {
                        tempCmdString = tempCmdString.Trim();
                        cmdStringList.Add(tempCmdString);
                        tempCmdString = string.Empty;
                    }
                }
                tempCmdString += "\r\n"+temp;
                if (i == allCMDLines.Count<string>() - 1)
                {
                    cmdStringList.Add(tempCmdString);
                    tempCmdString = string.Empty;
                }
            }
            foreach(string  cmdStringTemp in cmdStringList)
            {
                CmdletClass cmd = new CmdletClass(cmdStringTemp);
                if (!cmd.CmdletName.Contains("Credential"))
                {
                    cmdletCollection.Add(cmd);
                }
            }
        }
        public virtual string GetString()
        {
            return "Base Class";
        }
    }
}
