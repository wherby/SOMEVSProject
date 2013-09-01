﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuickUnion
{
    class Program
    {
        static void Main(string[] args)
        {
            QuickUnionUF qu = new QuickUnionUF(10);
            qu.union(7, 8);
            qu.union(8, 9);
            qu.test();
            Console.ReadLine();
        }
    }

    public class QuickUnionUF
    {
       private int [] id;

        public QuickUnionUF(int N)
       {
        id=new int[N];
        for(int i=0;i<N;i++)id[i]=i;
       }
       
       private int root(int i)
       {
		  while(i!=id[i])
           {
               i=id[i];
           }
		  return i;
       }
       
       public bool connnected(int p, int q)
       {
 	  	  return root(p)==root(q);
	   }
		  
	   public void union(int p,int q)
	    {
 		    int i=root(p);
 			int j=root(q);
 			id[i]=j;
		}
        public void test()
        {
            for(int i=0;i<id.Length;i++)
            {
                Console.WriteLine(string.Format("{0} and id {1}",i, id[i]));
            }
        }
    }
}
