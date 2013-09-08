//Class for Percolation

public class Percolation
{
    //The rows and columns.
    private int N;
    
    private int openNum=0;
    
    private WeightedQuickUnionUF QU;
    
    private int [][] map;
    
    private int convert (int i, int j)
    {
        return ((N+2) * i + j);
    }
    
    public Percolation(int N)
    {
        this.N=N;
        this.map=new int[N+2][N+2];
        this.QU=new WeightedQuickUnionUF((N+2)*(N+2));
        

        
        for(int j=1;j<N+1;j++)
        {
            map[0][j]=1;
            map[N+1][j]=1;
            if(j!=1)
            {
                QU.union(convert(0,1),convert(0,j));
                QU.union(convert(N+1,1),convert(N+1,j));
            }
        }
    }
    
    public boolean isOpen(int i,int j)
    {
        if(map[i][j]==1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void open(int i,int j)
    {
        if(!isOpen(i,j))
        {
            map[i][j]=1;
            openNum++;
            if(isOpen(i-1,j))
                QU.union(convert(i,j),convert(i-1,j));
            if(isOpen(i+1,j))
                QU.union(convert(i,j),convert(i+1,j));
            if(isOpen(i,j-1))
                QU.union(convert(i,j),convert(i,j-1));
            if(isOpen(i,j+1))
                QU.union(convert(i,j),convert(i,j+1));
        }
    }
    

    
    public boolean isFull(int i,int j)
    {
        return QU.connected(1,convert(i,j));
    }
    
    public boolean percolates()
    {
        return QU.connected(1,(N+2)*(N+2)-2);
    }
    
}