namespace Metamon.Combat.State.Mods
{
    public class WisdomMod : AttackAttributeMod
    {
        public int FlatAddition { get; set; } = 0;
        public float Multiplier { get; set; } = 1;

        public override AttackAttributes GetModified(AttackAttributes attackAttributes)
        {
            return new AttackAttributes()
            {
                Strength = attackAttributes.Strength,
                Intellect = attackAttributes.Intellect,
                Wisdom = (int)Math.Ceiling(attackAttributes.Wisdom * Multiplier) + FlatAddition,
                Agility = attackAttributes.Agility
            };
        }
    }
}