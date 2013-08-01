using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static int Count(int a)
        {
            int COUNT =26;
            int b=a;
            string str = a.ToString();
            bool Minus1 = true;
            for (int j = 1; j < 100; j++)
            {
                if (Minus1 == true)
                {
                    b = b * 2 - 1;
                }
                else
                {
                    b = b * 2;
                }
                str = str + " " + b;
                if (b > COUNT)
                {
                    b = b - COUNT;
                    if (Minus1 == true)
                    {
                        Minus1 = true;
                    }
                    else
                    {
                        Minus1=true;
                    }
                }
                if (b == a)
                {
                   // Console.WriteLine(str);
                    return j;
                }

            }
            return 100;
        }
        static void Main(string[] args)
        {
            for (int i = 2; i < 26; i++)
            {
                Console.WriteLine(string.Format("{0} in {1}", i, Count(i)));

            }
            Console.ReadLine();
        }
    }
}
