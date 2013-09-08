import java.util.*;

public class PercolationStats 
{
    private double [] threshold;
    private int T;
    
    public PercolationStats(int N,int T)
    {
        this.T=T;
        threshold=new double[T];
        Random rd=new Random();
        
        for(int i=0;i<T;i++)
        {
            Percolation p=new Percolation(N);
            int count=0;
            while(!p.percolates())
            {
                //System.out.println("One more time");
                int a=rd.nextInt(N)+1;
                int b=rd.nextInt(N)+1;
                if(!p.isOpen(a,b))
                {
                    p.open(a,b);
                    count++;
                }
            }
            int numberOpened=count;
            threshold[i]=(numberOpened *1.0)/(N*N);
        }
        
        double p_mean = mean();
        System.out.println("Mean: " + p_mean);

        double p_std = stddev();
        System.out.println("Standard Deviation: " + p_std);

        double nine_five_percent1 = p_mean - (1.96 * p_std) / Math.sqrt(T);
        double nine_five_percent2 = p_mean + (1.96 * p_std) / Math.sqrt(T);
        System.out.println("95 Percent: " + nine_five_percent1 + " , " + nine_five_percent2);
        
    }
 public double confidenceLo()
 {
      double p_mean = mean();
      double p_std = stddev();
      return  p_mean - (1.96 * p_std) / Math.sqrt(T);
 }
 
 public double confidenceHi()
 {
      double p_mean = mean();
      double p_std = stddev();
      return  p_mean + (1.96 * p_std) / Math.sqrt(T);
 }
    
    
 public double mean()
 {
  return StdStats.mean(threshold);
 }

 public double stddev()
 {
  return StdStats.stddev(threshold);
 }

 public static void main(String[] args) 
 {
  int N = 0; 
  int T = 0;
  Scanner scan = new Scanner(System.in);

  System.out.println("Please enter the width (and height) of the grid: ");
  N = scan.nextInt();

  System.out.println("Please enter the number of tests to run: ");
  T = scan.nextInt();

  PercolationStats stats = new PercolationStats(N, T);




 }
        
}