using System;
using System.Collections.Generic;
using System.Text;

namespace KDTree
{
    public class Point<T> where T : IComparable
    {
        private T x;

        public T X
        {
            get { return x; }
            set { x = value; }
        }
        private T y;

        public T Y
        {
            get { return y; }
            set { y = value; }
        }
        public string key;

        public Point(T x, T y,string keyString)
        {
            this.x = x;
            this.y = y;
            this.key = keyString;
        }
    }

    public class Node<S> where S : IComparable
    {
        public Node<S> left;
        public  Node<S> right ;
        public bool ver;

        public  Point<S> Value;

        public Node(Point<S> pointT)
        {
            Value = pointT;
        }

        public string key;
    }

    public class TwoDTree<S> where S : IComparable
    {
        Node<S> root;
        public TwoDTree()
        {
            root = null;
        }
        public void Insert(Point<S> x)
        {
            if (root == null)
            {
                root = new Node<S>(x);
                root.ver = true;
            }
            else
            {
                Insert(x, root);
            }
        }




        private void Insert(Point<S> point, Node<S> parent)
        {
            if (parent.ver == true)
            {
                if (point.X.CompareTo(parent.Value.X)<0)
                {
                    if (parent.left == null)
                    {
                        parent.left = new Node<S>(point);
                        parent.left.ver = false;
                    }
                    else
                    {
                        Insert(point, parent.left);
                    }
                }
                if (point.X .CompareTo( parent.Value.X)>0)
                {
                    if (parent.right == null)
                    {
                        parent.right = new Node<S>(point);
                        parent.right .ver = false;
                    }
                    else
                    {
                        Insert(point, parent.right);
                    }
                }
            }
            else
            {
                if (point.Y .CompareTo( parent.Value.Y)<0)
                {
                    if (parent.left == null)
                    {
                        parent.left = new Node<S>(point);
                        parent.left .ver = true;
                    }
                    else
                    {
                        Insert(point, parent.left);
                    }
                }
                if (point.Y .CompareTo( parent.Value.Y)>0)
                {
                    if (parent.right == null)
                    {
                        parent.right = new Node<S>(point);
                        parent.right .ver = true;
                    }
                    else
                    {
                        Insert(point, parent.right);
                    }
                }
            }
        }

        public void Search(Point<S> point)
        {
            if (root == null)
            {
                Console.Write("No element");
            }
            Search(point,root);
        }

        private void Search(Point<S> point, Node<S> parent)
        {
            if (parent != null)
            {
                Console.Write(parent.Value.key + " ");

            }
            else
            {
                return;
            }
            if (parent.ver == true)
            {
                if (point.X.CompareTo(parent.Value.X) < 0)
                {
                    Search(point, parent.left);
                }
                else
                {
                    Search(point, parent.right);
                }
            }
            else
            {
                if (point.Y.CompareTo(parent.Value.Y) < 0)
                {
                    Search(point, parent.left);
                }
                else
                {
                    Search(point, parent.right);
                }
            }
        }

        public IEnumerable<string> LevelOrderkeys()
        {
            Queue<string> q = new Queue<string>();
            Queue<Node<S>> q2 = new Queue<Node<S>>();
            if (root != null)
            {
                q2.Enqueue(root);
            }
            levelorder(q, q2);
            return q;
        }

        private void levelorder(Queue<string> q, Queue<Node<S>> q2)
        {
            if (q2.Count == 0)
                return;
            Node<S> temp = q2.Dequeue();
            q.Enqueue(temp.Value.key);
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
