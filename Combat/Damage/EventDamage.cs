using Metamon.Combat.State;

namespace Metamon.Combat.Damage
{
    public class EventDamage : IDamage
    {
        public Action<FighterState, FighterState>? OnDeal { get; set; }

        public void DealDamage(FighterState source, FighterState target)
        {
            OnDeal?.Invoke(source, target);
        }
    }
}
