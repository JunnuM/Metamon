namespace Metamon.Combat.State.Mods
{
    public class MaxHealthMod : DefenceAttributeMod
    {
        public int FlatAddition { get; set; } = 0;
        public float Multiplier { get; set; } = 1;

        public override DefenceAttributes GetModified(DefenceAttributes defenceAttributes)
        {
            return new DefenceAttributes()
            {
                MaxHealth = (int)Math.Ceiling(defenceAttributes.MaxHealth * Multiplier) + FlatAddition,
                Armor = defenceAttributes.Armor,
                FireRes = defenceAttributes.FireRes,
                IceRes = defenceAttributes.IceRes,
                ArcaneRes = defenceAttributes.ArcaneRes
            };
        }
    }
}