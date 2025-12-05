using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string testFilePath = "input/test.txt";
        string inputFilePath = "input/actual.txt";

        string[] input = File.ReadAllLines(inputFilePath);
      
        //Console.WriteLine($"Input: {input.Length}");
        int sum = loopInputs(input);
        Console.WriteLine($"Sum: {sum}");
    }

    static int getLargestVoltage(string input)
    {   
        int max_tens = 0;
        int max_ones = 0;
        int index = 0;
        
        for(int i = 0; i < input.Length - 1; i++)
        {
            int value = Int32.Parse(input[i].ToString());
            if (value > max_tens)
            {
                max_tens = value;
                index = i;
            }
        }

        for(int i = index + 1; i < input.Length; i++)
        {
            
            int value = Int32.Parse(input[i].ToString());
            if (value > max_ones)
            {
                max_ones = value;
            }
        }
        
        //Console.WriteLine($"Input: {input}, Index: {index}");
        //Console.WriteLine($"Max tens: {max_tens}, Max ones: {max_ones}");
        return max_tens * 10 + max_ones;
    }

    static int loopInputs(string[] inputs)
    {
        int sum = 0;

        foreach (string input in inputs)
        {
            int largestVoltage = getLargestVoltage(input);
            sum += largestVoltage;
        }

        return sum;
    }

}