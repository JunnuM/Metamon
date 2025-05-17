namespace Metamon.Combat.State
{
    public class FighterState
    {
        public string Name { get; private set; }
        public HealthAttributes HealthAttrs { get; private set; }
        public AttackAttributes AttackAttrs { get; private set; }
        public AttackAttributeMod[] AttackAttrMods { get; private set; } = [];
        public AttackAttributes AttackAttrsModified()
        {
            return AttackAttrMods.Aggregate(AttackAttrs, (current, mod) => mod.GetModified(current));
        }
        public DefenceAttributes DefenceAttrs { get; private set; }
        public DefenceAttributeMod[] DefenceAttrMods { get; private set; } = [];
        public DefenceAttributes DefenceAttrsModified()
        {
            return DefenceAttrMods.Aggregate(DefenceAttrs, (current, mod) => mod.GetModified(current));
        }

        public FighterState(
            string name,
            HealthAttributes healthAttrs,
            AttackAttributes? attackAttrs = null,
            DefenceAttributes? defenceAttrs = null
            )
        {
            Name = name;
            HealthAttrs = healthAttrs;
            AttackAttrs = attackAttrs ?? new AttackAttributes();
            DefenceAttrs = defenceAttrs ?? new DefenceAttributes();
        }

        public float HealthNormalized()
        {
            var current = (float)HealthAttrs.CurrentHealth + HealthAttrs.CurrentShield;
            var max = (float)DefenceAttrsModified().MaxHealth + HealthAttrs.CurrentShield;

            return current / max;
        }

        public override string ToString()
        {
            return $@"
=== Fighter: {Name} ===

Health Attributes:
  Current Health : {HealthAttrs.CurrentHealth}
  Current Shield : {HealthAttrs.CurrentShield}

Attack Attributes:
  Strength       : {AttackAttrs.Strength}
  Intellect      : {AttackAttrs.Intellect}
  Wisdom         : {AttackAttrs.Wisdom}
  Agility        : {AttackAttrs.Agility}

Defence Attributes:
  Max Health     : {DefenceAttrs.MaxHealth}
  Armor          : {DefenceAttrs.Armor}
  Fire Resist    : {DefenceAttrs.FireRes}
  Ice Resist     : {DefenceAttrs.IceRes}
  Arcane Resist  : {DefenceAttrs.ArcaneRes}
".Trim();
        }

    }
}