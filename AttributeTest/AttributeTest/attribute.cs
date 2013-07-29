using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

namespace AttributeTest
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct,
                       AllowMultiple = true)  // multiuse attribute
]
    public class Lock : System.Attribute
    {
        string name;
        public double version;

        private string[] list = new string[] { "aa", "bb", "cc" };
        Random rd = new Random((int)DateTime.Now.Ticks);
        public Lock(string name)
        {
            this.name = name;
            version = 1.0;  // Default value
        }

        public string GetName()
        {
            return name;
        }

        public string SetName()
        {

            name=list[rd.Next(list.Length)];
            return name;
        }

        public void GetResource()
        {

        }
    }


}
