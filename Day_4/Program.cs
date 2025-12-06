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

        int removedTP = removeTP(grid);
        Console.WriteLine($"Total TP removed: {removedTP}");
    }

    static int loopGrid (char[][] grid)
    {
        int total = 0;

        for (int r = 0; r < grid.Length; r++)
        {
            for (int c = 0; c < grid[0].Length; c++)
            {
                // Example operation: count total '@' in the grid
                if (grid[r][c] == '@' || grid[r][c] == 'x')
                {
                    int numNeighbors = getNumNeighbors(grid, r, c);
                    if (numNeighbors < 4)
                    {
                        total++;
                        //remove it from grid now
                        grid[r][c] = 'x';
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
                    if (grid[r][c] == '@' || grid[r][c] == 'x')
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    //function that goes thru the grid and changes 'x' to '.'
    static void cleanGrid(char[][] grid)
    {
        for (int r = 0; r < grid.Length; r++)
        {
            for (int c = 0; c < grid[0].Length; c++)
            {
                if (grid[r][c] == 'x')
                {
                    grid[r][c] = '.';
                }
            }
        }
    }

    static int removeTP (char[][] grid){
        //returns the number of tp we removed.
        //run the loop until no more tp can be removed
        int removedCount = 0;
        while (true)
        {
            int currRemoved = loopGrid(grid);
            cleanGrid(grid);
            printGrid(grid);
            Console.WriteLine();
            if (currRemoved == 0)
            {
                
                return removedCount;
            }
            removedCount += currRemoved;
        }
    }

    //prints grid
    static void printGrid(char[][] grid)
    {
        for (int r = 0; r < grid.Length; r++)
        {
            for (int c = 0; c < grid[0].Length; c++)
            {
                Console.Write(grid[r][c]);
            }
            Console.WriteLine();
        }
    }



}