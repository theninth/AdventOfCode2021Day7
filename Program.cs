// Niclas Nilsson, 2022
// This program needs at least C# 7.0 for it's tuple syntax.

namespace TheTreacheryOfWhales;

/// <summary>
/// Main class of project.
/// </summary>
public static class Program
{
    /// <summary>
    /// A list where each item is a crab and the value it is initial horizontal position.
    /// </summary>
    private static readonly List<Int32> Crabs = new();

    /// <summary>
    /// Calculates number of moves between two positions.
    /// </summary>
    /// <param name="fromPos">Position A</param>
    /// <param name="toPos">Position B</param>
    /// <returns>A positive (or zero) value of number of steps for that move.</returns>
    private static int CalcNoOfMovesToPos(int fromPos, int toPos) => Math.Abs(fromPos - toPos);

    /// <summary>
    /// Calculate number of moves for each of the crabs to get to the right position.
    /// </summary>
    /// <param name="toPos">Target position.</param>
    /// <returns>
    /// An array where each element is number of moves for the crab with the same index in the
    /// Position-list.
    /// </returns>
    private static int[] CalcNoOfMovesForPos(int toPos)
    {
        int noOfCrabs = Crabs.Count();
        int[] noOfMoves = new int[noOfCrabs];

        for (int crabIdx = 0; crabIdx < noOfCrabs; crabIdx++)
        {
            int fromPos = Crabs[crabIdx];
            noOfMoves[crabIdx] = CalcNoOfMovesToPos(fromPos, toPos);
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
    private static (int, int[]) CalcBestPosition()
    {
        // Creates an array for all reasonable positions. I.e. from the position of the
        // lowest positioned crab to the highest positioned.
        int[][] reasonablePositions = new int[Crabs.Max() - Crabs.Min() + 1][];

        for (int i = 0; i < reasonablePositions.Length; i++)
        {
            reasonablePositions[i] = CalcNoOfMovesForPos(Crabs.Min() + i);
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
    /// Make user input crab positions and add those to the Positions list.
    /// </summary>
    private static void InputData()
    {
        int count = 1;

        while (true)
        {
            Console.Write($"The original position of crab {count} (just press enter when no more crabs to input): ");
            string? input = Console.ReadLine();

            if (input == null || input.Trim() == string.Empty)
            {
                if (count <= 2)
                {
                    Console.WriteLine("You have to enter at least two valid positions!");
                    Console.WriteLine();
                    continue;
                }
                return;
            }
            
            if (int.TryParse(input.Trim(), out int origPos))
            {
                Crabs.Add(origPos);
                count++;
            }
            else
            {
                Console.WriteLine("Error: Not a valid value.");
            }
        }
    }

    /// <summary>
    /// Outputs data to screen.
    /// </summary>
    /// <param name="targetPos">Target position.</param>
    /// <param name="noOfMoves">Array with each crabs number of moves to get to target.</param>
    private static void OutputToScreen(int targetPos, int[] noOfMoves)
    {
        Console.WriteLine();
        Console.WriteLine($"Best position:      {targetPos, 3}");
        Console.WriteLine($"Total fuels needed: {noOfMoves.Sum(), 3}");
        Console.WriteLine();

        for (int i = 0; i < noOfMoves.Length; i++)
        {
            Console.WriteLine($"  - Move from {Crabs[i]} to {targetPos}: {noOfMoves[i]} fuel");
        }
        
    }

    /// <summary>
    /// Main method.
    /// </summary>
    public static void Main()
    {
        InputData();
        (int bestPos, int[] bestMoves) = CalcBestPosition();
        OutputToScreen(bestPos, bestMoves);

        Console.ReadLine();
    }
}