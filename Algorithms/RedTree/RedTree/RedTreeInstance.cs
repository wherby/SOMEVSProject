using System;
using System.Collections.Generic;
using System.Text;

namespace RedTree
{


    public  class RedTreeInstance<Key,Val>where Key:IComparable
    {
        private static bool RED = true;
        private static bool BLACK = false;
        public Node root;

        public class Node
        {
            public Key key;
            public Val value;
            public Node left, right;
            public bool color;

            public Node(Key key, Val value, bool color)
            {
                this.key = key;
                this.value = value;
                this.color = color;
                left = null;
                right = null;
            }
        }

        public RedTreeInstance()
        {
            root = null;
        }



        private bool isRed(Node x)
        {
            if (x == null) return false;
            return x.color == RED;
        }

        public Val get(Key key)
        {
            Node x=root;
            while(x!=null)
            {
                int cmp = key.CompareTo(x.key);
                if (cmp < 0) x = x.left;
                else if (cmp > 0) x = x.right;
                else
                {
                    if (x != null)
                    {
                        Console.WriteLine(" color of :" +x.key+"   : " +x.color);
                    }
                    return x.value;
                }
            }
            return default(Val);
        }

        private Node rotateLeft(Node h)
        {
            Node x = h.right;
            h.right = x.left;
            x.left = h;
            x.color = h.color;
            h.color = RED;
            return x;
        }

        private Node rotateRight(Node h)
        {
            Node x = h.left;
            h.left = x.right;
            x.right = h;
            x.color = h.color;
            h.color = RED;
            return x;
        }

        private void flipColors(Node h)
        {
            h.color = RED;
            h.left.color = BLACK;
            h.right.color = BLACK;
        }

        public Node put(Node h, Key key, Val value)
        {
            if (h == null)
            {
                return new Node(key, value, RED);
            }
            int cmp = key.CompareTo(h.key);
            if (cmp < 0) h.left = put(h.left, key, value);
            else if (cmp > 0) h.right = put(h.right, key, value);
            else h.value = value;

            if (isRed(h.right) && (!isRed(h.left))) h = rotateLeft(h);
            if (isRed(h.left) && isRed(h.left.left)) h = rotateRight(h);
            if (isRed(h.left) && isRed(h.right)) flipColors(h);

            return h;
        }

        public IEnumerable<Key> LevelOrderkeys()
        {
            Queue<Key> q = new Queue<Key>();
            Queue<Node> q2 = new Queue<Node>();
            if (root != null)
            {
                q2.Enqueue(root);
            }
            levelorder(q, q2);
            return q;
        }

        private void levelorder(Queue<Key> q, Queue<Node> q2)
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
    }
}
