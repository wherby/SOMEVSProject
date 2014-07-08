using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using DomainFile;



namespace HostMan
{

    class Test
    {
        static void Main(string[] args)
        {


            AppDomain ap1 = Hoster.CreateAppDomain();
            DomainFileIO.WriteAllLines( "text.txt", new string[] { "test" });
            AppDomain ap2 = Hoster.CreateAppDomain();
            AppDomain ap3 = Hoster.CreateAppDomain();
            Hoster.Invoke("CombinationGenerator.dll", "CombinationGenerator.CombinationGeneratorClass", "Invoke");
            Hoster.Invoke("Hostee1.dll", "Hostee1.Test", "Test1", ap1);
            Hoster.Invoke("Hostee.dll", "Hostee.Test", "Test2");
            Hoster.Invoke("Hostee.dll", "Hostee.Test", "Test2", ap2);
            Hoster.Invoke("Hostee.dll", "Hostee.Test", "Test2", ap1);

            string result = Hoster.GetAllDomain();
        }
    }
}
