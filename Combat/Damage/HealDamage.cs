namespace Metamon.Combat.Damage
{
    public class HealDamage : IDamage
    {
        public int Amount { get; set; } = 0;
        public float PercentageAmount { get; set; } = 0;

        public void DealDamage(Fighter source, Fighter target)
        {
            var health = source.State.HealthAttrs.CurrentHealth;
            var maxHealth = source.State.DefenceAttrsModified().MaxHealth;
            var healingAmount = (int)Math.Ceiling(health * (PercentageAmount / 100f)) + Amount;
            var newHealth = Math.Min(health + healingAmount, maxHealth);
            source.State.HealthAttrs.CurrentHealth = newHealth;
        }
    }
}