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

        
        

    }

    static long loopColumns (long[][] numberGraph, char[] operators)
    {
        long total = 0;

        for (int c = 0; c < numberGraph[0].Length; c++)
        {
            long columnTotal = totalColumn(numberGraph, operators, c);
            Console.WriteLine($"Total for column {c}: {columnTotal}");
            total += columnTotal;
        }

        return total;
    }

    static long totalColumn (long[][] numberGraph, char[] operators, int column)
    {
        long total = 0;

        
        for (int r = 0; r < numberGraph.Length; r++)
        {
            char op = operators[column];
            if (op == '+')
            {
                //add them up
                total += numberGraph[r][column];
            } else
            {
                if (total == 0)
                {
                    total = 1;
                }  
                total *= numberGraph[r][column];
            } 
        }
        
        return total;
    }

   
    static void formatGraph (char[][] grid, out long[][] numberGraph, out char[] operators)
    {   
        numberGraph = new long[grid.Length - 1][];
        operators = new char[grid[grid.Length - 1].Length];

        for (int r = 0; r < grid.Length - 1; r++)
        {
            string rowString = new string(grid[r]);
            string[] str = rowString.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            numberGraph[r] = new long[str.Length];

            //Console.WriteLine($"Row {r}: {str}");
            for (int i = 0; i < str.Length; i++)
            {
                numberGraph[r][i] = long.Parse(str[i]);
            }
            
        }

        //get operators from last row
        //make sure to remove extra spaces
        string lastRowString = new string(grid[grid.Length - 1]);
        string[] ops = lastRowString.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ops.Length; i++)
        {
            operators[i] = ops[i][0];
        }
    }

    static void printGrid (string[][] graph)
    {
        for (int r = 0; r < graph.Length; r++)
        {
            for (int c = 0; c < graph[0].Length; c++)
            {
                Console.Write(graph[r][c]);
            }
            Console.WriteLine();
        }
    }
}