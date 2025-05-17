using Metamon.Combat.Abilities;
using Metamon.Combat.State;

namespace Metamon.Combat
{
    public class Fighter
    {
        public FighterState State { get; private set; }
        public string Image { get; private set; }
        public Ability[] Abilities { get; private set; } = new Ability[4];

        public Fighter(FighterState state, string image, Ability[]? abilities = null)
        {
            State = state;
            Image = image;
            Abilities = abilities ?? [];
        }

        public void ExecuteAbility(int index, Fighter target)
        {
            var ability = Abilities[index];
            ability.Execute(this, target);
        }
    }
}