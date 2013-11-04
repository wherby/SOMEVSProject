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
                PostScriptFile.Items.Add(item);
            }
            foreach (string item in GlobalData.prefixFileString)
            {
                PreScriptFile.Items.Add(item);
            }
            foreach (string item in GlobalData.allPSFiles)
            {
                ScriptFile.Items.Add(item);
            }
            string[] allSystem = GlobalData.systemConfigString.Split(',');
            foreach (string temp in allSystem)
            {
                SystemConfig.Items.Add(temp);
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < SystemConfig.Items.Count; j++)
            {
                string systemType = "\""+SystemConfig.Items[j].ToString()+"\"";
                GlobalData.psMachine.RunScript(new List<string>() { string.Format("$SystemTypeString={0}", systemType) }, new List<PSParam>());

                for (int i = 0; i < PreScriptFile.Items.Count; i++)
                {
                    string temp = PreScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });

                    PreScriptFile.SetSelected(i, true);
                    PreScriptFile.SetItemChecked(i, true);
                }

                for (int i = 0; i < ScriptFile.Items.Count; i++)
                {
                    string temp = ScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);
                    foreach (string temp2 in allPSString)
                    {
                        if (temp2.Trim() != string.Empty)
                        {
                            GlobalData.psMachine.RunScript(new List<string> { temp2 }, new List<PSParam>() { });
                        }
                    }
                    ScriptFile.SetSelected(i, true);
                    ScriptFile.SetItemChecked(i, true);
                }

                for (int i = 0; i < PostScriptFile.Items.Count; i++)
                {
                    string temp = PostScriptFile.Items[i].ToString();

                    string[] allPSString = File.ReadAllLines(temp);

                    GlobalData.psMachine.RunScript(allPSString.ToList<string>(), new List<PSParam>() { });

                    PostScriptFile.SetSelected(i, true);
                    PostScriptFile.SetItemChecked(i, true);
                }
                SystemConfig.SetSelected(j, true);
                SystemConfig.SetItemChecked(j, true);
            }
        }
    }
}
