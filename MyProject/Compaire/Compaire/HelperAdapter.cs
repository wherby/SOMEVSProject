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
using System.Net;

namespace PowerShellTestTools
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
    /// StorageAccessControl struct
    /// </summary>
    public struct StorageAccessControlStruct
    {
        public string StorageGlobalID;
        public List<string> Pools;
    }
    
    /// <summary>
    /// HyperVisorType enumeric type
    /// </summary>
    public enum HyperVisorType
    {
        HyperV,
        XenServer,
        VMWare
    }

    /// <summary>
    /// Capacity Unit
    /// </summary>
    public enum CapacityUnit
    {
        Byte,
        KB,
        MB,
        GB,
        TB
    }

    /// <summary>
    /// Config Type
    /// </summary>
    public enum ConfigType
    {
        StorageSystem,
        ESIService,
        PSParameterForService
    }

    /// <summary>
    /// The class for the common methods which will be used for test.
    /// </summary>
    public class HelperAdapter
    {              
        static string alphaNum = "1234567890abcdefghijklmnopqrstuvwxyz";

        # region Available Value Declaration
        public static string[] VhdType = { "Dynamic", "Fixed" };
        public static string[] VmdkType = { "LazyZeroedThick", "Thin", "EagerZeroedThick" };
        public static string[] PartitionStyle = { "Mbr", "Gpt"};
        public static string[] FileSystemType = { "Ntfs", "Fat32" };
        public static string[] Fat32AllocationUnitSize = { "512", "1024", "2048", "4096", "8192", "16384", "32768" };
        public static string[] NtfsAllocationUnitSize = { "512", "1024", "2048", "4096", "8192", "16384", "32768", "65536" };
        public static string[] ThinOrThick = { "true", "false" };
        # endregion 

        # region Pool select
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
                    string[] availableCapacity = pool["AvailableCapacity"].Trim().Split(new char[]{' '});
                    if ((availableCapacity[1] == "GB") || (availableCapacity[1] == "TB"))
                    {
                        if (float.Parse(availableCapacity[0]) > int.Parse(filters["Capacity"]) || (availableCapacity[1] == "TB"))
                        {
                            selectedPools.Add(pool);
                        }
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

            arrayPoolId = "\"" + arrayPoolId + "\"";
            return arrayPoolId;
        }

        # endregion 

        #region Get Test environment setting
        /// <summary>
        /// The method is return the environment has cluster or not.
        /// </summary>
        /// <returns>the environment has cluster or not.</returns>
        public static bool IsClusterSet()
        {
            string clusterSetValue = HelperAdapter.GetProperty("IsClusterSet");
            return clusterSetValue.ToLower().Equals("true");
        }

        /// <summary>
        /// The method is return the environment has hyper-v or not.
        /// </summary>
        /// <returns>the environment has hyper-v or not.</returns>
        public static bool IsHyperVSet()
        {
            string hyperVSetValue = HelperAdapter.GetProperty("IsHyperVSet");
            return hyperVSetValue.ToLower().Equals("true");
        }

        /// <summary>
        /// The method is return the environment has VMWare or not.
        /// </summary>
        /// <returns>the environment has VMWare or not.</returns>
        public static bool IsVMwareSet()
        {
            string vmwareSetValue = HelperAdapter.GetProperty("IsVMwareSet");
            return vmwareSetValue.ToLower().Equals("true");
        }

        /// <summary>
        /// The method is return the environment has Xen or not.
        /// </summary>
        /// <returns>the environment has Xen or not.</returns>
        public static bool IsXenSet()
        {
            string xenSetValue = HelperAdapter.GetProperty("IsXenSet");
            return xenSetValue.ToLower().Equals("true");
        }

        /// <summary>
        /// The method is to return a flag which indicates whether verification should be done on storage side.
        /// </summary>
        /// <returns>true: verify result on storage side. false: don't verify result on storage side</returns>
        public static bool IsVerifyStorageSide()
        {
            string value = HelperAdapter.GetProperty("VerifyStorageSide");
            return value.ToLower().Equals("true");
        }

        #endregion

        # region Parse result string to Key/Value pair
        /// <summary>
        /// The method is to extract key-value pair form result string.
        /// </summary>
        /// <param name="result">The result string.</param>
        /// <returns>The kry-value pairs for the result string.</returns>
        public static SortedList<string, string> GenerateKeyValuePairs(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
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
        /// GenerateKeyValuePairsList
        ///     Generate KeyValuePair into a list. 
        ///     This is useful if the result has multiple paragraphs and same keys
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<SortedList<string, string>> GenerateKeyValuePairsList(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            List<SortedList<string, string>> pairs = new List<SortedList<string, string>>();
            Regex regex = new Regex("^\\s*$");

            // Split with blank lines
            string[] results = result.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string res in results)
            {
                try
                {
                    // Generate KeyValuePairs, skip the blank lines
                    if (!regex.IsMatch(res))
                    {
                        SortedList<string, string> pair = HelperAdapter.GenerateKeyValuePairs(res);
                        pairs.Add(pair);
                    }
                }
                catch(Exception e)
                { 
                }
            }

            return pairs;
        }
        # endregion 

        # region GetProperty, GetParameter
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
        public static string GetParameter(string name, ConfigType type = ConfigType.PSParameterForService)
        {
            Dictionary<string, string> dic = Load(type);
            return dic[name];
        }
        # endregion 

        # region Load content from xml file
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
            Dictionary<string, string> dic = new Dictionary<string, string>();

            switch (node.ToLower())
            {
                case "hyperv":
                dic = GetHypervisorProperties(HyperVisorType.HyperV);
                break;

                case "xenserver":
                dic = GetHypervisorProperties(HyperVisorType.XenServer);
                break;

                case "vmware":
                dic = GetHypervisorProperties(HyperVisorType.VMWare);
                break;

                case "esxhost":
                dic = GetHypervisorHosts(HyperVisorType.VMWare)[0];
                break;

                default:
                XmlNodeList topM = GetTopXMLNodeList(path);
                XmlNodeList nodeList = GetChildXMLNodeList(topM, node);
                dic = GetNodeListProperties(nodeList);
                break;
            }
            return dic;
        }


        public static Dictionary<string, string> Load(ConfigType type, string node = null)
        {
            if (node == null)
            {
                return Load(type.ToString() + ".xml");
            }

            return Load(type.ToString() + ".xml", node);
        }
        /// <summary>
        /// GetNodeListProperties
        ///     Get the properties of a node list, and add the properties to a dictionary
        /// </summary>
        /// <param name="nodeList">xml node list</param>
        /// <returns>properties of the node list</returns>
        private static Dictionary<string, string> GetNodeListProperties(XmlNodeList nodeList)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (nodeList == null)
            {
                return dic;
            }

            foreach (XmlElement element in nodeList)
            {
                if (element.Name == "Properties")
                {
                    dic.Add(element.Attributes["Name"].Value, element.Attributes["Value"].Value);
                }
            }
            return dic;
        }

        /// <summary>
        /// GetTopXMLNodeList
        ///     Get the top node list of a XML file
        /// </summary>
        /// <param name="path">XML file path</param>
        /// <returns>the top node list</returns>
        private static XmlNodeList GetTopXMLNodeList(string path)
        {
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(path);

            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;

            return topM;
        }

        /// <summary>
        /// GetChildXMLNodeList
        ///     Get child node list of the parent node list
        /// </summary>
        /// <param name="nodeList">parent node list</param>
        /// <param name="nodeName">node name in parent list</param>
        /// <returns>the child node list</returns>
        private static XmlNodeList GetChildXMLNodeList(XmlNodeList nodeList, string nodeName)
        {
            XmlNodeList childNodeList = null;

            foreach (XmlElement element in nodeList)
            {
                if (element.Name == nodeName)
                {
                    childNodeList = element.ChildNodes;
                    break;
                }
            }

            return childNodeList;
        }

        /// <summary>
        /// GetHypervisorProperties
        ///     Get hyperviosr properties
        /// </summary>
        /// <param name="type">hypervisor type</param>
        /// <returns>the properties of a hypervisor</returns>
        public static Dictionary<string, string> GetHypervisorProperties(HyperVisorType type)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string path = null;

            switch(type)
            {
                case HyperVisorType.HyperV:
                    path = HelperAdapter.GetProperty("HyperVConfig");
                    break;
                case HyperVisorType.XenServer:
                    path = HelperAdapter.GetProperty("XenServerConfig");
                    break;
                case HyperVisorType.VMWare:
                    path = HelperAdapter.GetProperty("VMwareConfig");
                    break;
            }

            dic = Load(path, "Hypervisor");
            return dic;
        }

        /// <summary>
        /// GetHypervisorHosts
        ///     Get a list of hosts properties on a hypervisor
        /// </summary>
        /// <param name="type">hypervisor type</param>
        /// <returns>a list of hosts properties</returns>
        public static List<Dictionary<string, string>> GetHypervisorHosts(HyperVisorType type)
        {
            List<Dictionary<string, string>> hostsList = new List<Dictionary<string, string>>();

            string path = null;

            switch (type)
            {
                case HyperVisorType.HyperV:
                    path = HelperAdapter.GetProperty("HyperVConfig");
                    break;
                case HyperVisorType.XenServer:
                    path = HelperAdapter.GetProperty("XenServerConfig");
                    break;
                case HyperVisorType.VMWare:
                    path = HelperAdapter.GetProperty("VMwareConfig");
                    break;
            }

            XmlNodeList topM = GetTopXMLNodeList(path);

            XmlNodeList nodeList = GetChildXMLNodeList(topM, "Hypervisor");

            for (int i = 0; ; i++)
            {
                XmlNodeList childList = GetChildXMLNodeList(nodeList, "Host" + i);
                if (childList != null)
                {
                    hostsList.Add(GetNodeListProperties(childList));
                }
                else
                {
                    break;
                }
            }

            return hostsList;
        }

        /// <summary>
        /// GetHostVMs
        ///     Get a list of VMs properties on a 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="hostIndex"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> GetHostVMs(HyperVisorType type, int hostIndex = 0)
        {
            List<Dictionary<string, string>> vmsList = new List<Dictionary<string, string>>();

            string path = null;
            XmlNodeList topM = null;
            XmlNodeList nodeList = null;
            XmlNodeList hostList = null;

            switch (type)
            {
                case HyperVisorType.HyperV:
                    path = HelperAdapter.GetProperty("HyperVConfig");
                    topM = GetTopXMLNodeList(path);
                    hostList = GetChildXMLNodeList(topM, "Hypervisor");
                    break;
                case HyperVisorType.XenServer:
                    path = HelperAdapter.GetProperty("XenServerConfig");
                    topM = GetTopXMLNodeList(path);
                    nodeList = GetChildXMLNodeList(topM, "Hypervisor");
                    hostList = GetChildXMLNodeList(nodeList, "Host" + hostIndex);
                    break;
                case HyperVisorType.VMWare:
                    path = HelperAdapter.GetProperty("VMwareConfig");
                    topM = GetTopXMLNodeList(path);
                    nodeList = GetChildXMLNodeList(topM, "Hypervisor");
                    hostList = GetChildXMLNodeList(nodeList, "Host" + hostIndex);
                    break;
            }

            for (int i = 0; ; i++)
            {
                XmlNodeList vmList = GetChildXMLNodeList(hostList, "VM" + i);
                if (vmList != null)
                {
                    vmsList.Add(GetNodeListProperties(vmList));
                }
                else
                {
                    break;
                }
            }

            return vmsList;
        }
        # endregion 

        # region Comparison 

        



        # region Generate random string, name
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
        /// Generate random name.
        /// </summary>
        /// <param name="name">Name prefix.</param>
        /// <param name="len">The random numer.</param>
        /// <returns>The random nameString.</returns>
        public static string GenerateName(string name,int len=6)
        {
            string nameFull = name + "_" + GenerateRandomString(len);
            return nameFull;
        }

        /// <summary>
        /// GenerateRandomValue
        ///     Generate random value.
        /// </summary>
        /// <param name="values">string array of values</param>
        /// <returns>random value string</returns>
        public static string GenerateRandomValue(string[] values)
        {
            string value;
            Random rand = new Random();
            int i = rand.Next(values.Length);
            value = values[i];

            return value;
        }

        /// <summary>
        /// GenerateRandomValue
        ///        Generate random value.     
        /// </summary>
        /// <param name="values">string which contains values connected woth ','.</param>
        /// <returns>random value string</returns>
        public static string GenerateRandomValue(string values)
        {
            string[] valuesArray=values.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return GenerateRandomValue(valuesArray);
        }

        /// <summary>
        /// GenerateRandomRefreshInterval
        ///     Generate random refresh interval, the value is between 1 and 2147483647
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomRefreshInterval()
        {
            Random rand = new Random();
            
            int i = rand.Next(0, System.Int32.MaxValue) + 1;

            return i;
        }

        # endregion 

        # region GetBlobContent


        /// <summary>
        /// GenerateNetSharepath
        /// </summary>
        /// <param name="volumeResult">volume output string</param>
        /// <param name="ip">ip address</param>
        /// <param name="file">file name</param>
        /// <returns>net share path</returns>
        public static string GenerateNetSharePath(string volumeResult, string ip, string file)
        {

            string driveLetter = GenerateKeyValuePairs(volumeResult)["MountPath"];
            driveLetter = driveLetter.Replace(":\\", "");
            string netSharePath = "\\\\" + ip + "\\" + driveLetter + "$\\" + file;

            return netSharePath;
        }

        public static string GenerateCreationParameters(string systemType, string prefix = null, bool isCluster = false)
        {
            if (prefix == null)
            {
                prefix = GetParameter("CreationParameters");
            }
            Dictionary<string, string> dic = null, paramdic = new Dictionary<string,string>();
            string parameters = null;

            switch (systemType)
            {
                case "Citrix XenServer":
                    dic = HelperAdapter.GetHypervisorProperties(HyperVisorType.XenServer);
                    paramdic.Add("Username", dic["UserName"]);
                    paramdic.Add("Password", dic["Password"]);
                    paramdic.Add("Server", dic["IPAddress"]);
                    break;
                case "vSphere/vCenter Server":
                    dic = HelperAdapter.GetHypervisorProperties(HyperVisorType.VMWare);
                    paramdic.Add("Username", dic["UserName"]);
                    paramdic.Add("Password", dic["Password"]);
                    paramdic.Add("Server", dic["IPAddress"]);
                    break;
                case "VMware":
                case "ESX":
                    dic = HelperAdapter.GetHypervisorHosts(HyperVisorType.VMWare)[0];
                    paramdic.Add("Username", dic["UserName"]);
                    paramdic.Add("Password", dic["Password"]);
                    paramdic.Add("Server", dic["IPAddress"]);
                    break;
                case "VNXe":
                    dic = HelperAdapter.Load(ConfigType.StorageSystem, systemType);
                    paramdic.Add("UserFriendlyName", "vnxe");
                    paramdic.Add("Username", dic["Username"]);
                    paramdic.Add("Password", dic["Password"]);
                    paramdic.Add("SpaIpAddress", dic["SpaIpAddress"]);
                    break;
                case "VNX-CIFS":
                    dic = HelperAdapter.Load(ConfigType.StorageSystem, systemType);
                    paramdic.Add("UserFriendlyName", systemType.ToLower());
                    paramdic.Add("Username", dic["Username"]);
                    paramdic.Add("Password", dic["Password"]);
                    paramdic.Add("ControlStationIPAddress", dic["ControlStationIPAddress"]);
                    paramdic.Add("ControlStationPort", "443");
                    paramdic.Add("BypassServerCertificateValidation", "true");
                    break;
                case "CLARiiON-CX4":
                case "VNX-Block":
                    dic = HelperAdapter.Load(ConfigType.StorageSystem, "VNX-Block");
                    paramdic.Add("UserFriendlyName", systemType.ToLower());
                    paramdic.Add("Username", dic["Username"]);
                    paramdic.Add("Password", dic["Password"]);
                    paramdic.Add("SpaIpAddress", dic["SpaIpAddress"]);
                    paramdic.Add("SpbIpAddress", dic["SpbIpAddress"]);
                    paramdic.Add("Port", "443");
                    break;
                case "VNX":
                    dic = HelperAdapter.Load(ConfigType.StorageSystem, systemType);
                    paramdic.Add("UserFriendlyName", systemType.ToLower());
                    paramdic.Add("Block-Username", dic["BlockUsername"]);
                    paramdic.Add("Block-Password", dic["BlockPassword"]);
                    paramdic.Add("SpaIpAddress", dic["SpaIpAddress"]);
                    paramdic.Add("SpbIpAddress", dic["SpbIpAddress"]);
                    paramdic.Add("Block-Port", "443");
                    paramdic.Add("Cifs-Username", dic["CifsUsername"]);
                    paramdic.Add("Cifs-Password", dic["CifsPassword"]);
                    paramdic.Add("ControlStationIPAddress", dic["ControlStationIPAddress"]);
                    paramdic.Add("ControlStationPort", "443");
                    paramdic.Add("BypassServerCertificateValidation", "true");
                    break;
                case "VMAX":
                    dic = HelperAdapter.Load(ConfigType.StorageSystem, systemType);
                    paramdic.Add("SerialNumber", dic["SerialNumber"]);
                    paramdic.Add("Host", dic["Host"]);
                    paramdic.Add("Username", dic["Username"]);
                    paramdic.Add("Password", dic["Password"]);
                    paramdic.Add("IgnoreServerCACheck", "true");
                    paramdic.Add("IgnoreServerCNCheck", "true");
                    break;
                //case "Windows Server 2008":
                //case "Windows Server 2008 R2":
                //    if (isCluster)
                //    {
                //        dic = HelperAdapter.Load(ConfigType.System, "Cluster");
                //        paramdic.Add("IsCluster", "true");
                //        paramdic.Add("UserFriendlyName", "cluster");
                //        paramdic.Add("UseCurrentDomainCredential", "false");
                //    }
                //    else
                //    {
                //        dic = HelperAdapter.Load(ConfigType.System, "Host");
                //        paramdic.Add("IsCluster", "false");
                //        paramdic.Add("UserFriendlyName", "host");
                //        if (dic["HostName"].Contains(Dns.GetHostName()))
                //        {
                //            paramdic.Add("UseCurrentDomainCredential", "true");
                //        }
                //        else
                //        {
                //            paramdic.Add("UseCurrentDomainCredential", "false");
                //        }
                //    }
                //    paramdic.Add("Username", dic["Username"]);
                //    paramdic.Add("Password", dic["Password"]);
                //    paramdic.Add("HostSystemName", dic["HostName"]);
                //    break;
                default:
                    throw new Exception("Unknown storage system type");
            }

            parameters = prefix + "=@{";

            foreach (string key in paramdic.Keys)
            {
                parameters += "\"" + key + "\"" + "=\"" + paramdic[key] + "\";";
            }
            parameters = parameters.Remove(parameters.Length - 1) + "}";

            return parameters;
        }
        # endregion 

        # region Find matched item from list
        /// <summary>
        /// FindElementFromList
        ///     Find an element from list
        /// </summary>
        /// <param name="elementValue">Element Value</param>
        /// <param name="key">Key string</param>
        /// <param name="list">Element List</param>
        /// <returns></returns>
        public static SortedList<string, string> FindElementFromList(string elementValue, string key, List<SortedList<string, string>> list)
        {
            foreach (SortedList<string, string> element in list)
            {
                if (element[key].ToLower().Equals(elementValue.ToLower()))
                {
                    return element;
                }
            }
            return null;
        }
        # endregion 

        # region Access control
 

        /// <summary>
        /// ParseAccessControlOutput
        ///     Parse Storage Access Control output to a list of StorageAccessControlStruct
        /// </summary>
        /// <param name="result">result string of New-EmcStorageAccessControl/Remove-EmcStorageAccessControl/Add-EmcStorageAccessControl</param>
        /// <returns></returns>
        public static List<StorageAccessControlStruct> ParseAccessControlOutput(string result)
        {
            List<string> lines = new List<string>();
            List<StorageAccessControlStruct> accessControlList = new List<StorageAccessControlStruct>();
            StorageAccessControlStruct accessControl;
            accessControl.StorageGlobalID = "";
            accessControl.Pools = new List<string>();
            bool first = true;

            result = result.Replace("Storage Access Control", "");
            string[] ret = result.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in ret)
            {
                if (line.Contains("StorageSystemGlobalId"))
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        accessControlList.Add(accessControl);
                    }
                    char c = ':';
                    accessControl.StorageGlobalID = line.Split(c)[1].Trim();
                    accessControl.Pools = new List<string>();

                }
                else
                {
                    accessControl.Pools.Add(line);
                }
            }

            accessControlList.Add(accessControl);

            return accessControlList;
        }

        /// <summary>
        /// GetPoolAccessControlValue
        ///     Get access control of specified pool
        /// </summary>
        /// <param name="result">Access Control output result</param>
        /// <param name="storageGlobalID">storage global id</param>
        /// <param name="poolName">pool name</param>
        /// <param name="poolType">pool type</param>
        /// <returns>pool control value</returns>
        public static string GetPoolAccessControlValue(string result, string storageGlobalID, string poolName, string poolType)
        {
            List<StorageAccessControlStruct> storageAccessControlList = HelperAdapter.ParseAccessControlOutput(result);
            foreach (StorageAccessControlStruct accessControl in storageAccessControlList)
            {
                if (accessControl.StorageGlobalID.Equals(storageGlobalID))
                {
                    foreach (string pool in accessControl.Pools)
                    {
                        string[] poolProperties = pool.Split(new string[] { "\t" }, System.StringSplitOptions.RemoveEmptyEntries);
                        string poolNameValue = poolProperties[0].Trim();
                        string poolTypeValue = poolProperties[1].Trim();
                        string poolControlValue = poolProperties[2].Trim();
                        if (poolTypeValue == "Cifs")
                        {
                            poolTypeValue = "File";
                        }
                        if (poolNameValue.Equals(poolName) && poolTypeValue.Equals(poolType))
                        {
                            return poolControlValue;
                        }
                    }
                }                
            }
            return null;
        }
        # endregion
        # endregion
        # region Parse Capacity
        /// <summary>
        /// ParseCapacity
        ///     Parse capacity in Byte or Block to size in suitable unit
        /// </summary>
        /// <param name="count">the capacity to parse</param>
        /// <param name="isBlock">Specify the count is in block or Byte</param>
        /// <returns>X.XXX unit</returns>
        public static string ParseCapacity(string count, bool isBlock = true)
        {
            CapacityUnit unit = CapacityUnit.Byte;
            double parsedCount = double.Parse(count);
            double capacityInBytes = 0;
            if (isBlock)
            {
                capacityInBytes = parsedCount * 512;
            }
            else
            {
                capacityInBytes = parsedCount;
            }
            double capacity = capacityInBytes;
            while (true)
            {
                if (capacity / 1024 >= 1 && unit != CapacityUnit.TB)
                {
                    capacity /= 1024;
                    unit += 1;
                }
                else
                {
                    break;
                }
            }
            if (capacity == 0)
            {
                return capacity.ToString();
            }
            else
            {
                return (capacity.ToString("0.000") + " " + unit.ToString());
            }
        }

        # endregion

        static public string GetRandomAvailableStorage()
        {
            string path = HelperAdapter.GetParameter("SystemConfig");
            Dictionary<string, string> dic = HelperAdapter.Load(path, "Storage");
            string[] storages = dic["All"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Random rd = new Random();
            return storages[rd.Next( storages.Length)];
        }

        static public int GetRandomInt(int randomMax)
        {
            Random rd = new Random();
            return rd.Next(randomMax);
        }
    }   
}
