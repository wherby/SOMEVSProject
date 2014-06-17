using System;
using System.Collections.Generic;
using System.Text;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Merge
    {
        static private <Item>[] a, aux;
        public Merge(Comparable[] ai)
        {
            a = ai;
        }
        private static void merge(Comparable[] a, Comparable[] aux, int lo, int mid, int hi)
        {
            for (int k = lo; k <= hi; k++)
            {
                aux[k] = a[k];
            }
            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid) a[k] = aux[j++];
                else if (i > hi) a[k] = aux[i++];
                else if (less(aux[i], aux[j])) a[k] = aux[j++];
                else a[k] = aux[i++];
            }
        }

        private static boolean less(Comparable v, Comparable w)
        {
            return v.compareTo(w) < 0;
        }

        private static void sort(Comparable[] a, Comparable[] aux, int lo, int hi)
        {
            if (hi <= lo) return;
            int mid = lo + (hi - lo) / 2;
            sort(a, aux, lo, mid);
            sort(a, aux, mid + 1, hi);
            merge(a, aux, lo, mid, hi);
        }
        public static void sort(Comparable[] a)
        {
            aux = new Comparable[a.length];
            sort(a, aux, 0, a.length - 1);
        }
    }
}
