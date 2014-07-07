using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainFile
{
    class Test
    {
        static void Main(string[] args)
        {
            //AppDomain ap1 = Hoster.CreateAppDomain();
             DomainFileIO.WriteAllLines( "text.txt", new string[] { "test" });
        }
        
    }
}
