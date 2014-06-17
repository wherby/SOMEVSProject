using System;
using System.Collections.Generic;
using System.Text;

namespace BST
{
    public class BST<T,S> where T : IComparable
    {
        private class Node
        {
            public  T key;
            public S value;
            public Node left, right;
            public int count;
            public Node(T key, S value)
            {
                this.key = key;
                this.value = value;
            }
        }

        private Node root;

        public int size()
        {
            return size(root);
        }

        private int size(Node x)
        {
            if (x == null) return 0;
            return x.count;
        }

        public int rank(T key)
        {
            return rank(key, root);
        }

        private int rank(T key, Node x)
        {
            if (x == null) return 0;
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return rank(key, x.left);
            else if (cmp > 0) return 1 + size(x.left) + rank(key, x.right);
           // else if (cmp == 0) return size(x.left);
            else return size(x.left);
        }

        public void put(T key, S val)
        {
            root=put(root ,key ,val);

        }

        private Node put(Node x,T key,S val)
        {
            if(x==null) return new Node(key,val);
            int cmp=key.CompareTo(x.key);
            if (cmp<0)
                x.left=put(x.left,key,val);
            else if(cmp>0)
                x.right=put(x.right,key,val);
            else if(cmp==0)
                x.value=val;
            x.count = 1 + size(x.left) + size(x.right);
            return x;
        }

        public S get(T key)
        { 
            Node x=root;
            while(x!=null)
            {
                int cmp=key.CompareTo(x.key);
                if (cmp<0) x=x.left;
                else if (cmp>0) x=x.right;
                else if(cmp==0) return x.value;
            }
            return default(S);
        }

        public T floor(T key)
        {
            Node x = floor(root, key);
            if (x == null)
                return default(T);
            return x.key;
        }

        private Node floor(Node x, T key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.value);

            if (cmp == 0) return x;

            if (cmp < 0) return floor(x.left, key);

            Node t = floor(x.right, key);
            if (t != null) return t;
            else return x;
        }

        public IEnumerable<T> LevelOrderkeys()
        {
            Queue<T> q = new Queue<T>();
            Queue<Node> q2 = new Queue<Node>();
            if (root != null)
            {
                q2.Enqueue(root);
            }
            levelorder( q,q2);
            return q;
        }

        private void levelorder(Queue<T> q,Queue<Node> q2)
        {
            if (q2.Count == 0)
                return;
            Node temp = q2.Dequeue();
            q.Enqueue(temp.key);
            if (temp.left != null)
            {
                q2.Enqueue(temp.left);

            }
            if (temp.right != null)
            {
                q2.Enqueue(temp.right);
            }
            levelorder(q, q2);
        }

        public  IEnumerable<T> keys()
        {
            Queue<T> q = new Queue<T>();
            inorder(root, q);
            return q;
        }

        private void inorder(Node x, Queue<T> q)
        {
            if (x == null) return;
            inorder(x.left, q);
            q.Enqueue(x.key);
            inorder(x.right, q);
        }

        public void deleteMin()
        {
            root = deleteMin(root);
        }

        private Node deleteMin(Node x)
        {
            if (x.left == null) return x.right;
            x.left = deleteMin(x.left);
            x.count = 1 + size(x.left) + size(x.right);
            return x;
        }

        public void delete(T key)
        {
            root = delete(root,key);
        }

        private Node delete(Node x, T key)
        {
            if (x == null) return null;
            int cmp=key.CompareTo(x.key);
            if (cmp < 0) x.left = delete(x.left, key);
            else if (cmp > 0) x.right = delete(x.right, key);
            else
            {
                if (x.right == null) return x.left;
                if (x.left == null) return x.right;

                Node t = x;
                x = min(t.right);
                x.right = deleteMin(t.right);
                x.left = t.left;
            }
            x.count = size(x.left) + size(x.right) + 1;
            return x;
        }

        private BST<T, S>.Node min(BST<T, S>.Node x)
        {
            if (x == null) return null;
            if (x.left == null) return x;
            else return min(x.left);
        }

        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Q1();
            Q2();
        }
        
        private static void Q1()
        {
            /*
(seed = 10583)
Give the level-order traversal of the BST that results after inserting
the following sequence of keys into an initially empty BST:

    51 25 22 16 11 23 78 76 38 68 
                */
            BST<int, int> bt = new BST<int, int>();
            int[] lin ={ 86 ,79, 48, 24, 66, 53, 16, 51, 49 ,63 };
            foreach (int t in lin)
            {
                bt.put(t, 0);
            }

            IEnumerable<int> qu = bt.LevelOrderkeys();
            foreach (int temp in qu)
            {
                Console.Write(temp + " ");
            }
            Console.WriteLine();
        }

        private static void Q2()
        {
            /*
             * (seed = 699464)
Given a BST whose level-order traversal is:

    99 91 53 94 45 79 23 59 14 29 57 11 

What is the level-order traversal of the resulting BST after Hibbard deleting
the following three keys?

    29 99 53 
                */
            BST<int, int> bt = new BST<int, int>();
            int[] lin ={ 85, 67, 86, 48, 68, 90, 30, 52, 28, 45, 50, 54 };
            foreach (int t in lin)
            {
                bt.put(t, 0);
            }
            int[] del ={68 ,86 ,48   };
            foreach (int t in del)
            {
                bt.delete(t);
            }
            IEnumerable<int> qu = bt.LevelOrderkeys();
            foreach (int temp in qu)
            {
                Console.Write(temp + " ");
            }
        }
    }
}
