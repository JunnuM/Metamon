using Metamon.Combat.State;

namespace Metamon.Combat
{
    public class Fighter
    {
        public FighterState State { get; private set; }

        public Fighter(FighterState state)
        {
            State = state;
        }
    }
}