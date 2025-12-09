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

    
        string[] input = File.ReadAllLines(inputFilePath);
        
        
        Console.WriteLine($"Input: {input.Length}");

        
        formatCoordinates(input, out (int, int)[] coordList);

        foreach (var coord in coordList)
        {
            Console.WriteLine($"X: {coord.Item1}, Y: {coord.Item2}");
        }

        //char[][] grid = createGrid(coordList);

        //printGrid(grid);

        long maxArea = getMaxArea(coordList, out (int, int) maxCoord1, out (int, int) maxCoord2);
        Console.WriteLine($"Max Area: {maxArea}");
        Console.WriteLine($"Max Area Coordinates: ({maxCoord1.Item1}, {maxCoord1.Item2}) and ({maxCoord2.Item1}, {maxCoord2.Item2})");
    }

    static long getMaxArea ((int, int)[] coordList, out (int, int) coord1, out (int, int) coord2)
    {
        long maxArea = 0;
        coord1 = (0, 0);
        coord2 = (0, 0);

        //go thru each coord, calculate area of it with each other coord. coords will be opposite corners of rectangle
        for (int i = 0; i < coordList.Length; i++)
        {
            for (int j = 0; j < coordList.Length; j++)
            {
                if (i != j)
                {
                    long x1 = coordList[i].Item1;
                    long y1 = coordList[i].Item2;
                    long x2 = coordList[j].Item1;
                    long y2 = coordList[j].Item2;

                    long area = Math.Abs(x2 - x1 + 1) * Math.Abs(y2 - y1 + 1);
                    if (area > maxArea)
                    {
                        maxArea = area;
                        coord1 = coordList[i];
                        coord2 = coordList[j];
                    }
                }
            }
        }

        return maxArea;
    }

    static char[][] createGrid((int, int)[] coordList)
    {
        //find max x and y. make grid 1 larger than both maxes
        int maxX = 0;
        int maxY = 0;

        foreach (var coord in coordList)
        {
            if (coord.Item1 > maxX)
            {
                maxX = coord.Item1;
            }
            if (coord.Item2 > maxY)
            {
                maxY = coord.Item2;
            }
        }
        maxX += 2;
        maxY += 2;

        char[][] grid = new char[maxY][];

        for (int i = 0; i < maxY; i++)
        {
            grid[i] = new char[maxX];
            for (int j = 0; j < maxX; j++)
            {
                grid[i][j] = '.';
            }
        }

        foreach (var coord in coordList)
        {
            grid[coord.Item2][coord.Item1] = '#'; 
        }

        return grid;
    }

    static void formatCoordinates(string[] input, out (int, int)[] coordList)
    {
        int x = 0;
        int y = 0;

        coordList = new (int, int)[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            string[] parts = line.Split(',');
            x = Int32.Parse(parts[0]);
            y = Int32.Parse(parts[1]);
            coordList[i] = (x, y);
        }

        // foreach (string line in input)
        // {
        //     string[] parts = line.Split(',');
        //     x = Int32.Parse(parts[0]);
        //     y = Int32.Parse(parts[1]);
        // }
    }

    static void printGrid(char[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                Console.Write(grid[i][j]);
            }
            Console.WriteLine();
        }
    }



}