using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PsAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            PowershellMachine psMachine=new PowershellMachine();
            if (args.Count<string>() < 1 )
            {
                Console.WriteLine("There SHOULD have more than one parameter");
                Console.WriteLine("Usage: PsAgent.exe ScriptFile ConfigureFile");
                return;
            }
            int offset = 0;
            
            if (args[0] .IndexOf( ".exe")>=0)
            {
                //check the fist argument is self or not.
                offset = 1;
            }
            if (args.Count<string>() < 1+offset)
            {
                Console.WriteLine("There SHOULD have more than one parameter");
                Console.WriteLine("Usage: PsAgent.exe ScriptFile ConfigureFile");
            }
            else
            {
                try
                {
                    List<string> cmdList = new List<string>();
                    if (args.Count<string>() == 2+offset)
                    {
                        string[] tempStrings = File.ReadAllLines(args[1+offset]);
                        cmdList.AddRange(tempStrings);
                    }
                    string[] allPSString = File.ReadAllLines(args[0+offset]);
                    cmdList.AddRange(allPSString);
                    var Con = Console.Error;
                    var Con1 = Console.Out;
                    TextWriter NullWriter=TextWriter.Null;
                    Console.SetError(NullWriter);
                    Console.SetOut(NullWriter);
                    //Console.Error = NullWriter;
                   
                    string result = psMachine.RunScript(cmdList, new List<PSParam>() { }).OutStr.Trim();
                    Console.SetError(Con);
                    Console.SetOut(Con1);
                    Console.WriteLine(result);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
            }
        }
    }
}
