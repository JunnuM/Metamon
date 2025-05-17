namespace Metamon.Combat.Damage
{
    public class EventDamage : IDamage
    {
        public Action<Fighter, Fighter>? OnDeal { get; set; }

        public void DealDamage(Fighter source, Fighter target)
        {
            OnDeal?.Invoke(source, target);
        }
    }
}
