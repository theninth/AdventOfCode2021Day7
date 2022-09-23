// Niclas Nilsson, 2022
// This program needs at least C# 7.0 for it's tuple syntax.

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
    /// An array where each element is number of moves for the crab with the same index in the
    /// Position-list.
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
        int[][] reasonablePositions = new int[Positions.Max() - Positions.Min()][];

        for (int i = 0; i < reasonablePositions.Length; i++)
        {
            reasonablePositions[i] = CalcMovesForPos(Positions.Min() + i);
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

        int bestPos = Positions.Min() + BestIdx;
        int[] bestMoves = reasonablePositions[BestIdx];
        return (bestPos, bestMoves);
    }

    /// <summary>
    /// Main method.
    /// </summary>
    public static void Main()
    {
        (int bestPos, int[] bestMoves) = CalcBestPosition();
    }
}