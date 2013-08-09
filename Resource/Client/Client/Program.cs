using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Client
{
    class Program
    {
        static string urlString="http://test-tao1:8732/ResourceSync/";
        static void Main(string[] args)
        {
            if ((args.Length == 2)||(args.Length==1))
            {
                string ResourceName = null;
                if (args.Length == 2)
                {
                    ResourceName = args[1];
                }
                switch (args[0])
                {
                    case "Get":
                            bool result = GetResource(ResourceName);
                            Console.WriteLine("result is " + result);
                            break;
                        
                    case "Release":   
                            ReleaseResource(ResourceName);
                            Console.WriteLine(ResourceName + " is released");
                            break;

                    case "Reset":
                            ResetResource();
                            Console.WriteLine("All resource reseted");
                            break;

                    default:
                            Console.WriteLine("*******Usage*****");
                            Console.WriteLine(@"The first arg SHOULD be Get\Release\Reset, for example client.exe Get abc");
                        break;
                }

               // Console.ReadLine();
            }
           // Console.ReadLine();
        }

        static public bool GetResource(string ResourceName)
        {

            WSHttpBinding bingding = new WSHttpBinding();


            Uri url = new Uri(urlString);

            EndpointAddress address = new EndpointAddress(url);

            ResourceClient client = new ResourceClient(bingding, address);
            //// client.ClientCredentials.UserName.UserName=@"pie\administrator";
            ////    client.ClientCredentials.UserName.Password = "Password!";
            //ResourceClient rClient = new ResourceClient();
            //bool result2 = rClient.GetResource(ResourceName);

            // Use the 'client' variable to call operations on the service.
            bool result = client.GetResource(ResourceName);
            Console.WriteLine("The resource {0} is {1}", ResourceName, result);
            //  client.ReleaseResource(name);
            // Always close the client.
            client.Close();
            return result;
        }


        static public void ReleaseResource(string ResourceName)
        {
            WSHttpBinding bingding = new WSHttpBinding();

            Uri url = new Uri(urlString);

            EndpointAddress address = new EndpointAddress(url);

            ResourceClient client = new ResourceClient(bingding, address);


            // Use the 'client' variable to call operations on the service.
            client.ReleaseResource(ResourceName);
            // Always close the client.
            client.Close();
        }

        static public void ResetResource()
        {
            WSHttpBinding bingding = new WSHttpBinding();

            Uri url = new Uri(urlString);

            EndpointAddress address = new EndpointAddress(url);

            ResourceClient client = new ResourceClient(bingding, address);


            // Use the 'client' variable to call operations on the service.
            client.ResetAllResource();
            // Always close the client.
            client.Close();
        }
    }


}
