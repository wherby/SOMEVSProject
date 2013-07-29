using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using MagicRightBallServiceLib;



namespace MagicEightBallServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*******Console Based WCF*****");

            using (ServiceHost serviceHost = new ServiceHost(typeof(MagicRightBallService)))
            {
                serviceHost.Open();
                DisplayHostInfo(serviceHost);
                Console.WriteLine("The Service is Ready");
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }
        }

        static void DisplayHostInfo(ServiceHost host)
        {
            Console.WriteLine();
            Console.WriteLine("***Host Info***");
            Console.WriteLine("Name: {0}", host.Description.ConfigurationName);
            Console.WriteLine("Port: {0}", host.BaseAddresses[0].Port);
            Console.WriteLine("LocalPath: {0}", host.BaseAddresses[0].LocalPath);
            Console.WriteLine("URL: {0}", host.BaseAddresses[0].AbsoluteUri);
            Console.WriteLine("Schema: {0}", host.BaseAddresses[0].Scheme);
        }
    }
}
