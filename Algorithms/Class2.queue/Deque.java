import java.util.Iterator;
public class Deque<Item> implements Iterable<Item> 
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
    
    public Deque()
    {
        num=0;
        first=null;
        last=null;
    }
    
    public boolean isEmpty()
    {
        return first == null;
    }
    
    public int size()
    {
        return num;
    }
    
    public void addFirst(Item item)
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
    
    public void addLast(Item item)
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
    
    public Item removeFirst()
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
    
    public Item removeLast()
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