using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HosteeBase;
using DomainFile;
using System.IO;
using System.Reflection;

namespace CombinationGenerator
{

    [Serializable]
    public partial class CombinationGeneratorClass : BaseHostee
    {
        public CombinationGeneratorClass()
        {
            this.PrepareEnv();
        }

        public override void Init()
        {
            Li.Add("InputFile");
            Li.Add("CombinationMethod");
        }

        private string inputFile;
        private string combinationMethod;

        public string CombinationMethod
        {
            get { return combinationMethod; }
            set { combinationMethod = value; }
        }


        public string InputFile
        {
            get { return inputFile; }
            set { inputFile = value; }
        }

        static string cmdName = null;
        static List<string> alterante = new List<string>();
        static List<string> mandatory = new List<string>();
        static List<string> optional = new List<string>();
        static int state = 0;

        public void Invoke()
        {
            
            string[] allLines = File.ReadAllLines(InputFile);

            int lineCount = 0;
            foreach (string tempLine in allLines)
            {
                lineCount++;
                if ((tempLine.IndexOf("alternate") < 0) && (tempLine.IndexOf("mandatory") < 0) && (tempLine.IndexOf("optional") < 0))
                {
                    state = 0;
                }
                if (tempLine.IndexOf("alternate") >= 0)
                {
                    state = 1;
                }
                if (tempLine.IndexOf("mandatory") >= 0)
                {
                    state = 2;
                }
                if (tempLine.IndexOf("optional") >= 0)
                {
                    state = 3;
                }
                if (lineCount == allLines.Count())
                {
                    state = 4;
                }
                StateMachine(tempLine);
                if (state == 0)
                {
                    cmdName = tempLine.Split(new char[] { ':' })[0];
                }
            }
        }

        public void StateMachine(string tempLine)
        {
            if (state == 0)
            {
                if ((mandatory.Count == 0) && (optional.Count == 0))
                {
                    return;
                }
                else
                {
                    GenerateCombinations();
                }
            }
            if (state == 1)
            {
                List<string> alts = GetSplitedString(tempLine);
                alterante.AddRange(alts);
            }
            if (state == 2)
            {
                List<string> mans = GetSplitedString(tempLine);
                mandatory.AddRange(mans);
            }
            if (state == 3)
            {
                List<string> opts = GetSplitedString(tempLine);
                optional.AddRange(opts);
                GenerateCombinations();
            }
            if (state == 4)
            {
                GenerateCombinations();
            }
        }

        private void GenerateCombinations()
        {
            string[] methods = CombinationMethod.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string temp in methods)
            {
                MethodInfo tempMethod = this.GetType().GetMethod(CombinationMethod);
                if (tempMethod != null)
                {
                    this.GetType().GetMethod(CombinationMethod).Invoke(this, new object[] { });
                }
            }
            CleanTempValue();
        }

        private void CleanTempValue()
        {
            alterante.Clear();
            mandatory.Clear();
            optional.Clear();
        }

        private List<string> GetSplitedString(string tempString)
        {
            List<string> sList = new List<string>();
            string[] s2 = tempString.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            if (s2.Count() == 2)
            {
                string s3 = s2[1];
                string[] s4 = s3.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string temp in s4)
                {
                    sList.Add(temp);
                }
            }
            return sList;
        }

    }
}
