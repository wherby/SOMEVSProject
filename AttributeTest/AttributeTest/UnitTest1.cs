using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AttributeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            FirstClass a = new FirstClass();
            PrintAuthorInfo(a.GetType());
            PrintAuthorInfo(typeof(SecondClass));
            PrintAuthorInfo(typeof(ThirdClass));
            System.Console.WriteLine("new:");
            baseClass b = new SecondClass();
            baseClass c = new ThirdClass();
            a.SelfPrint();
            b.SelfPrint();
            c.SelfPrint();
        }

        private static void PrintAuthorInfo(System.Type t)
        {
            System.Console.WriteLine("Author information for {0}", t);
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);  // reflection

            foreach (System.Attribute attr in attrs)
            {
                
                if (attr is Lock)
                {
                    Lock a = (Lock)attr;
                    System.Console.WriteLine("   {0}, version {1:f}", a.GetName(), a.version);
                }
            }
        }

    }

    public class baseClass
    {
        public virtual void SelfPrint()
        {

            System.Type t = this.GetType();
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);  // reflection

            foreach (System.Attribute attr in attrs)
            {

                if (attr is Lock)
                {
                    Lock a = (Lock)attr;
                    System.Console.WriteLine("   {0}, version {1:f}", a.GetName(), a.version);
                    a.GetResource();
                }
            }
        }
    }


    [Lock("M. Knott", version = 2.0)]
    public  class FirstClass:baseClass
    {
        // ...
    }

    // No Author attribute
    public class SecondClass:baseClass
    {
        // ...
    }

    [ Lock("M. Knott", version = 2.0)]
    public class ThirdClass:baseClass
    {
        // ...
    }



}
