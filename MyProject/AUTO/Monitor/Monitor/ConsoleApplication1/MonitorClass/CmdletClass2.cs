using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace EMC.ESI.PowerShell.Monitor
{
    partial class CmdletClass
    {
        public void AutoGenerate()
        {
            List<KeyValuePair<string, string>> mandatory = element.Mandatory;
            string cmdString = cmdletName.Replace("-", "");
            string projectFolder = HelperAdapter.GetProperty("ProjectFolder");
            string testClassFile = projectFolder + @"\Test Classes\" + cmdletName + "TestClass.cs";
            string cmdClassFile = projectFolder + @"\EMC PowerShell\ClassFor" + cmdString + ".cs";
            string tempFolder = HelperAdapter.GetProperty("TemplateFolder");
            string[] generateFiles = {testClassFile, cmdClassFile};
            string[] tempFiles = {tempFolder + @"\CmdTestClass.cs", @"..\..\Template\ClassForCmd.cs"};
            string parameters = string.Empty;
            string stringparameters = string.Empty;

            string[] temp = new string[4];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = GenerateTemp(File.ReadAllText(tempFolder + @"\Temp" + i + ".txt"), mandatory);
            }

            for (int i = 0; i < mandatory.Count; i++)
            {
                if (mandatory[i].Key.ToLower().Contains("confirm") || mandatory[i].Key.Trim() == string.Empty || mandatory[i].Key == null)
                {
                    continue;
                }
                string param = mandatory[i].Key;
                if (mandatory[i].Key.ToLower() == "readonly")
                {
                    param = "ReadOnlyParam";
                }
                parameters += param.ToLower() + ", ";
                stringparameters += "string " +param.ToLower() + " = null, ";
                
                //if (i != mandatory.Count - 1)
                //{
                //    parameters += ", ";
                //    stringparameters += ", ";
                //}
            }

            for(int i = 0; i < generateFiles.Length; i++)
            {
                string text = File.ReadAllText(tempFiles[i]);

                text = text.Replace("[CMD]", cmdletName);
                text = text.Replace("[CMDString]", cmdString);
                text = text.Replace("[cmdstring]", cmdString.ToLower());
                text = text.Replace("[cmd]", cmdletName.ToLower());
                text = text.Replace("[parameters]", parameters);
                text = text.Replace("[string parameters]", stringparameters);
                for (int j = 0; j < 4; j++)
                {
                    text = text.Replace("[TEMP" + j + "]", temp[j]);
                }

                if (File.Exists(generateFiles[i]))
                {
                    Regex reg = new Regex("#region AutoGenerate([\\s\\S]*?)#endregion");
                    Match m = reg.Match(text);
                    string newContent = m.Groups[1].Value;
                    string oldText = File.ReadAllText(generateFiles[i]);
                    if(reg.IsMatch(oldText))
                    {
                        File.SetAttributes(generateFiles[i], ~FileAttributes.ReadOnly);
                        File.Delete(generateFiles[i]);

                        text = Regex.Replace(oldText, "(#region AutoGenerate)([\\s\\S]*?)(#endregion)", "$1" + newContent + "$3");
                        File.WriteAllText(generateFiles[i], text);
                    }
                }
                else
                {
                     File.WriteAllText(generateFiles[i], text);
                     GenerateProjectFile(projectFolder + @"\PowerShellAutomation.csproj", generateFiles[i].Replace(projectFolder + @"\", "") );
                }
            }
            
        }

        private string GenerateTemp(string text, List<KeyValuePair<string, string>> args)
        {
            string newText = string.Empty;
            string tempText = string.Empty;
            bool confirm = false;

            foreach (KeyValuePair<string, string> arg in args)
            {
                if (arg.Key.ToLower().Contains("confirm") || arg.Key.Trim() == string.Empty)
                {
                    confirm = true;
                    continue;
                }
                else
                {
                    string param = arg.Key;
                    if (arg.Key.ToLower() == "readonly")
                    {
                        param = "ReadOnlyParam";
                    }

                    tempText = text.Replace("[parameter]", param.ToLower());
                    tempText = tempText.Replace("[Parameter]", param);
                    string appendParameter = string.Empty;
                    if (arg.Value != string.Empty)
                    {
                        appendParameter = "\" -" + arg.Key + " {0}\", " + param.ToLower() + "String";
                    }
                    else
                    {
                        appendParameter = "\" -" + arg.Key + "\"";
                    }
                    newText += tempText.Replace("[AppendParameter]", appendParameter) + "\r\n";
                }
            }
            if (confirm && text.Contains("[AppendParameter]"))
            {
                newText += "		    sb.AppendFormat(\" -Confirm:$false\");" + "\r\n";
            }

            return newText;
        }

        private void GenerateProjectFile(string projectFile, string addedFile)
        {
            
            string oldText = File.ReadAllText(projectFile);
            File.SetAttributes(projectFile, ~FileAttributes.ReadOnly);
            
            if (!oldText.Contains(addedFile))
            {
                File.Delete(projectFile);
                string text = Regex.Replace(oldText, "(<ItemGroup>\r\n)(    <Compile Include=)", "$1" + "    <Compile Include=\"" + addedFile + "\" />\r\n" + "$2");
                File.WriteAllText(projectFile, text);
            }
        }
    }
}
