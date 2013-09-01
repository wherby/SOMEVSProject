using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearch1.Test();
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
    }
}
