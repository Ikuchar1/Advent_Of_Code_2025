using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string testFilePath = "input/test.txt";
        string inputFilePath = "input/actual.txt";

        string[] input = File.ReadAllLines(inputFilePath);

        splitInput(input, out string[] ranges, out string[] values);
        splitRanges(ranges, out (long, long)[] limits);
                            
        int val = checkFreshness(values, limits);
        Console.WriteLine($"value: {val}");
    }

    static void splitInput(string[] input, out string[] ranges, out string[] values)
    {
        //find where the blank line is
        //lines before are ranges, lines after are values
        int blankLineIndex = Array.IndexOf(input, "");
        ranges = input.Take(blankLineIndex).ToArray();
        values = input.Skip(blankLineIndex + 1).ToArray();
    }

    //split ranges into lower and upper limits
    static void  splitRanges(string[] ranges, out (long, long)[] limits)
    {
        limits = new (long, long)[ranges.Length];

        for (int i = 0; i < ranges.Length; i++)
        {
            string[] upper_lower_parts = ranges[i].Split('-');
            long lower = Int64.Parse(upper_lower_parts[0]);
            long upper = Int64.Parse(upper_lower_parts[1]);
            limits[i] = (lower, upper);
        }
    }

    static int checkFreshness(string[] values, (long, long)[] limits)
    {
        int count = 0;

        for (int i = 0; i < values.Length; i++)
        {
            long value = Int64.Parse(values[i]);

            for (int j = 0; j < limits.Length; j++)
            {
                //Console.WriteLine($"Value: {value}, Lower: {limits[j].Item1}, Higher: {limits[j].Item2}");

                //go until it is larger than the bottom limit
                //if it is smaller than upper limit add one to count
                if (value >= limits[j].Item1)
                {
                    if (value <= limits[j].Item2)
                    {
                        count++;
                        //Console.WriteLine($"Count increased to {count}");
                        break;
                    } else
                    {
                        //it is above the upper limit so it is not fresh
                        continue;
                    }
                }
            }
        }

        return count;
    }

}