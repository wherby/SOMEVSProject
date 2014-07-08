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

    public partial class CombinationGeneratorClass : BaseHostee
    {
        public static void Combination1()
        {
            List<string> ComStrings = new List<string>();
            List<string> OptionalComs = GenerateOptionalParas(optional);
            string temp = cmdName;
            foreach (string tempS in mandatory)
            {
                temp = temp + tempS;
            }
            foreach (string tempS in OptionalComs)
            {
                ComStrings.Add(temp + tempS);
            }
            DomainFileIO.AppendLine(cmdName + ".txt", ComStrings.ToArray());
        }

        private static List<string> GenerateOptionalParas(List<string> optionalparams)
        {
            List<string> optionalList = new List<string>();
            int num = 1 << optionalparams.Count;
            string temp = "";
            for (int i = 0; i < num; i++)
            {
                int tempNum = i;
                for (int j = 0; j < optionalparams.Count; j++)
                {
                    int k = tempNum % 2;
                    if (k == 1)
                    {
                        temp = temp + optionalparams[j];
                    }
                    tempNum = tempNum / 2;
                    if (tempNum == 0) break;
                }
                optionalList.Add(temp);
                temp = "";
            }
            if (optionalList.Count == 0)
            {
                optionalList.Add("");
            }
            return optionalList;
        }

        public static void Combination2()
        {
            List<string> ComStrings = new List<string>();

            string temp = cmdName;
            foreach (string tempS in mandatory)
            {
                temp = temp + tempS;
            }
            ComStrings.Add(temp);
            string optionalString = "";
            foreach (string tempS in optional)
            {
                optionalString = optionalString + tempS;
            }
            ComStrings.Add(temp + optionalString);
            DomainFileIO.AppendLine(cmdName + ".txt", ComStrings.ToArray());
        }


    }

}
