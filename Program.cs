// Niclas Nilsson, 2022

namespace TheTreacheryOfWhales;

/// <summary>
/// Main class of project.
/// </summary>
public static class Game
{
    private static readonly List<Int32> Positions = new() {
        16, 1, 2, 0, 4, 2, 7, 1, 2, 14
    };

    /// <summary>
    /// Calculates number of moves between two positions.
    /// </summary>
    /// <param name="fromPos">Position A</param>
    /// <param name="toPos">Position B</param>
    /// <returns>A positive (or zero) value of number of steps for that move.</returns>
    private static int CalcMovesToPos(int fromPos, int toPos) => Math.Abs(fromPos - toPos);

    /// <summary>
    /// Calculate number of moves for each of the crabs to get to the right position.
    /// </summary>
    /// <param name="toPos">Target position.</param>
    /// <returns>
    /// A array 
    /// </returns>
    private static int[] CalcMovesForPos(int toPos)
    {
        int noOfCrabs = Positions.Count();
        int[] moves = new int[noOfCrabs];

        for (int crabIdx = 0; crabIdx < noOfCrabs; crabIdx++)
        {
            int fromPos = Positions[crabIdx];
            moves[crabIdx] = CalcMovesToPos(fromPos, toPos);
        }

        return moves;
    }

    /// <summary>
    /// Calculate moves for all crabs to all positions (from the position of the lowest
    /// positioned crab to the highest positioned crab)
    /// </summary>
    /// <returns>
    /// An multidemensional array where index 0 is the position of the lowest positioned crab, index 1
    /// is the position of the lowest positioned crab + 1 and, index 2 is the position of the  lowest
    /// positioned crab + 2 and so on. Each subarray is the number of moves each crab has to make to
    /// reach that position.
    /// </returns>
    private static int[][] CalcMovesForAllCrabs()
    {
        int noOfCrabs = Positions.Count();
        int noOfPossiblePositions = Positions.Max() - Positions.Min();
        int[][] allPositions = new int[noOfPossiblePositions][];

        for (int i = 0; i < noOfPossiblePositions; i++)
        {
            allPositions[i] = CalcMovesForPos(Positions.Min() + i);
        }

        return allPositions;
    }

    /// <summary>
    /// Calculate on which index in the jagged array returned by the CalcMovesForAllCrabs()-
    /// method to find the position with least total crab moves.
    /// </summary>
    /// <returns></returns>
    private static int CalcBestPositionIdx()
    {
        int bestIdx = 0;
        int bestSumYet = Int32.MaxValue;

        int[][] allPositions = CalcMovesForAllCrabs();

        for (int i = 0; i < allPositions.Length; i++)
        {
            int totalNoOfMoves = allPositions[i].Sum();
            if (totalNoOfMoves < bestSumYet)
            {
                bestSumYet = totalNoOfMoves;
                bestIdx = i;
            }
        }

        return bestIdx;
    }

    /// <summary>
    /// Main method.
    /// </summary>
    public static void Main()
    {
        Console.WriteLine(CalcBestPositionIdx());
    }
}