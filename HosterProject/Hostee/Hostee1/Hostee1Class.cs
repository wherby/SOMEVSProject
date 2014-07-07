using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HosteeBase;

namespace Hostee1
{


    [Serializable]
    public class Hostee1 : BaseHostee
    {

        String a, b;

        public string B
        {
            get { return b; }
            set { b = value; }
        }

        public string A
        {
            get { return a; }
            set { a = value; }
        }

        public void Invoke()
        {
            int a1 = Int32.Parse(A);
            int b1 = Int32.Parse(B);
            Console.WriteLine("{0}+{1}={2}", a1, b1, a1 + b1);
        }

        public override void Init()
        {
            Li.Add("A");
            Li.Add("B");
            
        }
    }
}
