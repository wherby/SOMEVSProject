using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace ProcessManager
{
    public partial class Runner : Form
    {
        public Runner()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Runner_Load(object sender, EventArgs e)
        {
            foreach (string item in GlobalData.postfixFileString)
            {
                PostScriptFile.Items.Add(item,true);
            }
            foreach (string item in GlobalData.prefixFileString)
            {
                PreScriptFile.Items.Add(item,true);
            }
            foreach (string item in GlobalData.allPSFiles)
            {
                ScriptFile.Items.Add(item,true);
            }
            string[] allSystem = GlobalData.systemConfigString.Split(',');
            foreach (string temp in allSystem)
            {
                SystemConfig.Items.Add(temp,true);
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {
            GlobalData.psMachine.RunScript(new List<string>() { "$CurrentDir=\""+ GlobalData.pathCurrent+"\"" }, new List<PSParam>());
            for (int j = 0; j < SystemConfig.Items.Count; j++)
            {
                string systemType = "\""+SystemConfig.Items[j].ToString()+"\"";
                GlobalData.psMachine.RunScript(new List<string>() { string.Format("$SystemTypeString={0}", systemType) }, new List<PSParam>());
                SystemConfig.SetSelected(j, true);
                for (int i = 0; i < PreScriptFile.Items.Count; i++)
                {                    
                    if (!PreScriptFile.GetItemChecked(i))
                    {
                        continue;
                    }
                    string temp = PreScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    allPSString = CMDParser(allPSString);

                    GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });

                    PreScriptFile.SetSelected(i, true);
               //     PreScriptFile.SetItemChecked(i, true);
                }

                for (int i = 0; i < ScriptFile.Items.Count; i++)
                {
                    if (!ScriptFile.GetItemChecked(i))
                    {
                        continue;
                    }
                    string temp = ScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    allPSString = CMDParser(allPSString);

                    GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });
                    //foreach (string temp2 in allPSString)
                    //{
                    //    if (temp2.Trim() != string.Empty)
                    //    {
                    //        GlobalData.psMachine.RunScript(new List<string> { temp2 }, new List<PSParam>() { });
                    //    }
                    //}
                    ScriptFile.SetSelected(i, true);
                 //   ScriptFile.SetItemChecked(i, true);
                }

                for (int i = 0; i < PostScriptFile.Items.Count; i++)
                {
                    if (!PostScriptFile.GetItemChecked(i))
                    {
                        continue;
                    }
                    string temp = PostScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    allPSString = CMDParser(allPSString);

                    GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });

                    PostScriptFile.SetSelected(i, true);
                   // PostScriptFile.SetItemChecked(i, true);
                }

              //  SystemConfig.SetItemChecked(j, true);
            }
        }

        private string [] CMDParser(string [] allScripts)
        {
            string[] tempScripts = ParserForCMD(allScripts);
            tempScripts = ParserForDll(tempScripts);
            return tempScripts;
        }

        private static string[] ParserForCMD(string[] allScripts)
        {
            List<string> scripts = allScripts.ToList<string>();
            for (int i = 0; i < scripts.Count; i++)
            {
                if (scripts[i].IndexOf("##USING CMD##") >= 0)
                {
                    scripts.RemoveAt(i);
                    scripts.Insert(i, "cmd");
                    scripts.Add("exit");
                    List<string > preScriptTemp=new List<string>();
                    PowershellMachine ps = GlobalData.psMachine;
                    for(int j=0;j<i;j++)
                    {
                        preScriptTemp.Add(scripts[j]);
                        
                    }
                    ps.RunScript(preScriptTemp, new List<PSParam>());
                    for (int j = i; j < scripts.Count; j++)
                    {
                        string Pattern = @"##(\S+)##";
                        MatchCollection Matches = Regex.Matches(scripts[j], Pattern,RegexOptions.IgnoreCase|RegexOptions.ExplicitCapture);
                        foreach (Match nextMatch in Matches)
                        {
                            string nextMatchString = nextMatch.ToString();
                            string paraTemp= nextMatchString.Replace("#", "");
                            string result = ps.RunScript(new List<string> { paraTemp }, new List<PSParam>()).OutStr;
                            result = result.Trim();
                            scripts[j] = scripts[j].Replace(nextMatchString, result);
                        }
                    }
                }
            }
            scripts.Add(" "); //add empty line to split script to pieces.



            return scripts.ToArray();
        }

        private static string[] ParserForDll(string[] allScripts)
        {
            List<string> scripts = allScripts.ToList<string>();

            if (scripts[0].IndexOf("##DLL##") >= 0)
            {
                string dllLocation = scripts[1];
                string className = scripts[2];
                string methodName = scripts[3];
                scripts.Clear();
                scripts.Add(string.Format(@"$tempFile=$currentDir+""{0}""", dllLocation));
                scripts.Add("[void][reflection.assembly]::loadfile($tempFile)");
                scripts.Add(string.Format("$tempObj=New-Object {0}",className));
                string[] methodsArg = methodName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string temp = "$tempObj." + methodsArg[0]+"(";
                for (int i = 1; i < methodsArg.Count<string>(); i++)
                {
                    temp += methodsArg[i].ToString() +",";
                }
                temp = temp.Trim(new char[] { ',' });
                temp += ")";
                scripts.Add(temp);
            }
            
            scripts.Add(" "); //add empty line to split script to pieces.



            return scripts.ToArray();
        }

        private void ScriptGenerator_Click(object sender, EventArgs e)
        {
            List<string> scriptsList = new List<string>();
            scriptsList.Add("$CurrentDir=\"" + GlobalData.pathCurrent + "\"");
           // GlobalData.psMachine.RunScript(new List<string>() { "$CurrentDir=\"" + GlobalData.pathCurrent + "\"" }, new List<PSParam>());
            for (int j = 0; j < SystemConfig.Items.Count; j++)
            {
                string systemType = "\"" + SystemConfig.Items[j].ToString() + "\"";
               
                scriptsList.Add(string.Format("$SystemTypeString={0}", systemType));

                for (int i = 0; i < PreScriptFile.Items.Count; i++)
                {
                    if (!PreScriptFile.GetItemChecked(i))
                    {
                        continue;
                    }
                    string temp = PreScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    allPSString = CMDParser(allPSString);

                    scriptsList.AddRange(allPSString);

                }

                for (int i = 0; i < ScriptFile.Items.Count; i++)
                {
                    if (!ScriptFile.GetItemChecked(i))
                    {
                        continue;
                    }
                    string temp = ScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    allPSString = CMDParser(allPSString);

                    scriptsList.AddRange(allPSString);
                }

                for (int i = 0; i < PostScriptFile.Items.Count; i++)
                {
                    if (!PostScriptFile.GetItemChecked(i))
                    {
                        continue;
                    }
                    string temp = PostScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    allPSString = CMDParser(allPSString);


                    scriptsList.AddRange(allPSString);

                }

                //  SystemConfig.SetItemChecked(j, true);
            }
            File.WriteAllLines("ScriptOut.ps1", scriptsList.ToArray());
        }
    }
}
