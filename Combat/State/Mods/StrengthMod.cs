namespace Metamon.Combat.State.Mods
{
    public class StrengthMod : AttackAttributeMod
    {
        public int FlatAddition { get; set; } = 0;
        public float Multiplier { get; set; } = 1;

        public override AttackAttributes GetModified(AttackAttributes attackAttributes)
        {
            return new AttackAttributes()
            {
                Strength = (int)Math.Ceiling(attackAttributes.Strength * Multiplier) + FlatAddition,
                Intellect = attackAttributes.Intellect,
                Wisdom = attackAttributes.Wisdom,
                Agility = attackAttributes.Agility
            };
        }
    }
}