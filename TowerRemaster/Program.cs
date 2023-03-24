using TowerRemaster;

internal class Program
{
    private static void Main(string[] args)
    {
        // This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block.
        using Game game = new Game(1024, 512, "Tower Remaster");
        game.Run();
    }
}