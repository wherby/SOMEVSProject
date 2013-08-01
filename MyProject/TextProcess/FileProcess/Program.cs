using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;



namespace FileProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            string wlines = null;
            
            //using(FileStream ws=wt.OpenWrite())
            using (StreamReader rd = File.OpenText(@"C:\cmd.txt"))
            {
                string line = null;
                while ((line = rd.ReadLine()) != null) 
                {
                    string[] lines = line.Split(' ');
                    //if (lines[0].IndexOf("Credential") < 0)
                    //{
                        wlines = wlines + lines[0].Replace("-","") + " ";
                    //}

                }
                File.WriteAllLines(@"c:\cmdTFS2.txt", new string[] { wlines });
            }
            Console.WriteLine(wlines);

        }
    }
}
