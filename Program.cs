namespace TheTreacheryOfWhales;

public static class Game
{
    private static readonly int[] Positions = {
        16, 1, 2, 0, 4, 2, 7, 1, 2, 14
    };

    private static int CalcMovesToPos(int fromPos, int toPos) => Math.Abs(fromPos - toPos);

    public static void Main()
    {
        Console.WriteLine(CalcMovesToPos(16, 2));
    }
}