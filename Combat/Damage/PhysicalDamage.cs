using Metamon.UI;

namespace Metamon.Combat.Damage
{
    public class PhysicalDamage : IDamage
    {
        public int Amount { get; set; }
        public float AdditionalStrengthScaling { get; set; } = 0;
        public int FlatArmorPen { get; set; } = 0;
        public int PercentageArmorPen { get; set; } = 0; // [0, 100]

        // First apply flat armor pen, then percentage. Effective armor is always positive.
        public void DealDamage(Fighter source, Fighter target)
        {
            var modifiecAttackAttrs = source.State.AttackAttrsModified();
            var modifiedAmount = Amount + (int)Math.Ceiling(AdditionalStrengthScaling * modifiecAttackAttrs.Strength);

            var modifiedDefences = target.State.DefenceAttrsModified();
            float rawArmor = modifiedDefences.Armor - FlatArmorPen;
            float effectiveArmor = rawArmor * (1 - PercentageArmorPen * 0.01f);
            int finalArmor = (int)Math.Ceiling(Math.Max(effectiveArmor, 0));

            var amount = Math.Max(modifiedAmount - finalArmor, 0);
            var newHealth = Math.Max(target.State.HealthAttrs.CurrentHealth - amount, 0);
            target.State.HealthAttrs.CurrentHealth = newHealth;

            DuelDrawer.WriteToBattleLog($"{target.State.Name} took {amount} physical damage");
        }
    }
}