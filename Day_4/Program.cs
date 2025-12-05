using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string testFilePath = "input/test.txt";
        string inputFilePath = "input/actual.txt";

        char[][] grid = File.ReadAllLines(inputFilePath)
                            .Select(line => line.ToCharArray())
                            .ToArray();


        int total = loopGrid(grid);
        Console.WriteLine($"Total count from loopGrid: {total}");
    }

    static int loopGrid (char[][] grid)
    {
        int total = 0;

        for (int r = 0; r < grid.Length; r++)
        {
            for (int c = 0; c < grid[0].Length; c++)
            {
                // Example operation: count total '@' in the grid
                if (grid[r][c] == '@')
                {
                    int numNeighbors = getNumNeighbors(grid, r, c);
                    if (numNeighbors < 4)
                    {
                        total++;
                    }
                }
            }
        }
        return total;
    }

    static int getNumNeighbors (char[][] grid, int row, int col)
    {
        int numRows = grid.Length;
        int numCols = grid[0].Length;
        int count = 0;

        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                if (r >= 0 && r < numRows && c >= 0 && c < numCols)
                {
                    if (r == row && c == col)
                    {
                        continue; // Skip the cell itself
                    }
                    if (grid[r][c] == '@')
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

}