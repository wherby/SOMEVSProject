using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using PowerShellTestTools;

namespace PowerShellAutomation
{

    public class [CMDString] : BaseClass
    {
        private TestLog log = TestLog.GetInstance();

        #region AutoGenerate
        
[TEMP0]
        
        /// <summary>
        /// [CMDString]
        ///     Constructor for [CMDString] class
        /// </summary>
        /// <param name=""> object string</param>
        /// <param name="cmd">command string to test</param>
        public [CMDString]([string parameters] string cmd = null)
        {

[TEMP1]
            CmdString = cmd;
            ParseParameter();  
        }

        /// <summary>
        /// ToCMDString
        ///     Override ToCMDString method in BaseClass, build a command string
        /// </summary>
        /// <returns>command string</returns>
        public override string ToCMDString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[CMD]");

[TEMP2]

            return sb.ToString();
        }
        #endregion  

        /// <summary>
        /// VerifyTheCMD
        ///     Verify whether [CMD] commands succeeds or not
        /// </summary>
        /// <param name="psMachine">powershell machine</param>
        /// <returns>the result of [CMD]</returns>
        public string VerifyTheCMD(PowershellMachine psMachine)
        {
            string result = RunCMD(psMachine, true);

            VerifyFields(psMachine, result);

            return result;
        }

        private void VerifyFields(PowershellMachine psMachine, string result)
        {
            
        }
    }
}