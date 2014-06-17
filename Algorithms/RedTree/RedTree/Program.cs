using System;
using System.Collections.Generic;
using System.Text;

namespace RedTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Q1();

            Console.WriteLine("**********");
            Q2();
        }

        private static void Q2()
        {

            /*
             * (seed = 962251)
Consider the left-leaning red-black BST whose level-order traversal is

    35 13 86 10 31 71 91 29 68 72          ( red links = 29 71 )

What is the level-order traversal of the red-black BST that results after
inserting the following sequence of keys:

    40 80 37 
             */
            RedTreeInstance<int, int> rt = new RedTreeInstance<int, int>();
            int[] input ={35 ,21 ,73 ,10 ,31 ,52 ,88, 38, 71, 56, 23, 89, 54  };
            foreach (int t in input)
            {
                if (rt.root == null)
                {
                    rt.root = rt.put(rt.root, t, 0);
                }
                else
                {
                    RedTreeInstance<int,int>.Node tt= rt.put(rt.root, t, 0);
                    if (tt.left == rt.root || tt.right == rt.root)
                    {
                        rt.root = tt;
                    }
                }
            }

            IEnumerable<int> keys = rt.LevelOrderkeys();
            foreach (int t in keys)
            {
                Console.Write(" " + t);
                
                //rt.get(t);
            }

            Console.WriteLine();
            Console.WriteLine("root is " + rt.root.key);
        }

        private static void Q1()
        {
            /*
             * (seed = 821743)
Consider the left-leaning red-black BST whose level-order traversal is:

    67 59 81 24 63 73 94 18 50 62 66 90 48 

List (in ascending order) the keys in the red nodes. A node is red if the link
from its parent is red.
             */
            RedTreeInstance<int, int> rt = new RedTreeInstance<int, int>();
            int[] input ={  85, 55, 91, 29, 73, 87, 93, 27, 54, 65, 78 ,86 ,30    };
            foreach (int t in input)
            {
                if (rt.root == null)
                {
                    rt.root = rt.put(rt.root, t, 0);
                }
                else
                {
                    RedTreeInstance<int, int>.Node tt = rt.put(rt.root, t, 0);
                    if (tt.left == rt.root || tt.right == rt.root)
                    {
                        rt.root = tt;
                    }
                }
            }
            foreach (int t in input)
            {
                rt.get(t);
            }
        }
    }


}
