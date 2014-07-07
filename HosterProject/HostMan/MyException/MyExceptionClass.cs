using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyException
{
    class Test
    {

        static void Main(string[] args)
        {
        }
    }
    
    public class MyExceptionClass : System.Exception
    {
        public MyExceptionClass(string Msg)
        {
            new Exception(Msg);
        }
    }
}
