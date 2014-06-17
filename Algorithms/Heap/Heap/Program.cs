

using System;
using System.Collections.Generic;
using System.Text;

namespace Heap
{
    public class MaxPQ<T> where T:IComparable
    {
        private T[] pq;
        private int N;
        public MaxPQ(int capacity)
        {
            pq = new T[capacity + 1];
        }

        public bool isEmpty()
        {
            return N == 0;
        }

        public void insert(T key)
        {
            pq[++N] = key;
            swim(N);
        }

        public T delMAX()
        {
            T max = pq[1];
            exch(1, N--);
            sink(1);
            //pq[N + 1] = null;
            return max;
        }

        private void swim(int k)
        {
            while (k > 1 && less(k / 2, k))
            {
                exch(k, k / 2);
                k = k / 2;
            }
        }

        private void sink(int k)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && less(j, j+1)) j++;
                if (!less(k, j)) break;
                exch(k, j);
                k = j;
            }
        }

        private bool less(int i, int j)
        {
            return pq[i].CompareTo(pq[j])<0;
        }
        private void exch(int i, int j)
        {
            T temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }

        public void print()
        {
            for (int i = 1; i <= N; i++)
            {
                Console.Write(pq[i] + " ");
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Q1();
            Q2();



        }

        private static void Q2()
        {
            /*
             * (seed = 468797)
Give the sequence of the 7 keys in the array that results after performing 3 successive delete-the-max
operations on the following maximum-oriented binary heap of size 10:

    96 86 79 84 66 57 19 29 51 50 
             */
            MaxPQ<int> max = new MaxPQ<int>(20);
            int[] ori ={  91 ,79, 88, 66 ,65 ,81 ,73 ,33 ,25, 61   };

            foreach (int t in ori)
            {
                max.insert(t);
            }
            max.print();

            max.delMAX();
            
            max.print();
            max.delMAX();
            max.delMAX();
            max.print();
        }

        private static void Q1()
        {
            /*
 * seed = 589387)
Give the sequence of the 13 keys in the array that results after inserting the sequence of 3 keys

    35 81 38 

into the following maximum-oriented binary heap of size 10:

    97 86 67 53 75 17 22 51 50 66 
 */
            MaxPQ<int> max = new MaxPQ<int>(20);
            int[] ori ={  93 ,75, 84, 53 ,72 ,23, 70 ,37 ,48, 42    };

            foreach (int t in ori)
            {
                max.insert(t);
            }
            max.print();

            int[] ori2 ={ 39 ,86 ,47   };

            foreach (int t in ori2)
            {
                max.insert(t);
            }
            max.print();

        }
    }
}
