using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMC.ESI.PowerShell.Monitor
{
    public partial class CmdletClass
    {
        string cmdletName;
        ElementClass element;
        public CmdletClass(string cmdString)
        {
            string[] strs = cmdString.Split(new string[] { "\r\n" }, 2, StringSplitOptions.RemoveEmptyEntries);

            string[] strSection = strs[0].Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
            cmdletName = strSection[0];
            element = new ElementClass(strs[1]);

        }

        public string CmdletName
        {
            get
            {
                return cmdletName;
            }
        }
    }
}
