using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySearch
{
    class BinarySearch1
    {
        public BinarySearch1()
        {

        }
        public static int binarySearch(int[] a, int key)
        {
            int lo = 0, hi = a.Length - 1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (key < a[mid])
                {
                    hi = mid - 1;
                }
                else if (key > a[mid])
                {
                    lo = mid + 1;
                }
                else return mid;
            }
            return -1;
        }

        public static void Test()
        {

            int[] a ={ 2, 3, 4, 5, 8, 9, 10 };
            Console.Write("The list is ");
            foreach(int temp in a)
            {
                Console.Write(temp + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(string.Format("{0} in list is {1}", i, binarySearch(a, i)));
            }
            Console.ReadLine();
        }
    }
}
