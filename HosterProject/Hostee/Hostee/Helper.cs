using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace HosteeBase
{
    /// <summary>
    /// The class for XML configuration file attributes.
    /// </summary>
    [Serializable]
    public class Properties
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public string Value;
    }




    public class Helper
    {
        
        /// <summary>
        /// Load
        ///     This method is to load content from a customized XML configuration file
        /// </summary>
        /// <returns>Dictionary of the properties</returns>
        public static Dictionary<string, string> Load(string assemblyName)
        {
            
            string path = assemblyName + ".xml";
            TextReader txt = new StreamReader(path);
            List<Properties> list = new List<Properties>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Properties>));
            list = (List<Properties>)serializer.Deserialize(txt);
            txt.Close();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (Properties property in list)
            {
                dic.Add(property.Name, property.Value);
            }

            return dic;
        }

    }
}
