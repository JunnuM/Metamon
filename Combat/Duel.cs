using Metamon.Game;

namespace Metamon.Combat
{
    public class Duel
    {
        public Fighter PlayerFighter { get; private set; }
        public Fighter EnemyFighter { get; private set; }

        public event EventHandler<OnDuelEndedEventArgs>? OnDuelEnded;
        public class OnDuelEndedEventArgs
        {
            public Fighter Winner { get; private set; }
            public bool PlayerWon { get; private set; }

            public OnDuelEndedEventArgs(Fighter winner, bool leftWon)
            {
                Winner = winner;
                PlayerWon = leftWon;
            }
        }

        public Duel(Fighter leftFighter, Fighter rightFighter)
        {
            PlayerFighter = leftFighter;
            EnemyFighter = rightFighter;
        }

        public void Begin()
        {
            Clock.CombatTimer.Start();
            Clock.CombatTimer.OnTick += CheckDeaths;
        }

        public void Stop()
        {
            Clock.CombatTimer.Stop();
        }

        private void CheckDeaths(object? sender, EventArgs e)
        {
            if (PlayerFighter.State.HealthAttrs.CurrentHealth <= 0)
            {
                OnDuelEnded?.Invoke(this, new OnDuelEndedEventArgs(EnemyFighter, false));
                Clock.CombatTimer.OnTick -= CheckDeaths;
            }
            else if (EnemyFighter.State.HealthAttrs.CurrentHealth <= 0)
            {
                OnDuelEnded?.Invoke(this, new OnDuelEndedEventArgs(PlayerFighter, true));
                Clock.CombatTimer.OnTick -= CheckDeaths;
            }
        }
    }

}