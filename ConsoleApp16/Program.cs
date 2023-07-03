
using System;
public class Sudoku
{
    private char[][] board;
    private int stepsCount;

    public Sudoku()
    {
        board = new char[9][];

        Console.WriteLine("Enter Sudoku table:");

        for (int i = 0; i < 9; i++)
        {
            string line = Console.ReadLine();
            board[i] = line.PadRight(9).Substring(0, 9).ToCharArray();
            for (int j = 0; j < 9; j++)
            {
                if (board[i][j] < '0' || board[i][j] > '9')
                {
                    board[i][j] = '.';
                }
            }
        }

        stepsCount = 0;
    }

    public char[][] Board
    {
        get { return board; }
        set { board = value; }
    }

    public int StepsCount
    {
        get { return stepsCount; }
        set { stepsCount = value; }
    }

    public void PrintTable()
    {
        Console.WriteLine();
        Console.WriteLine("Solved table after {0} steps:", stepsCount);
        for (int i = 0; i < 9; i++)
        {
            Console.WriteLine("{0}", new string(board[i]));
        }
    }

    public char[] GetCandidates(int row, int col)
    {
        string s = "";

        for (char c = '1'; c <= '9'; c++)
        {
            bool collision = false;

            for (int i = 0; i < 9; i++)
            {
                if (board[row][i] == c || board[i][col] == c || board[(row - row % 3) + i / 3][(col - col % 3) + i % 3] == c)
                {
                    collision = true;
                    break;
                }
            }

            if (!collision)
            {
                s += c;
            }
        }

        return s.ToCharArray();
    }

    public bool Solve()
    {
        bool solved = false;

        int row = -1;
        int col = -1;
        char[] candidates = null;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i][j] == '.')
                {
                    char[] newCandidates = GetCandidates(i, j);
                    if (row < 0 || newCandidates.Length < candidates.Length)
                    {
                        row = i;
                        col = j;
                        candidates = newCandidates;
                    }
                }
            }
        }

        if (row < 0)
        {
            solved = true;
        }
        else
        {
            for (int i = 0; i < candidates.Length; i++)
            {
                board[row][col] = candidates[i];
                stepsCount++;
                if (Solve())
                {
                    solved = true;
                    break;
                }
                board[row][col] = '.';
            }
        }

        return solved;
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Sudoku sudoku = new Sudoku();

            if (sudoku.Solve())
            {
                sudoku.PrintTable();
            }
            else
            {
                Console.WriteLine("Could not solve this Sudoku.");
            }

            Console.WriteLine();
            Console.Write("More? (y/n) ");
            if (Console.ReadLine().ToLower() != "y")
            {
                break;
            }
        }
    }
}
