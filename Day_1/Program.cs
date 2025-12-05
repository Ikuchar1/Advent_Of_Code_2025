using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string testFilePath = "input/test.txt";
        string inputFilePath = "input/actual.txt";

        string[] inputs = File.ReadAllLines(inputFilePath);
        int startingValue = 50;

        Console.WriteLine($"Number of Lines: {inputs.Length}");

        int result = CrackTheCode(inputs, startingValue);
        Console.WriteLine($"The count is: {result}");
    }

    static int CrackTheCode(string[] inputs, int startingValue)
    {
        int currentValue = startingValue;
        int count = 0;

        foreach (string input in inputs)
        {
            if (input[0] == 'L')
            {
                int turnNumber = int.Parse(input.Substring(1));
                currentValue = TurnLeft(currentValue, turnNumber, ref count);
            } else if (input[0] == 'R')
            {
                int turnNumber = int.Parse(input.Substring(1));
                currentValue = TurnRight(currentValue, turnNumber, ref count);
            }

            Console.WriteLine($"Current Value: {currentValue} after instruction: {input}");
        }

        return count;
    }

    //if left take current value - turn number, check make sure it is greater than 0, if not while loop adding 100 until it is
    static int TurnLeft(int currentValue, int turnNumber, ref int count)
    {
        if (turnNumber >= 100)
        {   
            int overturn = turnNumber / 100;
            count += overturn;
            turnNumber = turnNumber % 100;
        }

        if (turnNumber >= currentValue)
        {
            if (currentValue != 0)
            {
                count++;
                Console.WriteLine($"Incrementing count to: {count}");
            }
            currentValue = (100 - turnNumber) + currentValue;
        } else
        {
            currentValue -= turnNumber;
        }

        //under 0
        if (currentValue < 0)
        {
            currentValue = 100 + currentValue;
        }

        if (currentValue == 100)
        {
            currentValue = 0;
        }
        
        return currentValue;
    }


    //if right and take current value + turn number, check make sure it is less than 100, if not while loop subtracting 100 until it is
    static int TurnRight(int currentValue, int turnNumber, ref int count)
    {
       if (turnNumber >= 100)
        {   
            int overturn = turnNumber / 100;
            count += overturn;
            turnNumber = turnNumber % 100;
        }

        if ((turnNumber + currentValue) >= 100)
        {
            currentValue = currentValue - (100 - turnNumber);
            count++;
            Console.WriteLine($"Incrementing count to: {count}");

        } else
        {
            currentValue += turnNumber;
        }

        //>= 100 check
        if (currentValue >= 100)
        {
            currentValue = currentValue - 100;
        }
        
        return currentValue;
    }
}
