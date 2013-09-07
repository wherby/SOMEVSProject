using System;
using System.Collections.Generic;
using System.Text;

namespace FoxAndGo3
{
    class FoxAndGo
    {
       public  class PT
        {
            public int x, y;
            public PT(int x1, int y1)
            {
                x = x1;
                y = y1;
            }
        }
        public int maxKill(String[] board)
        {

            int[,] bd = new int[21, 21];
            int N = board.Length;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    if (board[i][j] == 'o')bd[i+1 ,j+1] = 1;
                    if (board[i][j] == 'x') bd[i+1, j+1] = 0;
                    if (board[i][j] == '.') bd[i +1,j+1] = 2;
                }
            int[,] pd = new int[21, 21];
            List<List<PT>> LL = new List<List<PT>>();
            for(int i=1;i<N+1;i++)
                for (int j = 0; j < N + 1; j++)
                {
                    if ((bd[i, j] == 1) && (pd[i, j] != 1))
                    {
                        List<PT> tempL = new List<PT>();
                        Stack<PT> stak = new Stack<PT>();
                        stak.Push(new PT(i, j));
                        pd[i, j] = 1;
                        tempL = BFS(bd, pd, tempL, stak);
                        LL.Add(tempL);
                    }
                }
            int count = 0;
            foreach (List<PT> temp in LL)
            {
                if (liveC(temp,bd).Count == 0)
                {
                    count += temp.Count;
                }
            }
            List<List<PT>> TLL = new List<List<PT>>();
            for (int i=0;i<LL.Count;i++)
            {
                if (liveC(LL[i], bd).Count == 1)
                {
                    List<PT> t11 = LL[i];
                    List<PT> tp = liveC(LL[i], bd);
                    for (int j = i + 1; j < LL.Count; j++)
                    {
                        if (liveC(LL[j], bd).Count==1)
                        {
                             List<PT> tp2=liveC(LL[j], bd);
                            if((tp2[0].x==tp[0].x)&&(tp2[0].y==tp[0].y))
                            t11.AddRange(LL[j]);
                        }
                    }
                    TLL.Add(t11);

                }
            }
            int max = 0;
            foreach (List<PT> temp in TLL)
            {
                if (max < temp.Count)
                {
                    max = temp.Count;
                }
            }
            return count+max;
        }
        public List<PT> liveC(List<PT> tempL, int[,] bd)
        {
            List<PT> hp = new List<PT>();
            int n = 0;
            foreach (PT temp in tempL)
            {
                if (bd[temp.x + 1, temp.y] == 2) { n++; hp.Add(new PT(temp.x + 1, temp.y)); }
                if (bd[temp.x - 1, temp.y] == 2) { n++; hp.Add(new PT(temp.x - 1, temp.y)); }
                if (bd[temp.x, temp.y + 1] == 2) { n++; hp.Add(new PT(temp.x , temp.y+1)); }
                if (bd[temp.x, temp.y - 1] == 2) { n++; hp.Add(new PT(temp.x , temp.y-1)); }
            }
            return hp;
        }

        public List<PT> BFS( int[,] bd, int[,] pd, List<PT> tempL,Stack<PT> stak)
        {
            if (stak.Count == 0) return tempL;
            else
            {
                PT temp = stak.Pop();
                int i = temp.x;
                int j = temp.y;
                if ((bd[i+1, j] == 1) && (pd[i+1, j] != 1))
                {
                    PT tp = new PT(i+1, j);
                    stak.Push(tp);
                    pd[i+1, j] = 1;
                }
                if ((bd[i , j+1] == 1) && (pd[i, j+1] != 1))
                {
                    PT tp = new PT(i, j+1);
                    stak.Push(tp);
                    pd[i , j+1] = 1;
                }
                if ((bd[i, j - 1] == 1) && (pd[i, j - 1] != 1))
                {
                    PT tp = new PT(i, j - 1);
                    stak.Push(tp);
                    pd[i, j - 1] = 1;
                }
                if ((bd[i-1, j ] == 1) && (pd[i-1, j ] != 1))
                {
                    PT tp = new PT(i-1, j );
                    stak.Push(tp);
                    pd[i-1, j ] = 1;
                }
                tempL.Add(temp);
                BFS(bd, pd, tempL, stak);
            }
            return tempL;
        }

        static void Main(string[] args)
        {
            string[] input =
{"oooooooox",
  "xxxxoxxxx",
  "xxxx.oxxx",
  "xxxx.xxxx",
  "xxxxxoxxx",
  "xxxxooxxx",
  "xxxxooxxx",
  "xxxx.xxxx",
  "ooooooox."};
            FoxAndGo go = new FoxAndGo();
            int a=go.maxKill(input);
        }
    }
}


/*

Problem Statement
    
Fox Ciel is teaching her friend Jiro to play Go. Ciel has just placed some white and some black tokens onto a board. She now wants Jiro to find the best possible move. (This is defined more precisely below.)   You are given a String[] board. Character j of element i of board represents the cell (i,j) of the board: 'o' is a cell with a white token, 'x' a cell with a black token, and '.' is an empty cell. At least one cell on the board will be empty.   Jiro's move will consist of adding a single black token to the board. The token must be placed onto an empty cell. Once Jiro places the token, some white tokens will be removed from the board according to the rules described in the next paragraphs.   The white tokens on the board can be divided into connected components using the following rules: If two white tokens lie in cells that share an edge, they belong to the same component. Being in the same component is transitive: if X lies in the same component as Y and Y lies in the same component as Z, then X lies in the same component as Z.   Each component of white tokens is processed separately. For each component of white tokens we check whether it is adjacent to an empty cell. (That is, whether there are two cells that share an edge such that one of them is empty and the other contains a white token from the component we are processing.) If there is such an empty cell, the component is safe and its tokens remain on the board. If there is no such empty cell, the component is killed and all its tokens are removed from the board.   Jiro's score is the total number of white tokens removed from the board after he makes his move. Return the maximal score he can get for the given board.
Definition
    
Class:
FoxAndGo
Method:
maxKill
Parameters:
String[]
Returns:
int
Method signature:
int maxKill(String[] board)
(be sure your method is public)
    

Notes
-
The position described by board does not have to be a valid Go position. In particular, there can already be components of white tokens that have no adjacent empty cell.
-
Please ignore official Go rules. The rules described in the problem statement are slightly different. For example, in this problem the black tokens cannot be killed, and it is allowed to place the black token into any empty cell.
Constraints
-
n will be between 2 and 19, inclusive.
-
board will contain exactly n elements.
-
Each element in board will contain exactly n characters.
-
Each character in board will be 'o', 'x' or '.'.
-
There will be at least one '.' in board.
Examples
0)

    
{".....",
 "..x..",
 ".xox.",
 ".....",
 "....."}
Returns: 1
To kill the only white token, Jiro must place his black token into the cell (3,2). (Both indices are 0-based.)
1)

    
{"ooooo",
 "xxxxo",
 "xxxx.",
 "xxxx.",
 "ooooo"}
Returns: 6
By placing the token to the cell (2,4) Jiro kills 6 white tokens. The other possible move only kills 5 tokens.
2)

    
{".xoxo",
 "ooxox",
 "oooxx",
 "xoxox",
 "oxoox"}
Returns: 13
There is only one possible move. After Jiro makes it, all the white tokens are killed.
3)

    
{".......",
 ".......",
 ".......",
 "xxxx...",
 "ooox...",
 "ooox...",
 "ooox..."}
Returns: 9
Regardless of where Jiro moves, the 9 white tokens in the lower left corner will be killed.
4)

    
{".......",
 ".xxxxx.",
 ".xooox.",
 ".xo.ox.",
 ".xooox.",
 ".xxxxx.",
 "......."}
Returns: 8

5)

    
{"o.xox.o",
 "..xox..",
 "xxxoxxx",
 "ooo.ooo",
 "xxxoxxx",
 "..xox..",
 "o.xox.o"}
Returns: 12

6)

    
{".......",
 "..xx...",
 ".xooox.",
 ".oxxox.",
 ".oxxxo.",
 "...oo..",
 "......."}
Returns: 4

7)

    
{".ox....",
 "xxox...",
 "..xoox.",
 "..xoox.",
 "...xx..",
 ".......",
 "......."}
 
Returns: 5

8)

    
{"...................",
 "...................",
 "...o..........o....",
 "................x..",
 "...............x...",
 "...................",
 "...................",
 "...................",
 "...................",
 "...................",19
 "...................",
 "...................",
 "...................",
 "...................",
 "................o..",
 "..x................",
 "...............x...",
 "...................",
 "..................."}
Returns: 0

This problem statement is the exclusive and proprietary property of TopCoder, Inc. Any unauthorized use or reproduction of this information without the prior written consent of TopCoder, Inc. is strictly prohibited. (c)2003, TopCoder, Inc. All rights reserved.
 * 
*/