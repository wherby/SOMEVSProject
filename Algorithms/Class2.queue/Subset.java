import java.util.*;
public class Subset
{
     public static void main(String[] args) 
     {
         RandomizedQueue<String> list=new RandomizedQueue<String>();
         String s;
         Random rd=new Random();
         while(!StdIn.isEmpty())
         {
             s=StdIn.readString();
             list.enqueue(s);
         }
         int num=Integer.parseInt(args[0]);
         if((num<0)||(num>list.size()))
         {
              throw new IndexOutOfBoundsException();
         }       
         while(num!=0)
         {
             StdOut.println(list.dequeue());
             num--;
         }
     }
}