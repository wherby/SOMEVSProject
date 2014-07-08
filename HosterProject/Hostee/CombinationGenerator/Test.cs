using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombinationGenerator
{
    class Test
    {
        static void Main(string[] args)
        {
            CombinationGeneratorClass cg = new CombinationGeneratorClass();
            cg.InputFile = @"C:\Temp\ESIPSToolKitParameter.txt";
            cg.CombinationMethod = "Combination2";
            cg.Invoke();
        } 
    }
}
