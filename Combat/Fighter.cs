using Metamon.Combat.Abilities;
using Metamon.Combat.State;

namespace Metamon.Combat
{
    public class Fighter
    {
        public FighterState State { get; private set; }
        public Ability[] Abilities { get; private set; } = [];

        public Fighter(FighterState state, Ability[]? abilities = null)
        {
            State = state;
            Abilities = abilities ?? [];
        }

        public void ExecuteAbility(int index, Fighter target)
        {
            var ability = Abilities[index];
            ability.Execute(this, target);
        }
    }
}