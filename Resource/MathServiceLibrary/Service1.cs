using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ResourceSync
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IResource
    {
        static SortedList<string, bool> resourcePool = new SortedList<string, bool>();
        private object threadLock = new object();

        public bool GetResource(string resourceID)
        {
            bool resourceAvailability=false;
            lock (threadLock)
            {
                if (resourcePool.Keys.Contains(resourceID))
                {
                    if (resourcePool[resourceID] == true)
                    {
                        resourcePool[resourceID] = false;
                        resourceAvailability = true;
                    }
                    else
                    {
                        resourceAvailability = false;
                    }
                }
                else
                {
                    resourcePool.Add(resourceID, false);
                    resourceAvailability = true;
                }
            }
            return resourceAvailability;
        }

        public void ReleaseResource(string resourceID)
        {
            lock (threadLock)
            {
                if (resourcePool.Keys.Contains(resourceID))
                {
                    resourcePool[resourceID] = true;
                }
            }
        }

        public void ResetAllResource()
        {
            lock (threadLock)
            {
                resourcePool.Clear();
            }
        }

        public int Add(int x, int y)
        {
            System.Threading.Thread.Sleep(1000);
            return x + y;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
