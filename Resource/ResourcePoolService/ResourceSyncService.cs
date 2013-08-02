using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;


namespace ResourcePoolService
{
    static class ResourceSyncService
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new ResourceSyncService1() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
