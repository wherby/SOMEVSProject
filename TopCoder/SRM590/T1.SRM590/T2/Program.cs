using System;
using System.Collections.Generic;

namespace T2
{
    class FoxAndGomoku
    {
        public String win(String[] board)
        {
            int [,] bd=new int[25,25];
            int N = board.Length;
            for(int i=0;i<N;i++)
                for(int j=0;j<N;j++)
            {
                if (board[i][j] == 'o')
                {
                    bd[i + 5, j + 5] = 1;
                }
                else bd[i + 5, j + 5] = 0;
            }
            for(int i=5;i<N+5;i++)
                for (int j = 5; j < N+5; j++)
                {
                    int x1=bd[i,j]+bd[i,j+1]+bd[i,j+2]+bd[i,j+3]+bd[i,j+4];
                    int y1 = bd[i, j] + bd[i + 1, j] + bd[i + 2, j] + bd[i + 3, j] + bd[i + 4, j];
                    int z1 = bd[i, j] + bd[i + 1, j+1] + bd[i + 2, j+2] + bd[i + 3, j+3] + bd[i + 4, j+4];
                    int z2 = bd[i, j] + bd[i + 1, j - 1] + bd[i + 2, j - 2] + bd[i + 3, j - 3] + bd[i + 4, j - 4];
                    if ((x1 == 5) || (y1 == 5) || (z1 == 5)||(z2==5)) return "found";
                }
            return "not found";
        }

        static void Main(string[] args)
        {
            string[] input ={"....o.",
 "...o..",
 "..o...",
 ".o....",
 "o.....",
 "......"};
            FoxAndGomoku fs = new FoxAndGomoku();
            string re= fs.win(input);

        }
    }
}
