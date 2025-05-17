using Metamon.UI;

namespace Metamon.Combat.Damage
{
    public class FireDamage : IDamage
    {
        public int Amount { get; set; } = 1;
        public float IntellectScaling { get; set; } = 1;

        public void DealDamage(Fighter source, Fighter target)
        {
            var modifiedAttackAttrs = source.State.AttackAttrsModified();
            var modifiedAmount = (int)Math.Ceiling(Amount * IntellectScaling * modifiedAttackAttrs.Intellect);

            var modifiedDefences = target.State.DefenceAttrsModified();
            int fireRes = modifiedDefences.FireRes;

            // Arcane res is %-based
            var amount = Math.Max((int)Math.Ceiling(modifiedAmount * (fireRes / 100f)), 0);
            var newHealth = Math.Max(target.State.HealthAttrs.CurrentHealth - amount, 0);
            target.State.HealthAttrs.CurrentHealth = newHealth;

            DuelDrawer.WriteToBattleLog($"{target.State.Name} took {amount} fire damage");
        }
    }
}