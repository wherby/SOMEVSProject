using System;
using System.Collections.Generic;
using System.Text;

namespace QuickUnion
{
    public class QuickUnionUF
    {
        private int[] id;

        public QuickUnionUF(int N)
        {
            id = new int[N];
            for (int i = 0; i < N; i++) id[i] = i;
        }

        private int root(int i)
        {
            while (i != id[i])
            {
                i = id[i];
            }
            return i;
        }

        public bool connnected(int p, int q)
        {
            return id[p]==id[q];
        }

        public void union(int p, int q)
        {
            int pid = root(p);
            int qid = root(q);
            for (int i = 0; i < id.Length; i++)
            {
                if (id[i] == pid)
                {
                    id[i] = qid;
                }
            }
        }
        public void test()
        {
            for (int i = 0; i < id.Length; i++)
            {
                Console.WriteLine(string.Format("{0} and id {1}", i, id[i]));
            }
        }


        public void test2()
        {
            //string input = "0-2";
            string input = "8-1 9-3 4-1 0-1 2-4 7-5";
            string[] pairs = input.Split(' ');
            foreach (string temp in pairs)
            {
                string[] pair = temp.Split('-');
                int a = int.Parse(pair[0]);
                int b = int.Parse(pair[1]);
                union(a, b);
            }
            test();
        }
        

    }
}
