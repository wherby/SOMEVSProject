using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HosteeBase;

namespace Hostee
{


     [Serializable]
    public class Test 
    {
        static void Main(string[] args)
        {
            //Helper.Load();
        }

        static public void Test2()
        {
            BaseHostee tebase = new BaseHostee();
            SortedList<string, string> Properties = new SortedList<string, string>();
            Properties.Add("A", "10");
            Properties.Add("B", "24");
            tebase.addPro("A");
            //tebase.SetProperties(Properties);
            Test1 t = new Test1();
            t.addPro("A");
            t.addPro("B");
            t.SetProperties(Properties);
            t.Invoke();
        }
    }

     [Serializable]
     public class Test1 : BaseHostee
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

         public  List<string> Init()
         {
             List<string> li = new List<string>();
             li.Add("a");
             li.Add("b");
             return li;
         }
    }

}
