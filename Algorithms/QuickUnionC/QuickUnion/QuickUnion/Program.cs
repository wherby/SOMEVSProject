using System;
using System.Collections.Generic;
using System.Text;

namespace QuickUnion
{
    class Program
    {
        static void Main(string[] args)
        {
            QuickUnionUF3 qu = new QuickUnionUF3(10);
            qu.union(1, 2);
            qu.union(1, 7);
            qu.union(1, 0);
            qu.union(8, 3);
            qu.union(8, 9);
            qu.union(3, 4);
            qu.union(0, 5);
            qu.union(5, 6);
            qu.union(8, 1);
            qu.test();
            Console.ReadLine();
        }
    }
    #region quickunion simple

    #endregion 
}
