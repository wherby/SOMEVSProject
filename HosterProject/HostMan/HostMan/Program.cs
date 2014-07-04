using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;



namespace HostMan
{

    class Test
    {
        static void Main(string[] args)
        {


            AppDomain ap1 = Hoster.CreateAppDomain();
            AppDomain ap2 = Hoster.CreateAppDomain();
            AppDomain ap3 = Hoster.CreateAppDomain();
            Hoster.Invoke("Hostee.dll", "Hostee.Test", "Test2", ap1);
            Hoster.Invoke("Hostee.dll", "Hostee.Test", "Test2");
            Hoster.Invoke("Hostee.dll", "Hostee.Test", "Test2", ap2);
            Hoster.Invoke("Hostee.dll", "Hostee.Test", "Test2", ap1);

            string result = Hoster.GetAllDomain();
        }
    }
}
