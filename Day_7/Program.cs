using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string testFilePath = "input/test.txt";
        string inputFilePath = "input/actual.txt";

        char[][] grid = File.ReadAllLines(inputFilePath)
                            .Select(line => line.ToCharArray())
                            .ToArray();

        
        

        //int numSplits = getNumSplits(grid);
        //Console.WriteLine($"Number of splits: {numSplits}");
        long timelineCount = getTimelineCount(grid);
        Console.WriteLine($"Timeline Count: {timelineCount}");

    }

    static long getTimelineCount(char[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        long[][] counts = new long[rows + 1][];
        for (int i = 0; i <= rows; i++)
        {
            counts[i] = new long[cols];
        }

        //get s from first row
        for (int j = 0; j < cols; j++)
        {
            if (grid[0][j] == 'S')
            {
                counts[0][j] = 1;
            }
        }

        //start counting thru rest of rows
        long totalSplits = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                long tileVolume = counts[r][c];
                char tileChar = grid[r][c];

                //if there is no volume, skip
                if (tileVolume == 0)
                {
                    continue;
                }

                if (tileChar == '^')
                {
                    totalSplits += tileVolume;

                    //split volume to left and right
                    if (c - 1 >= 0)
                    {
                        counts[r + 1][c - 1] += tileVolume;
                    }
                    if (c + 1 < cols)
                    {
                        counts[r + 1][c + 1] += tileVolume;
                    }
                }
                else
                {
                    //push volume down
                    counts[r + 1][c] += tileVolume;
                }
            }
        }

        return totalSplits + 1;

    }

    static int getNumSplits(char[][] grid)
    {
        int count = 0;

        //go thru each row
        //check each column in the row
        //if char is 'S', set column in the row below to '|'
        //if char is '.', check to see if row above has '|' in the same column
        //if so, set current char to '|'
        //if char is '^', check row above to see if there is '|' in the same column
        //if so, increment count, then in the same row set char in the column to the left and right to '|'
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                char currentChar = grid[i][j];

                if (currentChar == 'S')
                {
                    if (i + 1 < grid.Length)
                    {
                        grid[i + 1][j] = '|';
                    }
                }
                else if (currentChar == '.')
                {
                    //bring stream down on empty space
                    if (i - 1 >= 0 && grid[i-1][j] == '|')
                    {
                        grid[i][j] = '|';
                    }
                }
                else if (currentChar == '^')
                {
                    //split the stream
                    if (i - 1 >= 0 && grid[i-1][j] == '|')
                    {
                        //check left
                        if (j - 1 >= 0)
                        {
                            //make sure left isnt a splitter
                            if (grid[i][j-1] != '^')
                            {
                                grid[i][j-1] = '|';
                            }
                            
                        }

                        //check right
                        if (j + 1 < grid[i].Length)
                        {
                            if (grid[i][j+1] != '^')
                            {
                                grid[i][j+1] = '|';
                            }
                        }

                        count++;
                    }
                }

            }
        }
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                Console.Write(grid[i][j]);
            }
            Console.WriteLine();
        }
        return count;
    }

}