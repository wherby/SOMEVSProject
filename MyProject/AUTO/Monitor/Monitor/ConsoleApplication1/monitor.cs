using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EMC.ESI.PowerShell.Monitor
{
    class monitor
    {
        static void Main(string[] args)
        {
            BaseClass bc = new BaseClass();
            string cmdFile = HelperAdapter.GetProperty("CMDFile");
            string[] cmds = File.ReadAllText(cmdFile).Split(new string[]{"\r\n", " "}, StringSplitOptions.RemoveEmptyEntries);

            foreach (CmdletClass cmd in bc.cmdletCollection)
            {
                if (cmds.Contains(cmd.CmdletName))
                {
                    cmd.AutoGenerate();
                }
            }
        }

    }
}
