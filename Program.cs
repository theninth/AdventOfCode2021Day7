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
    /// A list where each item is a crab and the value it is initial horizontal position.
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
    private static int CalcFuelToPosPart1(int fromPos, int toPos) => Math.Abs(fromPos - toPos);

    /// <summary>
    /// Calculates fuel needed to move between two positions for part 2 of assignment.
    /// </summary>
    /// <param name="fromPos">Position A</param>
    /// <param name="toPos">Position B</param>
    /// <returns>A positive (or zero) value of fuel needed for that move.</returns>
    private static int CalcFuelToPosPart2(int fromPos, int toPos)
    {
        int noOfMoves = Math.Abs(fromPos - toPos);
        return noOfMoves * (noOfMoves + 1) / 2;
    }

    /// <summary>
    /// Calculate number of moves for each of the crabs to get to the right position.
    /// </summary>
    /// <param name="toPos">Target position.</param>
    /// <param name="fuelCalculator">Algorithm to calculate fuel needed between two </param>
    /// <returns>
    /// An array where each element is number of moves for the crab with the same index in the
    /// Crabs-list.
    /// </returns>
    private static int[] CalcNoOfMovesForCrabs(int toPos, FuelCalculator fuelCalculator)
    {
        int noOfCrabs = Crabs.Count();
        int[] noOfMoves = new int[noOfCrabs];

        for (int crabIdx = 0; crabIdx < noOfCrabs; crabIdx++)
        {
            int fromPos = Crabs[crabIdx];
            noOfMoves[crabIdx] = fuelCalculator(fromPos, toPos);
        }

        return noOfMoves;
    }

    /// <summary>
    /// Calculate best position and number of moves each crab has to make.
    /// </summary>
    /// <returns>
    /// A tuple with:
    ///   - item1: Best position to move to.
    ///   - item2: Moves for each crab to get there.
    /// </returns>
    private static (int, int[]) CalcBestPosition(FuelCalculator fuelCalculator)
    {
        // Creates an array for all reasonable positions. I.e. from the position of the
        // lowest positioned crab to the highest positioned.
        // The array will have the form of {{a, b, c ...}, {a, b, c ...}, {a, b, c ...}}
        // where each sub array represents a position on the horizontal axis and each
        // letter is how many moves the crab of that index needs to take.
        int[][] reasonablePositions = new int[Crabs.Max() - Crabs.Min() + 1][];

        for (int i = 0; i < reasonablePositions.Length; i++)
        {
            reasonablePositions[i] = CalcNoOfMovesForCrabs(Crabs.Min() + i, fuelCalculator);
        }

        // Find out which of those positions have the lowest number of crab moves.
        int BestIdx = 0;
        int bestSum = Int32.MaxValue;

        for (int i = 0; i < reasonablePositions.Length; i++)
        {
            int totalNoOfMoves = reasonablePositions[i].Sum();
            if (totalNoOfMoves < bestSum)
            {
                bestSum = totalNoOfMoves;
                BestIdx = i;
            }
        }

        int bestPos = Crabs.Min() + BestIdx;
        int[] bestMoves = reasonablePositions[BestIdx];
        return (bestPos, bestMoves);
    }

    /// <summary>
    /// Read in values from datafile supplied with assignment (might not be the same as original).
    /// </summary>
    private static void InputData()
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
    /// <param name="targetPos">Target position.</param>
    /// <param name="noOfMoves">Array with each crabs number of moves to get to target.</param>
    /// <param name="verbose">If each move should be outputted</param>
    private static void OutputToScreen(int targetPos, int[] noOfMoves, bool verbose=false)
    {
        if (verbose)
        {
            Console.WriteLine();
            for (int i = 0; i < noOfMoves.Length; i++)
            {
                Console.WriteLine($"  - Move from {Crabs[i]} to {targetPos}: {noOfMoves[i]} fuel");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Best position:      {targetPos,3}");
        Console.WriteLine($"Total fuels needed: {noOfMoves.Sum(),3}");
        Console.WriteLine();
    }

    /// <summary>
    /// Main method.
    /// </summary>
    public static void Main(string[] args)
    {
        bool verbose = args.Contains("--verbose");

        InputData();
        (int bestPos, int[] bestMoves) = CalcBestPosition(CalcFuelToPosPart1);
        // (int bestPos, int[] bestMoves) = CalcBestPosition(CalcFuelToPosPart2);
        OutputToScreen(bestPos, bestMoves, verbose);

        Console.ReadLine();
    }
}
