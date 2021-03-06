﻿//#define Debug
using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System;


namespace PsAgent
{
    public class Log
    {
        public static void LogInfo(string info)
        {
#if Debug1
            Console.WriteLine(info);
#endif
        }
    }

    /// <summary>
    /// The PowershellMachine is the class which is used for trigger powershell command. 
    /// </summary>
    public class PowershellMachine
    {
        /// <summary>
        /// The constrator for PowershellMachine which will create a run space for Powershell and open the run space.
        /// </summary>
        public PowershellMachine()
        {
            runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
        }
        /// <summary>
        /// The constrator for PowershellMachine which will create a run space for remote Powershell and open the run space.
        /// </summary>
        /// <param name="servername">The remote server where powershell command will be invoked.</param>
        /// <param name="username">Domain\Username of remote server.</param>      
        /// <param name="password">Password to login remote server </param>
        public PowershellMachine(string servername, string username, string password)
        {
            Uri uri = new Uri(@"http://" + servername + ":5985/wsman");
            string schema = @"http://schemas.microsoft.com/powershell/Microsoft.PowerShell";
            System.Security.SecureString pwd = new System.Security.SecureString();
            foreach (char c in password.ToCharArray())
            {
                pwd.AppendChar(c);
            }
            PSCredential psc = new PSCredential(username, pwd);
            WSManConnectionInfo rri = new WSManConnectionInfo(uri, schema, psc);
            rri.AuthenticationMechanism = AuthenticationMechanism.Kerberos;
            rri.ProxyAuthentication = AuthenticationMechanism.Negotiate;

            runspace = RunspaceFactory.CreateRunspace(rri);
            runspace.Open();
        }

        /// <summary>
        /// The deconstructor for PowershellMchine which will close run space.
        /// </summary>
        ~PowershellMachine()
        {
            runspace.Close();
        }

        /// <summary>
        /// The runspace instance for PowershellMachine.
        /// </summary>
        private Runspace runspace = null;

        /// <summary>
        /// Get runspace for the PowershellMachine.
        /// </summary>
        /// <returns>The runspace for the PoweeshellMachine.</returns>
        public Runspace GetRunspace()
        {
            return runspace;
        }

        /// <summary>
        /// The mothod will invoke the powershell scripts in powershell run space.
        /// </summary>
        /// <param name="scripts">The powershell script which will be excuted.</param>
        /// <param name="pars">The powershell session variables values.</param>
        /// <returns>The result of powershell scripts.</returns>
        public OutPut RunScript(List<string> scripts, List<PSParam> pars)
        {
           // TestLog log = TestLog.GetInstance();

            OutPut outPut = new OutPut();

            if (pars != null)
            {
                foreach (var par in pars)
                {
                    runspace.SessionStateProxy.SetVariable(par.Key, par.Value);
                }
            }

            try
            {
                foreach (var script in scripts)
                {
                    Pipeline pipeline = runspace.CreatePipeline();

                    pipeline.Commands.AddScript("$error.clear()");
                    pipeline.Commands.AddScript(script);
                    //log.LogScript(script);
                    Log.LogInfo(script);

                    pipeline.Commands.Add("Out-String");

                    Collection<PSObject> results = null;

                    //There will collect powershell error message and throw as PSException.
                    try
                    {
                        results = pipeline.Invoke();
                    }
                    catch (Exception ex)
                    {
                        Log.LogInfo(ex.Message);
                        //log.LogError(ex.Message);
                    }

                    Pipeline pipeline2 = runspace.CreatePipeline();

                    string scr = "if($error.count -gt 0){$error}";

                    pipeline2.Commands.AddScript(scr);

                    Collection<PSObject> errors = pipeline2.Invoke();

                    StringBuilder stringBuilder = new StringBuilder();

                    if (errors.Count == 0)
                    {
                        foreach (PSObject obj in results)
                        {
                            stringBuilder.AppendLine(obj.ToString());
                        }
                        Log.LogInfo(stringBuilder.ToString());
                       // Console.WriteLine(stringBuilder.ToString());
                        //log.LogInfo(stringBuilder.ToString());
                        //Only the output of the last command will be returned
                        outPut.OutStr = stringBuilder.ToString();
                    }
                    else
                    {
                        foreach (PSObject err in errors)
                        {
                            stringBuilder.AppendLine(err.ToString());
                        }

                        outPut.State = 1;
                        outPut.OutStr = stringBuilder.ToString();

                        string detail = string.Format("The script: {0} occurs ERROR \r\n [Error InFO]: {1}", script, stringBuilder.ToString());
                        PSException psEx = new PSException(detail);
                       // throw psEx;
                    }
                }
            }
            catch (PSException ex)
            {
                Log.LogInfo(ex.messageDetail);
                //log.LogError(ex.messageDetail);
                throw ex;
            }
            //return string.Empty;
            return outPut;
        }

    }

    /// <summary>
    /// The exception which is triggered by PowerShell.
    /// </summary>
    public class PSException : Exception
    {
        /// <summary>
        /// The error message for exception.
        /// </summary>
        public string messageDetail;
        /// <summary>
        /// The construtor for PSException
        /// </summary>
        /// <param name="message">The error message for exception.</param>
        public PSException(string message)
            : base(message)
        {
            messageDetail = message;
        }
    }

    #region PSparam

    /// <summary>
    /// The class PSParam is used for PowerShell Session variable setting.
    /// </summary>
    public class PSParam
    {
        /// <summary>
        /// The variable name for PowerShell.
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// The variable value for PowerShell
        /// </summary>
        public object Value
        {
            get;
            set;
        }

    }

    /// <summary>
    /// The OutPut is the class for the result for poeweshell command.
    /// </summary>
    public class OutPut
    {
        int state;
        string outStr;

        /// <summary>
        /// The constructor for OutPut class. 
        /// </summary>
        public OutPut()
        {
            state = 0;
            outStr = string.Empty;
        }

        /// <summary>
        /// The State property is stand for the command status.
        /// </summary>
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        /// <summary>
        /// The OutStr property is the value for the result for powershell command.
        /// </summary>
        public string OutStr
        {
            get
            {
                return outStr;
            }
            set
            {
                outStr = value;
            }
        }
    }
    #endregion

}