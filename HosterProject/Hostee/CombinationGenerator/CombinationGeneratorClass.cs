using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HosteeBase;
using DomainFile;
using System.IO;

namespace CombinationGenerator
{
    class Test
    {
        static void Main(string[] args)
        {
        }
    }

    [Serializable]
    public class CombinationGeneratorClass : BaseHostee
    {
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
        static List<string> Combinations = new List<string>();
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
                if (tempLine.IndexOf("alternate") > 0)
                {
                    state = 1;
                }
                if (tempLine.IndexOf("mandatory") > 0)
                {
                    state = 2;
                }
                if (tempLine.IndexOf("optional") > 0)
                {
                    state = 3;
                }
                if (lineCount == allLines.Count())
                {
                    state = 4;
                }
                StateMachine();
                if (state == 0)
                {
                    cmdName = tempLine.Split(new char[] { ':' })[0];
                }
            }
        }

        public void StateMachine()
        {
            if (state == 0)
            {
                if ((mandatory.Count == 0) && (optional.Count == 0))
                {
                    return;
                }
                else
                {
                    this.GetType().GetMethod(combinationMethod).Invoke(this, new object[] { });
                }
            }
            if (state == 1)
            {
 
            }
            if (state == 2)
            { 

            }
            if (state == 3)
            { 

            }
            if (state == 4)
            {
                this.GetType().GetMethod(combinationMethod).Invoke(this, new object[] { });
            }
        }

        public static void Combination1()
        {
            
        }

    }
}
