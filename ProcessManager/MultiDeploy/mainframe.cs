/*=================================================
#* ProjectName: ProcessManager PowerShellScript WorkFlow.
#* Created: [11/05/2013]
#* Author: Tao Zhou
#*==================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProcessManager
{
    public partial class mainframe : Form
    {
        public mainframe()
        {
            InitializeComponent();
        }


        private void sourceFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SourceFolder source = new SourceFolder();
            source.ShowDialog();
        }



        private void runnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string hoststring = GlobalData.hostNameList;
            //string[] hostArray = Helper.StringSplit(hoststring);
            //foreach (string hosttemp in hostArray)
            //{
            //    string tempRoot = @"\\" + hosttemp + @"\c$\Deploy";
            //    string cmd=tempRoot+@"\test.bat >>"+tempRoot+@"\log.txt";
            //    DateTime daten= DateTime.Now;
            //    daten=daten.AddMinutes(1);
            //    string timeString=string .Format("{0:00}:{1:00}",daten.Hour,daten.Minute);

            //    string scheduleString = @"/K schtasks /create /sc once /s " + hosttemp + @" /tr """ + cmd + @""" /tn test2 /f /st " + timeString;
            //    RunCMDNoHidden( scheduleString);
            //}
        }

        private  void RunCMDNoHidden(string cmd)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = cmd;
            
            process.StartInfo = startInfo;
            process.Start();
        }

        private void mainframe_Load(object sender, EventArgs e)
        {
            GlobalData.psMachine = new PowershellMachine();
            GlobalData.psMachine.InitForEMCStorage();
            LoadConfigureData();
            //GlobalData.psMachine = null;
        }

        private void LoadConfigureData()
        {
            string allpsFilesString = Helper.GetProperty("allPSFilesFolder");
            GlobalData.allPSFiles = getALLFiles(allpsFilesString);
            string systemConfigString = Helper.GetProperty("systemConfigString");
            GlobalData.systemConfigString = systemConfigString;
            string prefixFileString = Helper.GetProperty("prefixFileFloder");
            GlobalData.prefixFileString = getALLFiles(prefixFileString);
            string postfixFileString = Helper.GetProperty("postfixFileFolder");
            GlobalData.postfixFileString = getALLFiles(postfixFileString);
        }

        private List<string> getALLFiles(string path)
        {
            return Helper.GetAllFiles(path).ToList<string>();
        }

        private void configSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigureSystem configSystem = new ConfigureSystem();
            configSystem.ShowDialog();
        }

        private void preScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreConfig pre = new PreConfig();
            pre.ShowDialog();
        }

        private void postScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PostConfig post = new PostConfig();
            post.ShowDialog();
        }

        private void runnerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Runner run = new Runner();
            run.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zhou.Tao@emc.com", "Send To",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Question);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mainframe_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalData.psMachine.CloseRunSpace();
            GlobalData.psMachine = null;
            TestLog log = TestLog.GetInstance();
            log.CloseLog();
        }

        private void mainframe_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  Helper.KillProcess("ProcessManager");
        }


  
    }
}
