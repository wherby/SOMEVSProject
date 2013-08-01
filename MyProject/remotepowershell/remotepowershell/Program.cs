using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation; // Windows PowerShell namespace  
using System.Management.Automation.Runspaces; // Windows PowerShell namespace  
using System.Security; // For the secure password 
using System.Collections;
using System.Collections.ObjectModel;


namespace remotepowershell
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(RunScript(null));
        }
        public static string RunScript(string scriptText)  

         {  

    

            Runspace remoteRunspace = null;  

            openRunspace("http://lamanna-test8-116:5985/wsman",

                 "http://schemas.microsoft.com/powershell/Microsoft.PowerShell",

                 @"sr5dom\administrator",

                 "Password!",  

                ref remoteRunspace);  

    

                 StringBuilder stringBuilder = new StringBuilder();  

                 using (PowerShell powershell = PowerShell.Create())  

                 {  

                     powershell.Runspace = remoteRunspace;  

                     powershell.AddCommand("get-process");  

                     powershell.Invoke();  

                     Collection<PSObject> results = powershell.Invoke();  

                     remoteRunspace.Close();  

                     foreach (PSObject obj in results)  

                     {  

                         stringBuilder.AppendLine(obj.ToString());  

                     }  

                 }  

    

                 return stringBuilder.ToString();  

          } 

         public static void  openRunspace(string uri, string schema, string username, string livePass, ref Runspace remoteRunspace)  

{  

     System.Security.SecureString password = new System.Security.SecureString();  

     foreach (char c in livePass.ToCharArray())  

     {  

         password.AppendChar(c);  

     }  

     PSCredential psc = new PSCredential(username, password);

     WSManConnectionInfo rri = new WSManConnectionInfo(new Uri(uri), schema, psc);  

     rri.AuthenticationMechanism = AuthenticationMechanism.Basic;  

     rri.ProxyAuthentication = AuthenticationMechanism.Negotiate;  

     remoteRunspace = RunspaceFactory.CreateRunspace(rri);  

     remoteRunspace.Open();  

 } 


    }
}
