using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMC.ESI.PowerShell.Monitor
{
    class ElementClass
    {
        List<KeyValuePair<string,string>> mandatory=new List<KeyValuePair<string,string>>();
        List<string> alternate=new List<string>();
        public ElementClass(string elemString)
        {
            string[] strs = elemString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string temp in strs)
            {
                string[] strSection = temp.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if ((strSection[0] == "alternate") && (strSection.Count<string>() == 2))
                {
                    alternate.Add(strSection[1]);
                }
                if ((strSection[0] == "mandatory")&&(strSection.Count<string>()==2))
                {
                    string[] temp2 = strSection[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach(string temp3 in temp2)
                    {
                        string temp4=temp3.Trim();
                        string[] temp5 = temp4.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string key = temp5[0].Substring(1, temp5[0].Length - 1);
                        string value = string.Empty;
                        if (temp5.Count<string>() == 2)
                        {
                            value = temp5[1];
                        }
                        bool isTheKeyExist = false;
                        foreach (KeyValuePair<string, string> temp6 in mandatory)
                        {
                            if (temp6.Key == key)
                            {
                                isTheKeyExist = true;
                            }
                        }
                        if (isTheKeyExist == false)
                        {
                            mandatory.Add(new KeyValuePair<string, string>(key, value));
                        }
                    }
                }
                if ((strSection[0] == "optional") && (strSection.Count<string>() == 2))
                {
                    string[] temp2 = strSection[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string temp3 in temp2)
                    {
                        string temp4 = temp3.Trim();
                        string[] temp5 = temp4.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string key = temp5[0].Substring(1, temp5[0].Length - 1);
                        string value = string.Empty;
                        if (temp5.Count<string>() == 2)
                        {
                            value = temp5[1];
                        }
                        bool isTheKeyExist = false;
                        foreach (KeyValuePair<string, string> temp6 in mandatory)
                        {
                            if (temp6.Key == key)
                            {
                                isTheKeyExist = true;
                            }
                        }
                        if (isTheKeyExist == false)
                        {
                            mandatory.Add(new KeyValuePair<string, string>(key, value));
                        }
                    }
                }
            }
        }

        public List<KeyValuePair<string, string>> Mandatory
        {
            get
            {
                return mandatory;
            }
        }
    }
}
