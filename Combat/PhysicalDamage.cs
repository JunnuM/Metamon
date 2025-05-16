using Metamon.Combat;

public class PhysicalDamage : IDamage
{
    public int Amount { get; set; }
    public float AdditionalStrengthScaling { get; set; } = 0;
    public int FlatArmorPen { get; set; } = 0;
    public int PercentageArmorPen { get; set; } = 0; // [0, 100]

    public IDamage GetDamage(FighterState source)
    {
        var modifiecAttackAttrs = source.AttackAttrsModified();
        var modifiedAmount = Amount + (int)Math.Ceiling(AdditionalStrengthScaling * modifiecAttackAttrs.Strength);
        // TODO: Armor pen changes by agility?
        return new PhysicalDamage
        {
            Amount = modifiedAmount,
            AdditionalStrengthScaling = AdditionalStrengthScaling,
            FlatArmorPen = FlatArmorPen,
            PercentageArmorPen = PercentageArmorPen
        };
    }

    // First apply flat armor pen, then percentage. Effective armor is always positive.
    public void DealDamage(FighterState target)
    {
        var modifiedDefences = target.DefenceAttrsModified();
        float rawArmor = modifiedDefences.Armor - FlatArmorPen;
        float effectiveArmor = rawArmor * (1 - PercentageArmorPen * 0.01f);
        int finalArmor = (int)Math.Ceiling(Math.Max(effectiveArmor, 0));

        var amount = Math.Max(Amount - finalArmor, 0);
        target.HealthAttrs.CurrentHealth -= amount;
    }

}