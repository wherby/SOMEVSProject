using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Facet.Combinatorics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using System.Collections;


namespace CaseGenerate
{
    class Program
    {
        static void Main(string[] args)
        {

            string dirPath = @"c:\users\administrator.sr5dom\desktop\";
            string assemblytPath = System.AppDomain.CurrentDomain.BaseDirectory;                     
            
           // string dirPath = args[0];
            Console.WriteLine(dirPath);

            string dir = dirPath + @"\Combination\";            
        
            string scriptFile = assemblytPath + @"..\..\getparameter.ps1";
            string result = RunScript(scriptFile);            

            StreamReader file = null;
            string filePath = result.Trim();
            Console.WriteLine(filePath);
            if (File.Exists(filePath))
            {
                file = new StreamReader(filePath);
            }
            else
            {
                throw new FileNotFoundException("Wrong path for the script file");
            }
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }
            Directory.CreateDirectory(dir);


            string sLine = string.Empty;
            StreamWriter caseFile = null;          
            string[] alter_params = null;
            string[] mandatory_params = null;
            string cmd = null;            
            List<string> param = new List<string>();
            Dictionary<string, int> dic = new Dictionary<string,int>();
            Dictionary<string, int> dic_param = new Dictionary<string,int>();

            int n = 0;

            while ((sLine = file.ReadLine()) != null)
            {
                string[] split = sLine.Split(new char[] { ':' });                

                if (split[0].Equals("mandatory"))
                {
                    mandatory_params = split[1].Split(new char[] { ',' });                                        
                }
                else if (split[0].Equals("optional"))
                {
                    string[] option_split = split[1].Split(new char[] { ',' });                    
                    
                    string str_case = cmd;
                    for (int i = 0; i <= option_split.Length; i++)
                    {
                        Combinations<string> combinations = new Combinations<string>(option_split, i);
                        foreach (IList<string> c in combinations)
                        {
                            str_case = null;
                            param.Clear();
                            param.Add(cmd);

                            foreach (string str in mandatory_params)
                            {
                                param.Add(str);
                                if (!dic_param.ContainsKey(str))
                                {
                                    
                                    dic_param.Add(str,1);
                                }
                            }

                            foreach (string str in c)
                            {
                                param.Add(str);
                                if (!dic_param.ContainsKey(str))
                                {
                                    dic_param.Add(str, 1);
                                }
                            }


                            if (alter_params != null)
                            {
                                List<string> str_alter = new List<string>();
                                foreach (string str in alter_params)
                                {
                                    string[] str_alter_set = str.Split(new char[] { '.' });
                                    str_alter.Add(str_alter_set[0]);
                                    int position = int.Parse(str_alter_set[1]);

                                    for (int j = param.Count - 1; j >= 0; j--)
                                    {
                                        string parameter = param[j];
                                        if (parameter.Contains(str_alter_set[0]))
                                        {
                                            param.RemoveAt(j);
                                            param.Insert(position, parameter);
                                        }
                                    }
                                    
                                }
                                foreach (string str in param)
                                {
                                    str_case += str;
                                }
                                str_case += "\r\n";
                                if (dic.ContainsKey(str_case))
                                {
                                    continue;
                                }
                                else
                                {
                                    dic.Add(str_case,1);
                                    
                                }
                                int m = 1;
                                foreach (string str in str_alter)
                                {
                                    if (str_case.Contains(str))
                                    {
                                        string new_str_case = str_case.Replace(str, "");
                                        str_case += new_str_case;
                                        m *= 2;
                                       
                                    }
                                }
                                n += m;
                            }
                            else
                            {
                                foreach (string str in param)
                                {
                                    str_case += str;
                                }
                                str_case += "\r\n";
                                n++;
                            }
                            caseFile.Write(str_case);
                        }
                    }

                    alter_params = null;
                }
                else if (split[0].Equals("alternate") && split.Length > 1)
                {
                    alter_params = split[1].Split(new char[] { ',' });

                }
                else
                {
                    if (caseFile != null)
                    {
                        caseFile.Close();
                    }
                    cmd = split[0];
                    caseFile = File.CreateText(dir + cmd + ".txt");
                    dic.Clear();
                }
                
            }
            caseFile.Close();
            //StreamWriter paramFile = File.CreateText(dirPath+"_param.txt");
            //foreach (KeyValuePair<string, int> str in dic_param)
            //{
              
            //    paramFile.WriteLine(str.Key);
            //}
            //paramFile.Close();
            Console.WriteLine(n);           
            
        }

        private static string RunScript(string psScriptPath)
        {

            string psScript = string.Empty;
            if (File.Exists(psScriptPath))
                psScript = File.ReadAllText(psScriptPath);
            else
                throw new FileNotFoundException("Wrong path for the script file");

            
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript("Set-ExecutionPolicy RemoteSigned");           
            pipeline.Commands.AddScript(psScript);
            pipeline.Commands.Add("Out-String");
            Console.WriteLine("invoke"+ psScriptPath);
            Collection<PSObject> results = pipeline.Invoke();
            Console.WriteLine("end");
            runspace.Close();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (PSObject obj in results)
            {

                stringBuilder.AppendLine(obj.ToString());

            }

            return stringBuilder.ToString();

        }


    }
}
