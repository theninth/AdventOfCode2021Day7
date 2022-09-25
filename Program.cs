// Niclas Nilsson, 2022
// This program needs at least C# 7.0 for it's tuple syntax.

namespace AdventOfCode2021Day7;

/// <summary>
/// Main class of project.
/// </summary>
public static class Program
{
    /// <summary>
    /// Filename of datafile.
    /// </summary>
    private const string DataFile = "SampleData.txt";

    /// <summary>
    /// A list where each item is a crab and the value is it's initial horizontal position.
    /// </summary>
    private static readonly List<Int32> Crabs = new();

    /// <summary>
    /// Calculates fuel needed to move between two positions.
    /// Used  because of different algorithm in part 1 and part 2.
    /// </summary>
    /// <param name="fromPos">Position A</param>
    /// <param name="toPos">Position B</param>
    /// <returns>A positive (or zero) value of fuel needed for that move.</returns>
    public delegate int FuelCalculator(int fromPos, int toPos);

    /// <summary>
    /// Calculates fuel needed to move between two positions for part 1 of assignment.
    /// </summary>
    /// <param name="fromPos">Position A</param>
    /// <param name="toPos">Position B</param>
    /// <returns>A positive (or zero) value of fuel needed for that move.</returns>
    private static int FuelCalculatorPart1(int fromPos, int toPos) => Math.Abs(fromPos - toPos);

    /// <summary>
    /// Calculates fuel needed to move between two positions for part 2 of assignment.
    /// </summary>
    /// <param name="fromPos">Position A</param>
    /// <param name="toPos">Position B</param>
    /// <returns>A positive (or zero) value of fuel needed for that move.</returns>
    private static int FuelCalculatorPart2(int fromPos, int toPos)
    {
        int noOfMoves = Math.Abs(fromPos - toPos);
        return noOfMoves * (noOfMoves + 1) / 2;
    }

    /// <summary>
    /// Calculate fuel needed for each of the crabs to get to the right position.
    /// </summary>
    /// <param name="toPos">Target position.</param>
    /// <param name="fuelCalculator">Algorithm to calculate fuel burnt between two positions.</param>
    /// <returns>
    /// An array where each element is the fuel the crab with the same index in the 
    /// Crabs-list needs to get to target position.
    /// </returns>
    private static int[] CalcFuelForEachCrab(int toPos, FuelCalculator fuelCalculator)
    {
        int noOfCrabs = Crabs.Count();
        int[] fuelForEachCrab = new int[noOfCrabs];

        for (int crabIdx = 0; crabIdx < noOfCrabs; crabIdx++)
        {
            int fromPos = Crabs[crabIdx];
            fuelForEachCrab[crabIdx] = fuelCalculator(fromPos, toPos);
        }

        return fuelForEachCrab;
    }

    /// <summary>
    /// Calculate best position and fuel needed for each crab to get there. This method is
    /// a little complicated because it needs to retain fuel burnt for each crab to be able
    /// to output this in verbose mode.
    /// </summary>
    /// <returns>
    /// A tuple with:
    ///   - item1: Best position to move to.
    ///   - item2: Fuel needed for each crab to get there.
    /// </returns>
    private static (int, int[]) CalcBestPosition(FuelCalculator fuelCalculator)
    {
        // Creates an array for all reasonable positions. I.e. from the position of the
        // lowest positioned crab to the highest positioned.
        // The array will have the form of {{a, b, c ...}, {a, b, c ...}, {a, b, c ...}}
        // where each sub array represents a position on the horizontal axis and each
        // letter is fuel burnt for the crab of that index to get to that position.
        int[][] reasonablePositions = new int[Crabs.Max() - Crabs.Min() + 1][];

        for (int i = 0; i < reasonablePositions.Length; i++)
        {
            reasonablePositions[i] = CalcFuelForEachCrab(Crabs.Min() + i, fuelCalculator);
        }

        // Find out which of those positions have the lowest number of crab moves.
        int bestIdx = 0;
        int bestFuel = Int32.MaxValue;

        for (int i = 0; i < reasonablePositions.Length; i++)
        {
            int totalFuel = reasonablePositions[i].Sum();
            if (totalFuel < bestFuel)
            {
                bestFuel = totalFuel;
                bestIdx = i;
            }
        }

        int pos = Crabs.Min() + bestIdx;
        int[] crabsFule = reasonablePositions[bestIdx];
        return (pos, crabsFule);
    }

    /// <summary>
    /// Read in values from datafile supplied with assignment (might not be the same as original).
    /// </summary>
    private static void ReadData()
    {
        // Really quick and ugly way to read data file.
        string textValues = File.ReadAllText(DataFile).Trim('{').Trim('}');

        foreach (string textValue in textValues.Split(','))
        {
            if (int.TryParse(textValue.Trim(), out int number))
            {
                Crabs.Add(number);
            }
            else
            {
                Console.WriteLine($"Error: Could not parse value \"{textValue.Trim()}\"");
            }
        }
    }

    /// <summary>
    /// Outputs data to screen.
    /// </summary>
    /// <param name="pos">Target position.</param>
    /// <param name="crabsFuel">Array with each crabs fuel needed to burn to get to target.</param>
    /// <param name="verbose">Output debug data with each move.</param>
    private static void OutputToScreen(int pos, int[] crabsFuel, bool verbose=false)
    {
        if (verbose)
        {
            Console.WriteLine();
            for (int i = 0; i < crabsFuel.Length; i++)
            {
                Console.WriteLine($"  - Move from {Crabs[i]} to {pos}: {crabsFuel[i]} fuel");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Best position:      {pos,3}");
        Console.WriteLine($"Total fuels needed: {crabsFuel.Sum(),3}");
        Console.WriteLine();
    }

    /// <summary>
    /// Main method.
    /// </summary>
    public static void Main(string[] args)
    {
        bool verbose = args.Contains("--verbose");

        ReadData();
        (int bestPos, int[] bestMoves) = CalcBestPosition(FuelCalculatorPart1);
        // (int bestPos, int[] bestMoves) = CalcBestPosition(FuelCalculatorPart2);
        OutputToScreen(bestPos, bestMoves, verbose);

        Console.ReadLine();
    }
}
