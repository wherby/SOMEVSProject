using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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

                    GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });

                    PostScriptFile.SetSelected(i, true);
                   // PostScriptFile.SetItemChecked(i, true);
                }

              //  SystemConfig.SetItemChecked(j, true);
            }
        }

        private void ScriptGenerator_Click(object sender, EventArgs e)
        {
            List<string> scriptsList = new List<string>();
            scriptsList.Add("$CurrentDir=\"" + GlobalData.pathCurrent + "\"");
           // GlobalData.psMachine.RunScript(new List<string>() { "$CurrentDir=\"" + GlobalData.pathCurrent + "\"" }, new List<PSParam>());
            for (int j = 0; j < SystemConfig.Items.Count; j++)
            {
                string systemType = "\"" + SystemConfig.Items[j].ToString() + "\"";
                //GlobalData.psMachine.RunScript(new List<string>() { string.Format("$SystemTypeString={0}", systemType) }, new List<PSParam>());
                scriptsList.Add(string.Format("$SystemTypeString={0}", systemType));
               // SystemConfig.SetSelected(j, true);
                for (int i = 0; i < PreScriptFile.Items.Count; i++)
                {
                    if (!PreScriptFile.GetItemChecked(i))
                    {
                        continue;
                    }
                    string temp = PreScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    scriptsList.AddRange(allPSString);
                   // GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });

                   // PreScriptFile.SetSelected(i, true);
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
                    scriptsList.AddRange(allPSString);
                    //GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });
                    //foreach (string temp2 in allPSString)
                    //{
                    //    if (temp2.Trim() != string.Empty)
                    //    {
                    //        GlobalData.psMachine.RunScript(new List<string> { temp2 }, new List<PSParam>() { });
                    //    }
                    //}
                   // ScriptFile.SetSelected(i, true);
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

                    scriptsList.AddRange(allPSString);

                   // GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });

                  //  PostScriptFile.SetSelected(i, true);
                    // PostScriptFile.SetItemChecked(i, true);
                }

                //  SystemConfig.SetItemChecked(j, true);
            }
            File.WriteAllLines("ScriptOut.ps1", scriptsList.ToArray());
        }
    }
}
