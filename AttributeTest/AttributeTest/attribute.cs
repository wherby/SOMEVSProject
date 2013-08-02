using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.ServiceModel;

namespace AttributeTest
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct,
                       AllowMultiple = true)  // multiuse attribute
]
    public class Lock : System.Attribute
    {
        string name;
        public double version;

        Random rd = new Random((int)DateTime.Now.Ticks);
        public Lock(string name)
        {
            this.name = name;
            version = 1.0;  // Default value
        }

        public string GetName()
        {
            return name;
        }




        public void GetResource()
        {
            WSHttpBinding bingding=new WSHttpBinding();

            Uri url=new Uri("http://test-tao1:8732/ResourceSync/");

            EndpointAddress address=new EndpointAddress(url);

            ResourceClient client = new ResourceClient(bingding, address);


            // Use the 'client' variable to call operations on the service.
            bool result =client.GetResource(name);
            Console.WriteLine("The resource {0} is {1}", name, result);
            client.ReleaseResource(name);
            // Always close the client.
            client.Close();
        }
    }


}
