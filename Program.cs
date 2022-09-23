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
    /// Main method.
    /// </summary>
    public static void Main()
    {
        CalcMovesForPos(2).ToList().ForEach(i => Console.WriteLine(i.ToString()));
    }
}