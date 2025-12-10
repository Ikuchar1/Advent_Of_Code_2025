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
        formatCoordinates(input, out (int, int, int)[] coordList);         

        //At the start each coord is in its own circuit
        //loop this ten times
        //find the closest 2 coords (make sure they arent in same circuit
        //connect them (merge circuits)

        //use a dictionary to map coord to their circuit id
        Dictionary<(int, int, int), int> circuits = new Dictionary<(int, int, int), int>();
        for (int i = 0; i < coordList.Length; i++)
        {
            circuits[coordList[i]] = i;
        }

        //connect 10 closest coordinates
        printCircuits(circuits);
        //connectClosestCoordinates(coordList, circuits);

        connectClosestCoordinates(coordList, circuits);

        //printCircuits(circuits);
        //printCircuits(circuits);

        //long product = find_3_largest(circuits);
        //Console.WriteLine($"Product of 3 largest circuits: {product}");
    }

    //function that will find the 3 largest circuits and print their sizes
    static long find_3_largest (Dictionary<(int, int, int), int> circuits)
    {
        //convert dict to list of lists, where each inner list is a circuit
        List<List<(int, int, int)>> circuitLists = new List<List<(int, int, int)>>();

        foreach (var kvp in circuits)
        {
            int circuitId = kvp.Value;
            (int, int, int) coord = kvp.Key;

            //check if circuitId already has a list
            while (circuitLists.Count <= circuitId)
            {
                circuitLists.Add(new List<(int, int, int)>());
            }

            circuitLists[circuitId].Add(coord);
        }
        //get the sizes of the circuits and sort them
        List<int> sizes = circuitLists.Select(circuit => circuit.Count).ToList();
        sizes.Sort();
        sizes.Reverse();

        //get the product of the 3 largest sizes
        long product = 1;
        for (int i = 0; i < Math.Min(3, sizes.Count); i++)
        {
            product *= sizes[i];
        }

        return product;
    }


    static void printCircuits(Dictionary<(int, int, int), int> circuits)
    {
        //print all the contents of the dictionary
        foreach (var kvp in circuits)
        {
            Console.WriteLine($"Coord: ({kvp.Key.Item1}, {kvp.Key.Item2}, {kvp.Key.Item3}) - Circuit ID: {kvp.Value}");
        }   
    }

    //function that will loop 10 times, each time finding the closest 2 coords and connecting them
    static void connectClosestCoordinates((int, int, int)[] coordList, Dictionary<(int, int, int), int> circuits)
    {
        HashSet<string> pairs = new HashSet<string>();
        int limit = 7500;

        for (int i = 0; i < limit; i++)
        {
            getMinDistance(coordList, pairs, out (int, int, int) c1, out (int, int, int) c2);

            string key = GetPairKey(c1, c2);
            pairs.Add(key);

            int id1 = circuits[c1];
            int id2 = circuits[c2];

            //if not the same circuit merge them
            if (id1 != id2)
            {
                Console.WriteLine($"Step {1+i}: Merging Circuits");
                mergeCircuits(c1, c2, circuits);

                //go until all coords are in same circuit
                if (allCoordsInSameCircuit(circuits))
                {
                    Console.WriteLine("All coordinates are now in the same circuit.");
                    //print the last 2 merged coords
                    Console.WriteLine($"Last merged coordinates: ({c1.Item1}, {c1.Item2}, {c1.Item3}) and ({c2.Item1}, {c2.Item2}, {c2.Item3})");
                    long product = (long)c1.Item1 * c2.Item1;
                    Console.WriteLine($"Product of X coordinates: {product}");
                    break;
                }
            }
            else
            {
                Console.WriteLine($"Step {1+i}: Already in same circuit.");
            }
        }

    }

    //see if all coords are in the same circuit
    static bool allCoordsInSameCircuit(Dictionary<(int, int, int), int> circuits)
    {
        if (circuits.Count == 0) return true;

        int firstCircuitId = circuits.Values.First();
        return circuits.Values.All(id => id == firstCircuitId);
    }

     static long getMinDistance((int, int, int)[] coordList, HashSet<string> processedPairs, out (int, int, int) minCoord1, out (int, int, int) minCoord2)
    {
        long minDistance = long.MaxValue;
        minCoord1 = (0, 0, 0);
        minCoord2 = (0, 0, 0);

        //go thru each coord, calculate distance to each other coord
        //check to see if they are in the same circuit, if not calculate distance
        for (int i = 0; i < coordList.Length; i++)
        {
            for (int j = i + 1; j < coordList.Length; j++)
            {
                string key = GetPairKey(coordList[i], coordList[j]);

                if (!processedPairs.Contains(key))
                {
                    long distance = calculateDistanceSquared(coordList[i], coordList[j]);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minCoord1 = coordList[i];
                        minCoord2 = coordList[j];
                    }
                }
            }
        }

        return minDistance;
    }

    static string GetPairKey((int, int, int) c1, (int, int, int) c2)
    {
        // Ensure the key is always the same regardless of order (A,B vs B,A)
        // We can just sort them by their X value or hash code logic, 
        // or just pick a consistent order for the string.
        
        // Simple way: Compare string representations to decide order
        string s1 = $"{c1.Item1},{c1.Item2},{c1.Item3}";
        string s2 = $"{c2.Item1},{c2.Item2},{c2.Item3}";

        if (string.Compare(s1, s2) < 0)
            return s1 + "|" + s2;
        else
            return s2 + "|" + s1;
    }

    //function that will merge two circuits containing coord1 and coord2
    static void mergeCircuits((int, int, int) coord1, (int, int, int) coord2, Dictionary<(int, int, int), int> circuits)
    {
        int circuitId1 = circuits[coord1];
        int circuitId2 = circuits[coord2];

        //update all coords in circuitId2 to circuitId1
        foreach (var key in circuits.Keys.ToList())
        {
            if (circuits[key] == circuitId2)
            {
                circuits[key] = circuitId1;
            }
        }
    }

   

    static long calculateDistance ((int, int, int) coord1, (int, int, int) coord2)
    {
        //get straight line distance between two coords in 3D space
        long dx = coord2.Item1 - coord1.Item1;
        long dy = coord2.Item2 - coord1.Item2;
        long dz = coord2.Item3 - coord1.Item3;
        long distance = (long)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        return distance;
    }

    static long calculateDistanceSquared((int, int, int) coord1, (int, int, int) coord2)
    {
    long dx = coord2.Item1 - coord1.Item1;
    long dy = coord2.Item2 - coord1.Item2;
    long dz = coord2.Item3 - coord1.Item3;
    return (dx * dx) + (dy * dy) + (dz * dz);
    }

    static void formatCoordinates (string[] input, out (int, int, int)[] coordList)
    {
        coordList = new (int, int, int)[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            string[] coords = input[i].Split(',');
            int x = Int32.Parse(coords[0]);
            int y = Int32.Parse(coords[1]);
            int z = Int32.Parse(coords[2]);
            coordList[i] = (x, y, z);
        }
    }


}