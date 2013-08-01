using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 



namespace GCTest
{
    public class Car
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
       public Car(string nameString)
        {
            name = nameString;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***GC Test**");
            Console.WriteLine("Byte on heap {0}", GC.GetTotalMemory(false));

            Console.WriteLine("Max generation {0}", GC.MaxGeneration + 1);

            Car carOne = new Car("One");
            Console.WriteLine("Max generation {0}", GC.MaxGeneration + 1);
            Console.WriteLine("CarOne generation {0}", GC.GetGeneration(carOne));

            GC.Collect();
            Console.WriteLine("CarOne generation {0} after  collection", GC.GetGeneration(carOne));
            GC.Collect();
            Console.WriteLine("CarOne generation {0} after 2  collection", GC.GetGeneration(carOne));
            GC.Collect();
            Console.WriteLine("CarOne generation {0} after 3 collections", GC.GetGeneration(carOne));
            GC.Collect();
            Console.WriteLine("CarOne generation {0} after  4  collections", GC.GetGeneration(carOne));
            Console.WriteLine("Max generation {0}", GC.MaxGeneration + 1);
            Console.ReadLine();
        }
    }
}
