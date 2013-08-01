using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using EMC.ESI.PowerShell.Helper;
using System.IO;
using System.Security.AccessControl;

namespace CaseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            TestLog log = TestLog.GetInstance();
            string assemblytPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string projectString = null;
            string cmdToTestFile = null;

            if (args.Length == 2)
            {
                projectString = args[0];
                cmdToTestFile = args[1];
            }
            else
            {
                projectString = HelperAdapter.GetProperty("ProjectName");
                
            }
            string[] projects = projectString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string solution = HelperAdapter.GetProperty("SolutionFolder");
            string cmdToTest = HelperAdapter.GetProperty("CommandsToTest");

            foreach (string project in projects)
            {
                string dropFolder = assemblytPath + solution + project + @"\Test Methods";
                string module = "ESIPSTOOLKit";
                if (project.ToLower().Contains("service"))
                {
                    module = "ESIServicePSTOOLKit";
                }
                if (HelperAdapter.GetProperty("ProjectName") == "VSSAutomation")
                {
                    module = "EMC.WinApps.Backup.Vss";
                }

                if (cmdToTestFile == null)
                {
                    cmdToTestFile = assemblytPath + HelperAdapter.GetProperty("CMDFolder") + module + HelperAdapter.GetProperty("CMDFileSuffix");
                }

                string[] cmds;
                string isFile = HelperAdapter.GetProperty("IsFile");
                if (isFile.ToLower() == "true")
                {
                    FileInfo cmdFile = new FileInfo(cmdToTestFile);
                    cmdToTest = cmdFile.OpenText().ReadToEnd();
                    //temp is used for report server query.
                    string temp = cmdToTest.Replace(" ", "").Replace("-", "").Replace("\r\n", " ");
                    cmds = cmdToTest.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    cmds = cmdToTest.Split(',');
                }

                string testType = HelperAdapter.GetProperty("TestType");

                #region dropFolder
                if (Directory.Exists(dropFolder))
                {
                    DirectoryInfo dir = new DirectoryInfo(dropFolder);
                    DirectorySecurity ds = dir.GetAccessControl();
                    ds.SetAccessRule(new FileSystemAccessRule("administrators", FileSystemRights.FullControl, AccessControlType.Allow));
                    dir.SetAccessControl(ds);

                    Directory.Delete(dropFolder, true);
                }
                Directory.CreateDirectory(dropFolder);
                #endregion

                List<string> buildString = new List<string>();

                #region generate test method.
                if (testType == "both")
                {
                    GenerateTestMethod(cmds, "positive", buildString, module, dropFolder);
                    GenerateTestMethod(cmds, "negative", buildString, module, dropFolder);

                }
                else if (testType == "positive")
                {
                    GenerateTestMethod(cmds, "positive", buildString, module, dropFolder);
                }
                else if (testType == "negative")
                {
                    GenerateTestMethod(cmds, "negative", buildString, module, dropFolder);
                }
                else
                {
                    throw new Exception("Error: Invalid test type");
                }
                #endregion

                string templateFolder = assemblytPath + HelperAdapter.GetProperty("TemplateFolder");
                string projectFile = templateFolder + "\\PowerShellAutomation.csproj";
                string dropProjectFile = assemblytPath + solution + project + @"\" + project + ".csproj";

                # region create project file template
                if (File.Exists(dropProjectFile))
                {
                    string[] curProjectLines = File.ReadAllLines(dropProjectFile);
                    if (File.Exists(projectFile))
                    {
                        File.SetAttributes(projectFile, ~FileAttributes.ReadOnly);
                        File.Delete(projectFile);
                    }
                    using (StreamWriter writer = File.CreateText(projectFile))
                    {
                        foreach (string line in curProjectLines)
                        {
                            string temp = line;
                            if (temp.Contains("Test Methods"))
                            {
                                continue;
                            }

                            if (temp.Contains("TestSetup.cs"))
                            {
                                writer.WriteLine(temp);
                                writer.WriteLine("[*MethodName*]");
                            }
                            else
                            {
                                writer.WriteLine(temp);
                            }

                        }
                    }

                    File.SetAttributes(dropProjectFile, ~FileAttributes.ReadOnly);
                    File.Delete(dropProjectFile);
                }
                # endregion

                # region Generate New Project File
                string[] projectLines = File.ReadAllLines(projectFile);
                using (StreamWriter writer = File.CreateText(dropProjectFile))
                {
                    foreach (string line in projectLines)
                    {
                        string temp = line;
                        if (temp.IndexOf("[*MethodName*]") < 0)
                        {
                            writer.WriteLine(temp);
                        }
                        else
                        {
                            foreach (string bString in buildString)
                            {
                                writer.WriteLine(bString);
                            }
                        }
                    }
                    writer.Close();
                }
                # endregion

                cmdToTestFile = null;
            }
           
        }
        /// <summary>
        /// GenerateTestMethod:
        ///       generate test method for specified testType: positive, negative
        /// </summary>
        /// <param name="cmds"></param>
        /// <param name="testType"></param>     
        public static void GenerateTestMethod(string[] cmds, string testType, List<string> buildString, string module, string dropFolder)
        {           
            # region Variable Declaration
            string assemblytPath = System.AppDomain.CurrentDomain.BaseDirectory;

            string combinationFolder = HelperAdapter.GetProperty("CombinationFolder") + @"\" + module + @"\";
            DirectoryInfo dropFolderInfor = new DirectoryInfo(dropFolder);
            dropFolder = dropFolderInfor.FullName;
            string templateFolder = assemblytPath + HelperAdapter.GetProperty("TemplateFolder");
            string templateFile = templateFolder + "\\Template.cs";
            string testMethod = templateFolder + "\\TestMethodTemplate.cs"; ;
            string[] testMethodLines = File.ReadAllLines(testMethod);
            # endregion

            # region Generate Test Method for each Command
            foreach (string entry in cmds)
            {
                string cmd = entry.Trim();

                # region Variable Declaration
                string cmdWithoutHyphen = cmd.Replace("-", "");
                string testClass = cmdWithoutHyphen;
                string dropFileName = null;
                string dropFile = null;                
                string cmdFile = null;
                string[] cmdLines = { };
                string[] templateLines = { };
                string subDropFolder = dropFolder + @"\" + cmd;
                # endregion

                #region subDropFolder
                if ( Directory.Exists(subDropFolder) == false)
                {
                    Directory.CreateDirectory(subDropFolder);
                }
                
                #endregion
                                
                if (testType.ToLower() == "positive")
                {
                    dropFileName = cmd + "Test" + ".cs";
                    dropFile = subDropFolder + @"\" + dropFileName;
                    cmdFile = combinationFolder + @"\positive\" + cmd + ".txt";                    
                }
                else if (testType.ToLower() == "negative")
                {
                    dropFileName = "N" + cmd + "Test" + ".cs";
                    dropFile = subDropFolder + @"\" + dropFileName;
                    cmdFile = combinationFolder + @"\negative\" + "N" + cmd + ".txt";                    
                    cmdWithoutHyphen = cmdWithoutHyphen + "Negative";
                }
                
                try
                {
                    cmdLines = File.ReadAllLines(cmdFile);
                    if (cmdLines.Length == 0)
                    {
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Warning: {0} doesn't exist", cmdFile);                    
                    continue;
                }

                try
                {
                    templateLines = File.ReadAllLines(templateFile);
                }
                catch
                {
                    Console.WriteLine("Warning: {0} doesn't exist", templateFile);
                    continue;
                }
                
                # region Write test method file
                using (StreamWriter writer = File.CreateText(dropFile))
                {
                    foreach (string templateLine in templateLines)
                    {
                        string temp = templateLine;
                        if (temp.Contains("[*test*]"))
                        {
                            temp = temp.Replace("[*test*]", "");
                            writer.WriteLine(temp);

                            int i = 0;
                            foreach (string cmdline in cmdLines)
                            {
                                if (cmdline.Trim() == string.Empty)
                                {
                                    continue;
                                }

                                foreach (string line in testMethodLines)
                                {
                                    string templine = line;

                                    templine = templine.Replace("[*para*]", (i + 1).ToString());
                                    if (!cmdWithoutHyphen.ToLower().Contains( "boundary"))
                                    {
                                        templine = templine.Replace("[*testName*]", cmdWithoutHyphen);
                                        templine = templine.Replace("[*cmd*]", "string cmd = \"" + cmdline + "\";");
                                        templine = templine.Replace("[*cmdString*]", "cmd");
                                    }
                                    else
                                    {
                                        string boundaryCmd =cmdline.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("-", "");
                                        templine = templine.Replace("[*testName*]", boundaryCmd + cmdWithoutHyphen);
                                        templine = templine.Replace("[*cmd*]", string.Empty);
                                        templine = templine.Replace("[*cmdString*]", string.Empty);
                                    }
                                    templine = templine.Replace("[*methodName*]", cmdWithoutHyphen);
                                    writer.WriteLine(templine);
                                }
                                i++;
                            }
                        }
                        else
                        {
                            if (!cmdWithoutHyphen.ToLower().Contains("boundary"))
                            {
                                temp = temp.Replace("[*boundaryCases*]", string.Empty);
                            }
                            else
                            {
                                string testCases = "partial void Init" + testType + "Cases()\r\n\t\t{\r\n";
                                testCases += "\t\t\t" + testType + "Cases = new string[] {";
                                int index = 1;
                                foreach (string line in cmdLines)
                                {
                                    testCases += "\r\n\t\t\t/*" + index + "*/\"" + line + "\",";
                                    index++;
                                }
                                testCases = testCases.Remove(testCases.Length - 1) + "};\r\n\t\t}";
                                temp = temp.Replace("[*boundaryCases*]", testCases);
                            }
                            temp = temp.Replace("[*testClass*]", testClass);                           
                            writer.WriteLine(temp);
                        }
                    }
                }
                # endregion
                buildString.Add(string.Format("    <Compile Include=\"Test Methods\\{0}\\{1}\" />", cmd, dropFileName));
            }
            # endregion
        } 
    }
}
