
using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System;


namespace ProcessManager
{
    /// <summary>
    /// The PowershellMachine is the class which is used for trigger powershell command. 
    /// </summary>
    public class PowershellMachine
    {
        # region Import ESIPSToolKit
        public int InitForEMCStorage()
        {
            //gPsMachine = psMachine;
           // Runspace runspace = GlobalData.psMachine.GetRunspace();
             
            TestLog log = TestLog.GetInstance();
            
            //create a pipeline
            Pipeline pipeline =runspace.CreatePipeline();

            pipeline.Commands.AddScript("$error.clear()");
            log.LogInfo("Set Execution Policy to RemoteSigned");
            pipeline.Commands.AddScript("Set-ExecutionPolicy RemoteSigned");
            log.LogInfo("Import ESIPSToolKit Module");
            pipeline.Commands.AddScript("Import-Module -Name ESIPSToolKit");
            try
            {
                pipeline.Invoke();
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }

            // Fetch the error output of PowerShell
            Pipeline pipeline2 = runspace.CreatePipeline();
            string scr = "if($error.count -gt 0){$error}";
            pipeline2.Commands.AddScript(scr);

            Collection<PSObject> errors = pipeline2.Invoke();

            if (errors.Count > 0)
            {
                foreach (PSObject err in errors)
                {
                    log.LogError(err.ToString());
                }

                log.LogInfo("Failed to import ESIPSToolKit Module");
                return 1;
            }
             

            return 0;
        }
        # endregion

        //public  Collection<PSObject> RunScript(string psScriptPath, string param = null, string module = null, string testMachine = "localhost")
        //{

        // //   runspace.Open();

        //    Pipeline pipeline = runspace.CreatePipeline();
        //   // pipeline.Commands.AddScript("Set-ExecutionPolicy RemoteSigned");
        //    pipeline.Commands.AddScript("Invoke-command -computerName " + testMachine + " -filepath " + psScriptPath);

        //    pipeline.Commands.Add("Out-String");
        //    Console.WriteLine("start invoke:" + psScriptPath);
        //    Collection<PSObject> results = pipeline.Invoke();
        //    //runspace.Close();

        //    Pipeline pipeline2 = runspace.CreatePipeline();

        //    string scr = "if($error.count -gt 0){$error}";

        //    pipeline2.Commands.AddScript(scr);

        //    Collection<PSObject> errors = pipeline2.Invoke();

        //    StringBuilder stringBuilder = new StringBuilder();

        //    TestLog log = TestLog.GetInstance();
        //    OutPut outPut = new OutPut();

        //    if (errors.Count == 0)
        //    {
        //        foreach (PSObject obj in results)
        //        {
        //            stringBuilder.AppendLine(obj.ToString());
        //        }

        //        log.LogInfo(stringBuilder.ToString());
        //        //Only the output of the last command will be returned
        //        outPut.OutStr = stringBuilder.ToString();
        //    }
        //    else
        //    {
        //        foreach (PSObject err in errors)
        //        {
        //            stringBuilder.AppendLine(err.ToString());
        //        }

        //        outPut.State = 1;
        //        outPut.OutStr = stringBuilder.ToString();


        //        string detail = string.Format("The script: {0} occurs ERROR \r\n [Error InFO]: {1}", psScriptPath, stringBuilder.ToString());
        //        PSException psEx = new PSException(detail);
        //        throw psEx;
        //    }


        //    return results;

        //}

        /// <summary>
        /// The constrator for PowershellMachine which will create a run space for Powershell and open the run space.
        /// </summary>
        public PowershellMachine()
        {
            runspace = RunspaceFactory.CreateRunspace();
            TestLog log = TestLog.GetInstance();
            log.LogInfo("Create RunSpace");
            runspace.Open();
            
        }

        public void CloseRunSpace()
        {
            runspace.Close();
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
        private static Runspace runspace = null;

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
            TestLog log = TestLog.GetInstance();

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
                string scriptstring = null;
                foreach (var script in scripts)
                {
                    scriptstring += script + "\r\n";
                }
                Pipeline pipeline = runspace.CreatePipeline();

                pipeline.Commands.AddScript("$error.clear()");

                pipeline.Commands.AddScript(scriptstring);
                log.LogScript(scriptstring);



                pipeline.Commands.Add("Out-String");

                Collection<PSObject> results = null;

                //There will collect powershell error message and throw as PSException.
                try
                {
                    results = pipeline.Invoke();
                }
                catch (Exception ex)
                {
                    log.LogError(ex.Message);
                }
                finally
                {
                    pipeline.Dispose();
                }


                Pipeline pipeline2 = runspace.CreatePipeline();

                string scr = "if($error.count -gt 0){$error}";

                pipeline2.Commands.AddScript(scr);

                Collection<PSObject> errors = pipeline2.Invoke();

                StringBuilder stringBuilder = new StringBuilder();

                pipeline2.Dispose();

                if (errors.Count == 0)
                {
                    foreach (PSObject obj in results)
                    {
                        stringBuilder.AppendLine(obj.ToString());
                    }

                    log.LogInfo(stringBuilder.ToString());
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


                    string detail = string.Format("The script: {0} occurs ERROR \r\n [Error InFO]: {1}", scriptstring, stringBuilder.ToString());
                    PSException psEx = new PSException(detail);
                    throw psEx;
                }
                //}

            }
            catch (PSException ex)
            {
                log.LogError(ex.messageDetail);
                //throw ex;   //No need throw exception here.
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
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
