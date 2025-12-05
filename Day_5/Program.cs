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
        string testFilePath2 = "input/test_1.txt";

        string[] input = File.ReadAllLines(inputFilePath);

        splitInput(input, out string[] ranges, out string[] values);
        splitRanges(ranges, out (long, long)[] limits);
                            
        //int val = checkFreshness(values, limits);
        //Console.WriteLine($"value: {val}");

        //int val = findValidValues(limits);
        //Console.WriteLine($"value: {val}");

        for (int i = 0; i < limits.Length; i++)
        {
            Console.WriteLine($"L:{limits[i].Item1} H:{limits[i].Item2}");
        }

        SortRanges(limits);
        Console.WriteLine("After Sorting:");
        fixRanges(limits);
        Console.WriteLine("");

        for (int i = 0; i < limits.Length; i++)
        {
            Console.WriteLine($"L:{limits[i].Item1} H:{limits[i].Item2}");
        }


        long value = sumRanges(limits);
        Console.WriteLine($"val:{value}");


    }

    static void SortRanges((long, long)[] ranges)
    {
        Array.Sort(ranges, (a, b) => a.Item1.CompareTo(b.Item1));
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

    //part 2
    static int findValidValues((long, long)[] limits)
    {
        HashSet<long> valueSet = new HashSet<long>();


        for (int i = 0; i < limits.Length; i++)
        {
            long lower = limits[i].Item1;
            long upper = limits[i].Item2;

            for (long j = lower; j <= upper; j++)
            {
                //add to set
                Console.WriteLine($"Adding {j}");
                valueSet.Add(j);
            }
        }

        return valueSet.Count;
    }

    static void fixRanges((long, long)[] ranges)
    {
        for (int i = 0; i < ranges.Length; i++)
        {
            for (int j = i + 1; j < ranges.Length; j++)
            {
                long a1 = ranges[i].Item1;
                long a2 = ranges[i].Item2;
                long b1 = ranges[j].Item1;
                long b2 = ranges[j].Item2;

                //if 1.2 >= 2.1 set 1.2 to 2.1-1
                if (a2 >= b1)
                {
                    //check to see if a2 is >= b2. 
                    //0-10 and 5-8
                    //0-4 and 5-10
                    if (a2 >= b2)
                    {
                        ranges[j].Item2 = ranges[i].Item2;

                    }
                    ranges[i].Item2 = ranges[j].Item1 - 1;
                }
            }
        }
    }

    static long sumRanges((long, long)[] ranges)
    {       
        long sum = 0;

        for (int i = 0; i < ranges.Length; i++)
        {
            long a1 = ranges[i].Item1;
            long a2 = ranges[i].Item2;

            if (a1 <= a2)
            {
                sum = sum + (a2 - a1 + 1);
            }
            

        }

        return sum;
    }

}