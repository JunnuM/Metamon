using Metamon.Combat.State;

namespace Metamon.Combat.Damage
{
    public interface IDamage
    {
        public void DealDamage(FighterState source, FighterState target);
    }
}