using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace DomainFile
{
    public class DomainFileIO
    {

        public static void DeleteFile(string fileName, AppDomain domain = null)
        {
            string path = GetDomainFileName(fileName, domain);
            File.Delete(path);
        }

        public static string[] ReadFile(string fileName, AppDomain domain = null)
        {
            string path = GetDomainFileName(fileName, domain);
            string[] allLines = File.ReadAllLines(path);
            return allLines;
        }

        public static void WriteAllLines(string fileName, string[] allLines, AppDomain domain = null)
        {
            string dir = GetDomainDir(domain);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = GetDomainFileName(fileName, domain);
            File.WriteAllLines(path, allLines);
        }



        public static void AppendLine(string fileName, string[] allLines, AppDomain domain = null)
        {
            string dir = GetDomainDir(domain);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = GetDomainFileName(fileName, domain);
            File.AppendAllLines(path, allLines);
        }



        public static string GetDomainDir(AppDomain domain)
        {
            if (domain == null)
            {
                return Environment.CurrentDirectory + @"\defaultDomain";
            }
            string dir = Environment.CurrentDirectory + @"\" + domain.FriendlyName;
            return dir;
        }

        private static string GetDomainFileName(string fileName, AppDomain domain = null)
        {
            string exepath = Environment.CurrentDirectory;
            if (domain == null)
            {
                return exepath + @"\defaultDomain\" + fileName;
            }
            string domainpath = exepath + @"\" + domain.FriendlyName + @"\" + fileName;
            return domainpath;
        }
    }

}
