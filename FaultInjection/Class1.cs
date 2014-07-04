using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FaultInjection
{
    public static class FaultInjectionAdapter
    {
        static public void SetFaultCode<T>(T d, string fileName)
        {
            string[] alllines = File.ReadAllLines(fileName);


            #region

            foreach (string temp in alllines)
            {
                string[] temp2 = temp.Trim().Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (temp2.Count<string>() == 2)
                {
                    try
                    {
                        foreach (PropertyInfo temp3 in d.GetType().GetProperties())
                        {
                            if (temp3.ToString().Contains(temp2[0]))
                            {
                                if (d.GetType().GetProperty(temp2[0]).PropertyType == "a".GetType())
                                {
                                    d.GetType().GetProperty(temp2[0]).SetValue(d, temp2[1]);
                                }
                                else if (d.GetType().GetProperty(temp2[0]).PropertyType.BaseType.FullName == "System.Enum")
                                {
                                    var obEn = Enum.Parse(d.GetType().GetProperty(temp2[0]).PropertyType, temp2[1]);
                                    d.GetType().GetProperty(temp2[0]).SetValue(d, obEn);
                                }
                                else if (d.GetType().GetProperty(temp2[0]).PropertyType.GetElementType().BaseType.FullName == "System.Enum")
                                {
                                    var obEn = Enum.Parse(d.GetType().GetProperty(temp2[0]).PropertyType.GetElementType(), temp2[1]);
                                    var aEn = Array.CreateInstance(d.GetType().GetProperty(temp2[0]).PropertyType.GetElementType(), 1);
                                    aEn.SetValue(obEn, 0);
                                    d.GetType().GetProperty(temp2[0]).SetValue(d, aEn);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }
                }
                if (temp2.Count<string>() == 4)
                {
                    try
                    {
                        foreach (PropertyInfo temp3 in d.GetType().GetProperties())
                        {
                            if (temp3.ToString().Contains(temp2[0]))
                            {
                                if ((d.GetType().GetProperty(temp2[0]).GetValue(d)).ToString() == temp2[1])
                                {
                                    foreach (PropertyInfo temp4 in d.GetType().GetProperties())
                                    {
                                        if (temp4.ToString().Contains(temp2[2]))
                                        {
                                            if (d.GetType().GetProperty(temp2[2]).PropertyType == "a".GetType())
                                            {
                                                d.GetType().GetProperty(temp2[2]).SetValue(d, temp2[3]);
                                            }
                                            else if (d.GetType().GetProperty(temp2[2]).PropertyType.BaseType.FullName == "System.Enum")
                                            {
                                                var obEn = Enum.Parse(d.GetType().GetProperty(temp2[2]).PropertyType, temp2[3]);
                                                d.GetType().GetProperty(temp2[2]).SetValue(d, obEn);
                                            }
                                            else if (d.GetType().GetProperty(temp2[2]).PropertyType.GetElementType().BaseType.FullName == "System.Enum")
                                            {
                                                var obEn = Enum.Parse(d.GetType().GetProperty(temp2[2]).PropertyType.GetElementType(), temp2[3]);
                                                var aEn = Array.CreateInstance(d.GetType().GetProperty(temp2[2]).PropertyType.GetElementType(), 1);
                                                aEn.SetValue(obEn, 0);
                                                d.GetType().GetProperty(temp2[2]).SetValue(d, aEn);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }
                }
            }

            #endregion


        }
    }
}
