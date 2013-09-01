using System;
using System.Collections.Generic;
using System.Text;

namespace QuickUnion
{
    public class QuickUnionUF2
    {
        public class unionClass
        {
            public int id;
            public int count;
            public unionClass(int idN, int countN)
            {
                id = idN;
                count = countN;
            }

        }
        private unionClass[] id;

        public QuickUnionUF2(int N)
        {
            id = new unionClass[N];
            for (int i = 0; i < N; i++)
            {
                id[i] = new unionClass(i, 1);
            }
        }

        private unionClass root(int i)
        {
            while (i != id[i].id)
            {
                i = id[i].id;
            }
            return id[i];
        }

        public bool connnected(int p, int q)
        {
            return root(p).id == root(q).id;
        }

        public void union(int p, int q)
        {
            unionClass i = root(p);
            unionClass j = root(q);
            if (i.count > j.count)
            {
                j.id = i.id;
                i.count = i.count + j.count;
            }
            else
            {
                i.id = j.id;
                j.count = i.count + j.count;
            }
        }
        public void test()
        {
            for (int i = 0; i < id.Length; i++)
            {
                Console.WriteLine(string.Format("{0} and id {1}and weight {2}", i, id[i].id,id[i].count));
            }
        }
    }
}
