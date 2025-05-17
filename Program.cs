using Metamon.Combat;
using Metamon.Game;
using Metamon.UI;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Started");
        GlobalRandom.SetSeed("kakka123".GetHashCode());

        DuelDrawer.Intro();
        Thread.Sleep(4000);

        var duel = new Duel();
        duel.OnDuelEnded += (s, e) =>
        {
            duel.Stop();
            if (e.PlayerWon)
            {
                DuelDrawer.Outro_Victory();
            }
            else
            {
                DuelDrawer.Outro_Defeat();
            }
        };

        DuelDrawer.Init(duel);
        Speakers.Play(Speakers.SoundKey.MUSIC, true);
        duel.Begin();

        while (true) { Thread.Sleep(10); } // Keep the game running
    }
}