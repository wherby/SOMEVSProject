using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace HosteeBase
{
    [Serializable]
    public class BaseHostee
    {
        private List<string> li;

        public List<string> Li
        {
            get { return li; }
            set { li = value; }
        }

        public BaseHostee()
        {
            li = new List<string>();
        }

        public List<string> addPro(string pro)
        {
            Li.Add(pro);
            return Li;
        }

        public virtual void SetProperties(SortedList<string,string> envList)
        {
            foreach (string temp in Li)
            {
                List<PropertyInfo> publics=  this.GetType().GetProperties(BindingFlags.Public|BindingFlags.Instance).ToList();
                List<PropertyInfo> privates = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).ToList();
                List<PropertyInfo> allProperties = publics;
                allProperties.AddRange(privates);
                foreach (PropertyInfo tempInfo in allProperties)
                {
                    if (li.IndexOf(tempInfo.Name) >= 0)
                    {
                        if (envList.Keys.Contains(tempInfo.Name))
                        {
                            this.GetType().GetProperty(tempInfo.Name).SetValue(this, envList[tempInfo.Name], null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generate env closure from XML configure
        /// </summary>
        /// <returns>Env list</returns>
        public virtual SortedList<string, string> GetEnv(object obj)
        {
            var assemblyName = Assembly.GetAssembly(obj.GetType()).GetName().Name; //using this to get the derived class name and find out the xml file.
            Dictionary<string, string> dic = Helper.Load(assemblyName);
            SortedList<string, string> envList = new SortedList<string, string>();
            foreach (string key in dic.Keys)
            {
                envList.Add(key, dic[key]);
            }
            return envList;
        }

        public virtual void Init()
        {
 
        }

        public virtual void PrepareEnv()
        {
            SortedList<string, string> envList = this.GetEnv(this);

            this.Init();
            this.SetProperties(envList);
        }
    }
}
