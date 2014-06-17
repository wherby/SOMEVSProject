using System;
using System.Collections.Generic;
using System.Text;

namespace KDTree
{


    class Program
    {
        static void Main(string[] args)
        {
            Q2();
            //Q3();
        }

        private static void Q3()
        {
            //Wrong answer
            TwoDTree<float> tdTree = new TwoDTree<float>();
            string inputString = @"A [ 6, 28]
B [21, 35]
C [30, 39]
D [ 8, 34]
E [ 1, 17]
F [15, 40]
G [ 0, 19]
H [27, 29]";
            string[] str = inputString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string temp in str)
            {
                string[] strTemp = temp.Split(new char[] { ' ' }, 2);
                string key = strTemp[0];
                string valT = strTemp[1].Replace("[", "").Replace("]", "");
                string[] valTemp = valT.Split(new char[] { ',' }, 2);
                float f1 = float.Parse(valTemp[0]);
                float f2 = float.Parse(valTemp[1]);
                tdTree.Insert(new Point<float>(f1, f2, key));
            }
          //  tdTree.Search(new Point<float>(37, 38, "Z"));
        }

        private static void Q2()
        {
            /*
             * Question 2
(seed = 537571)
What is the level-order traversal of the kd-tree that results after inserting
the following sequence of points into an initially tree?

A (0.36, 0.66)
B (0.73, 0.03)
C (0.62, 0.51)
D (0.85, 0.31)
E (0.20, 0.16)
F (0.05, 0.81)
G (0.47, 0.39)
H (0.76, 0.91)

Your answer should be a sequence of letters, starting with A and separated by single spaces.

Recall that our convention is to subdivide the region using the x-coordinate at even levels
(including the root) and using the y-coordinate at odd levels. Also, we use the left subtree
for points with smaller x- or y-coordinates.
             */
            TwoDTree<float> tdTree = new TwoDTree<float>();
            string inputString = @"A (0.53, 0.90)
B (0.98, 0.82)
C (0.59, 0.41)
D (0.28, 0.77)
E (0.16, 0.31)
F (0.13, 0.11)
G (0.05, 0.88)
H (0.87, 0.03)";
            string[] str = inputString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string temp in str)
            {
                string[] strTemp = temp.Split(new char[] { ' ' }, 2);
                string key = strTemp[0];
                string valT = strTemp[1].Replace("(", "").Replace(")", "");
                string[] valTemp = valT.Split(new char[] { ',' }, 2);
                float f1 = float.Parse(valTemp[0]);
                float f2 = float.Parse(valTemp[1]);
                tdTree.Insert(new Point<float>(f1, f2, key));
            }
            IEnumerable<string> keys = tdTree.LevelOrderkeys();
            foreach (string temp in keys)
            {
                Console.Write(temp + " ");
            }
        }
    }
}
