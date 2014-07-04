using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;

namespace HostMan
{
    class MyException : System.Exception
    {
        public MyException(string Msg)
        {
            new Exception(Msg);
        }
    }


    class Hoster
    {
        static List<AppDomain> appList = new List<AppDomain>();
        static int AppDomainNumber = 0;


        public static AppDomain CreateAppDomain()
        {
            AppDomainNumber = AppDomainNumber + 1;
            AppDomain tempDomain = AppDomain.CreateDomain("AD " + AppDomainNumber, null, AppDomain.CurrentDomain.SetupInformation);
            appList.Add(tempDomain);
            return tempDomain;
        }


        public static string GetAllDomain()
        {
            string resultString = null;
            foreach (AppDomain temp in appList)
            {
                resultString = resultString + temp.FriendlyName + "\r\n";
            }
            return resultString;
        }

        public static void RemoveDomain(AppDomain removedDomain)
        {
            appList.Remove(removedDomain);
            AppDomain.Unload(removedDomain);
        }

        public static void RemoveAllDomain()
        {
            foreach (AppDomain temp in appList)
            {
                RemoveDomain(temp);
            }
        }

        public static void Invoke(string assemblyName, string typeName, string methodName = "Invoke", AppDomain contentDomain = null)
        {
            bool isDomainRelease = false;
            if (contentDomain == null)
            {
                contentDomain = CreateAppDomain();
                isDomainRelease = true;
            }

            object wraper = null;

            string path = Environment.CurrentDirectory;
            string assemblyPath = path + @"\" + assemblyName;
            Assembly result = Assembly.LoadFile(assemblyPath);
            string assemblyNameString = AssemblyName.GetAssemblyName(assemblyPath).ToString();


            if (!System.IO.File.Exists(assemblyPath))
            {
                throw new Exception();
            }

            //ap1.Load(assemblyNameString);
            // ap2.Load(assemblyNameString);
            wraper = contentDomain.CreateInstanceAndUnwrap(assemblyNameString, typeName);
            //var Type2 = wraper.GetType();
            //var method3 = Type2.GetMethods();
            List<MethodInfo> publicObj = wraper.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static).ToList<MethodInfo>();
            List<MethodInfo> privateObj = wraper.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Static).ToList<MethodInfo>();
            List<MethodInfo> allMethod = publicObj;
            allMethod.AddRange(privateObj);
            MethodInfo method = SearchMethod(allMethod, methodName);
            if (method == null)
            {
                throw new MyException("Method not found");
            }

            method.Invoke(wraper, new object[] { });

            if (isDomainRelease == true)
            {
                RemoveDomain(contentDomain);
            }
        }

        static MethodInfo SearchMethod(List<MethodInfo> allMethod, string methodNameString)
        {
            MethodInfo methodFound = null;
            foreach (MethodInfo temp in allMethod)
            {
                if (temp.Name == methodNameString)
                {
                    methodFound = temp;
                }
            }
            return methodFound;
        }
    }
}
