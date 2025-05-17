using Metamon.Combat;
using Metamon.UI;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Started");

        var player = FighterFactory.CreateFighter(FighterFactory.FighterType.FROG);
        var enemy = FighterFactory.CreateFighter(FighterFactory.FighterType.EGG);

        var duel = new Duel(player, enemy);
        DuelDrawer.Init(duel);
        duel.Begin();

        while (true) { Thread.Sleep(10); } // Keep the game running
    }
}