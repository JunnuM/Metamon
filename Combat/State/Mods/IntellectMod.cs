namespace Metamon.Combat.State.Mods
{
    public class IntellectMod : AttackAttributeMod
    {
        public int FlatAddition { get; set; } = 0;
        public float Multiplier { get; set; } = 1;

        public override AttackAttributes GetModified(AttackAttributes attackAttributes)
        {
            return new AttackAttributes()
            {
                Strength = attackAttributes.Strength,
                Intellect = (int)Math.Ceiling(attackAttributes.Intellect * Multiplier) + FlatAddition,
                Wisdom = attackAttributes.Wisdom,
                Agility = attackAttributes.Agility
            };
        }
    }
}