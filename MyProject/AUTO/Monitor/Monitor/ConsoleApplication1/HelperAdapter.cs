using System.Collections.Generic;
using System.Configuration;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Web;
using System.IO;



namespace EMC.ESI.PowerShell.Monitor
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


    /// <summary>
    /// The class for the common methods which will be used for test.
    /// </summary>
    public class HelperAdapter
    {
        private static TestLog log = TestLog.GetInstance();        
        static string alphaNum = "1234567890abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// GenerateEmptyPoolFilter
        ///     Generate an empty pool filter object
        /// </summary>
        /// <returns>Pool Filter object</returns>
        public static SortedList<string, string> GenerateEmptyPoolFilter()
        {
            SortedList<string, string> filters = new SortedList<string, string>();

            filters.Add("Thin", null);
            filters.Add("Capacity", null);
            filters.Add("PoolType", null);
            filters.Add("RaidType", null);

            return filters;
        }

        /// <summary>
        /// SelectPool
        ///     Select a random pool which meets the specified requirement
        /// </summary>
        /// <param name="poolstring">pool information retrieved from Get-EmcStoragePool</param>
        /// <param name="filters">Pool Filter object</param>
        /// <returns>Selected Pool ID</returns>
        public static string SelectPool(string poolstring, SortedList<string,string> filters=null)
        {
            string arrayPoolId=null;            
            List<SortedList<string, string>> pools = HelperAdapter.GenerateKeyValuePairsList(poolstring);
            List<SortedList<string, string>> selectedPools = new List<SortedList<string, string>>();            
            selectedPools.AddRange(pools);

            if ((filters != null) && (filters["Thin"] != null))
            {
                selectedPools.Clear();
                foreach (SortedList<string, string> pool in pools)
                {
                    if (pool["SupportsThinProvisioning"] == filters["Thin"])
                    {
                        selectedPools.Add(pool);
                    }
                }
                pools.Clear();
                pools.AddRange(selectedPools);
            }
            if ((filters != null) && (filters["Capacity"] != null))
            {
                selectedPools.Clear();
                foreach (SortedList<string, string> pool in pools)
                {
                    if (int.Parse(pool["AvailableCapacity"]) > int.Parse(filters["Capacity"]))
                    {
                        selectedPools.Add(pool);
                    }

                }
                pools.Clear();
                pools.AddRange(selectedPools);
            }
            if ((filters != null) && (filters["PoolType"] != null))
            {
                selectedPools.Clear();
                foreach (SortedList<string, string> pool in pools)
                {
                    if (pool["OtherProperties"].Contains(filters["PoolType"]))
                    {
                        selectedPools.Add(pool);
                    }
                }
                pools.Clear();
                pools.AddRange(selectedPools);
            }
            if ((filters != null) && (filters["RaidType"] != null))
            {
                selectedPools.Clear();
                foreach (SortedList<string, string> pool in pools)
                {
                    if (pool["RaidType"].Equals(filters["RaidType"],StringComparison.OrdinalIgnoreCase))
                    {
                        selectedPools.Add(pool);
                    }
                }
                pools.Clear();
                pools.AddRange(selectedPools);
            }

            if (selectedPools.Count > 0)
            {
                int counts = selectedPools.Count; 
                int randnumber = new Random().Next(counts - 1);                
                arrayPoolId = selectedPools[randnumber]["ArrayPoolId"];
            }
            return arrayPoolId;
        }


        /// <summary>
        /// The method is to extract key-value pair form result string.
        /// </summary>
        /// <param name="result">The result string.</param>
        /// <returns>The kry-value pairs for the result string.</returns>
        public static SortedList<string, string> GenerateKeyValuePairs(string result)
        {
            string[] resultLines = result.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, j = 0; i < resultLines.Length && j < resultLines.Length; i = j)
            {
                for (j = i + 1; j < resultLines.Length; j++)
                {
                    if (resultLines[j].StartsWith("   "))
                    {
                        resultLines[i] += resultLines[j].Trim();
                        resultLines[j] = string.Empty;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            SortedList<string, string> prams = new SortedList<string, string>();
            foreach (string resultLine in resultLines)
            {
                if (resultLine == string.Empty)
                {
                    continue;
                }

                string[] ParaAndValue = resultLine.Split(new char[] { ':' }, 2);

                if (ParaAndValue.Length == 2)
                {
                    prams.Add(ParaAndValue[0].Trim(), ParaAndValue[1].Trim());
                }
                else if(ParaAndValue.Length==1)
                {
                    prams.Add(ParaAndValue[0].Trim(), null);
                }
            }
            return prams;
        }

        /// <summary>
        /// The method is used to get value from configure file.
        /// </summary>
        /// <param name="propertyName">The name of property.</param>
        /// <returns>The value of property.</returns>
        public static string GetProperty(string propertyName)
        {
            string propertyValue=ConfigurationManager.AppSettings[propertyName];
            if (propertyValue == null)
            {
                Exception ex = new Exception(string.Format("The property {0} is not exist in configure file", propertyName));
                throw ex;
            }
            return propertyValue;
        }

        /// <summary>
        /// The method is used to return the property value of configuration file, default configuration file is PSParameterConfig.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="file">The configuration file of the property.</param>
        /// <returns>The value of property.</returns>
        public static string GetParameter(string name, string file="PSParameterConfig")
        {
            string path = GetProperty(file);
            Dictionary<string, string> dic = Load(path);
            return dic[name];
        }

        /// <summary>
        /// Load
        ///     This method is to load content from a customized XML configuration file
        /// </summary>
        /// <param name="path">the path of the configuration file</param>
        /// <returns>Dictionary of the properties</returns>
        public static Dictionary<string, string> Load(string path)
        {
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

        /// <summary>
        /// Load
        ///   Overload load method to read content from customized XML
        /// </summary>
        /// <param name="path">XML configuration file path</param>
        /// <param name="node">section node name</param>
        /// <returns>Dictionary of the properties</returns>
        public static Dictionary<string, string> Load(string path, string node)
        {
            XmlDocument xmldoc = new XmlDocument();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            xmldoc.Load(path);
            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;

            foreach (XmlElement element in topM)
            {
                if (element.Name == node)
                {
                    XmlNodeList nodelist = element.ChildNodes;

                    foreach (XmlElement el in nodelist)
                    {
                        dic.Add(el.Attributes["Name"].Value, el.Attributes["Value"].Value);
                    }

                    break;
                }
            }

            return dic;
        }
   


        /// <summary>
        /// GenerateKeyValuePairsList
        ///     Generate KeyValuePair into a list. 
        ///     This is useful if the result has multiple paragraphs and same keys
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<SortedList<string, string>> GenerateKeyValuePairsList(string result)
        {
            List<SortedList<string, string>> pairs = new List<SortedList<string, string>>();
            Regex regex = new Regex("^\\s*$");

            // Split with blank lines
            string[] results = result.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);            

            foreach (string res in results)
            {
                // Generate KeyValuePairs, skip the blank lines
                if (!regex.IsMatch(res))
                {
                    SortedList<string, string> pair = HelperAdapter.GenerateKeyValuePairs(res);
                    pairs.Add(pair);
                }
            }

            return pairs;
        }       
        

        /// <summary>
        /// GenerateLunName
        ///     Generate random lun name
        /// </summary>
        /// <param name="len">length of lunname postfix</param>
        /// <returns>lun name</returns>
        public static string GenerateLunName(int len = 6)
        {
            string lunName = "ps_testlun_";

            lunName += GenerateRandomString(len);

            return lunName;
        }

        /// <summary>
        /// GenerateRandomString
        ///     Generate random string
        /// </summary>
        /// <param name="len">length of the string</param>
        /// <returns>random string</returns>
        public static string GenerateRandomString(int len = 6)
        {
            string str = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                double randNum = rand.NextDouble();
                str += alphaNum.ToCharArray()[(int)Math.Floor(randNum * alphaNum.Length)];
            }

            return str;
        }
        /// <summary>
        /// GetBlobContent
        ///     Get the Content of a Blob File for a Specified System
        /// </summary>
        /// <param name="systemType"></param>
        /// <returns>Content of a Blob File</returns>
        public static string GetBlobContent(string systemType)
        {
            string blobString = null;
            string blobPath = null;
            string configPath = GetProperty("SystemConfig");
            Dictionary<string, string> dic = HelperAdapter.Load(configPath, systemType);

            if (dic.TryGetValue("BlobFile", out blobPath))
            {
                StreamReader file = new StreamReader(blobPath);
                blobString = file.ReadToEnd().Trim();
            }
            else
            {
                log.LogInfo(string.Format("{0} System's Blob File is Not Prepared!", systemType));
            }

            return blobString;
        }
    }   
}
