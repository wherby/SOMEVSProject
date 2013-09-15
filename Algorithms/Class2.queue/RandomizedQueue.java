import java.util.Iterator;
import java.util.*;
public class RandomizedQueue<Item> implements Iterable<Item> 
{
    private Node first=null;
    private Node last=null;
    private int num;
    
    private class Node
    {
        Item item;
        Node next;
        Node previous;
    }
    
    public RandomizedQueue()
    {
        first=null;
        last=null;
        num=0;
    }
    
    public boolean isEmpty()
    {
        return first == null;
    }
    
    public int size()
    {
        return num;
    }
    
    public void enqueue(Item item)
    {
        Random rd=new Random();
        int r=rd.nextInt(100);
        if(r%2==0)
        {
            addFirst(item);
        }
        else
        {
            addLast(item);
        }
    }
    
    private void addFirst(Item item)
    {
        Node oldfirst = first;
        first = new Node();
        first.item = item;
        first.next = oldfirst;
        first.previous = null;
        if(oldfirst != null)
        {
            oldfirst.previous = first;
        }
        else
        {
            last=first;
        }
        num++;
    }
    
    private void addLast(Item item)
    {
        Node oldlast = last;
        last = new Node();
        last.item = item;
        last.previous=oldlast;
        last.next=null;
        if(oldlast != null)
        {
            oldlast.next = last;
        }
        else
        {
            first=last;
        }
        num++;
    }
    
    public Item dequeue()   
    {
        Random rd=new Random();
        int r=rd.nextInt(100);
        Item temp;
        if(r%2==0)
        {
            temp=removeFirst();
        }
        else
        {
            temp=removeLast();
        }       
        return temp;
    }
    
    private Item removeFirst()
    {
        if(first == null)
        {
             throw new IndexOutOfBoundsException();
        }
        else
        {
            Node oldfirst=first;
            first=first.next;
            if(first!=null)
            {
                first.previous=null;
            }
            else
            {
                last=null;
            }
            num--;
            return oldfirst.item;
        }
    }
    
    private Item removeLast()
    {
        if(last == null)
        {
             throw new IndexOutOfBoundsException();
        }
        else
        {
            Node oldlast=last;
            last=last.previous;
            if(last!=null)
            {
                last.next=null;
            }
            else
            {
                first=null;
            }
            num--;
            return oldlast.item;
        }    
    }
    
    public Item sample()
    {
        Random rd=new Random();
        int r=rd.nextInt(num);
        Node temp=first;     
        while(r!=0)
        {
            temp=first.next;
            r--;
        }
        return temp.item;
    }
    
    public Iterator<Item> iterator()
    {
        return new ListIterator();
    }
    
    private class ListIterator implements Iterator<Item>
    {
        private Node current=first;
        public boolean hasNext()
        {
            return current != null;
        }
        public void remove()
        {
        }
        
        public Item next()
        {
            Item item=current.item;
            current=current.next;
            return item;
        }
    }    
}