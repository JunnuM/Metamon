namespace Metamon.Combat.State.Mods
{
    public class ArmorMod : DefenceAttributeMod
    {
        public int FlatAddition { get; set; } = 0;
        public float Multiplier { get; set; } = 1;

        public override DefenceAttributes GetModified(DefenceAttributes defenceAttributes)
        {
            return new DefenceAttributes()
            {
                MaxHealth = defenceAttributes.MaxHealth,
                Armor = (int)Math.Ceiling(defenceAttributes.Armor * Multiplier) + FlatAddition,
                FireRes = defenceAttributes.FireRes,
                IceRes = defenceAttributes.IceRes,
                ArcaneRes = defenceAttributes.ArcaneRes
            };
        }
    }
}