using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string testFilePath = "input/test.txt";
        string inputFilePath = "input/actual.txt";

        string input = File.ReadAllText(inputFilePath);
        string[] inputs = splitInput(input);
        long invalidSum = getInvalidSum(inputs);

        Console.WriteLine($"Invalid Sum: {invalidSum}");

    }

    static string[] splitInput (string str)
    {
        string[] inputs = str.Split(',');
        return inputs;
    }

    static long getInvalidSum(string[] inputs)
    {
        long sum = 0;

        foreach (string input in inputs)
        {
            string[] limits = input.Split('-');
            long lowerLimit = Int64.Parse(limits[0]);
            long upperLimit = Int64.Parse(limits[1]);

            for (long i = lowerLimit; i <= upperLimit; i++)
            {
                string i_str = i.ToString();
                int len = i_str.Length;
                if (len % 2 == 0)
                {
                    if (i_str.Substring(0, len / 2) == i_str.Substring(len/2))
                    {
                        sum += i;
                    }
                    
                }
            }
        }

        return sum;
    }

}