using System.Collections.Generic;
using System.Configuration;
using System;

namespace EMC.ESI.PowerShell.Helper
{
    /// <summary>
    /// The class for the common methods which will be used for test.
    /// </summary>
    public class HelperAdapter
    {
        /// <summary>
        /// The method is to extract key-value pair form result string.
        /// </summary>
        /// <param name="result">The result string.</param>
        /// <returns>The kry-value pairs for the result string.</returns>
        public static SortedList<string, string> GenerateKeyValuePairs(string result)
        {
            string[] resultLines = result.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < resultLines.Length; i++)
            {
                if (resultLines[i].StartsWith("   "))
                {
                    resultLines[i] = resultLines[i].Trim();
                    if (i > 0)
                    {
                        resultLines[i - 1] = resultLines[i - 1] + resultLines[i];
                    }
                }
            }
            SortedList<string, string> prams = new SortedList<string, string>();
            foreach (string resultLine in resultLines)
            {
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
    }
}