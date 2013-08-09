using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using ResourceSync;
using System.ServiceModel;

namespace ResourcePoolService
{
    public partial class ResourceSyncService1 : ServiceBase
    {
        private ServiceHost myHost;

        public ResourceSyncService1()
        {
            InitializeComponent();
        }
          

 

        protected override void OnStart(string[] args)
        {
            if (myHost != null)
            {
                myHost.Close();
                myHost = null;
            }

            myHost = new ServiceHost(typeof(Service1));

            Uri address = new Uri("http://localhost:8732/ResourceSync/");
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.None);

            Type contract = typeof(IResource);

            //myHost.AddServiceEndpoint(contract, binding, address);
            
            myHost.Open();
        }



        protected override void OnStop()
        {
            if (myHost != null)
            {
                myHost.Close();
            }
        }
    }
}
