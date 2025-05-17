using Metamon.Combat;
using Metamon.Game;
using Metamon.UI;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Started");
        GlobalRandom.SetSeed("kakka123".GetHashCode());

        var player = FighterFactory.CreateFighter(FighterFactory.FighterType.FROG);
        var enemy = FighterFactory.CreateFighter(FighterFactory.FighterType.FISH);

        var duel = new Duel(player, enemy);
        DuelDrawer.Init(duel);
        Speakers.Play(Speakers.SoundKey.MUSIC, true);
        duel.Begin();

        while (true) { Thread.Sleep(10); } // Keep the game running
    }
}