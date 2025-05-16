namespace Metamon.Combat
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


        public override string ToString()
        {
            return $@"
=== Fighter: {Name} ===
Health Attributes:
  Current Health : {HealthAttrs.CurrentHealth}
  Current Shield : {HealthAttrs.CurrentShield}

Attack Attributes:
  Strength  : {AttackAttrs.Strength}
  Intellect : {AttackAttrs.Intellect}
  Wisdom    : {AttackAttrs.Wisdom}
  Agility   : {AttackAttrs.Agility}

Defence Attributes:
  Max Health : {DefenceAttrs.MaxHealth}
  Armor   : {DefenceAttrs.Armor}
  FireRes       : {DefenceAttrs.FireRes}
  IceRes        : {DefenceAttrs.IceRes}
  ArcaneRes     : {DefenceAttrs.ArcaneRes}
".Trim();
        }

        public FighterState(
            string name,
            HealthAttributes health,
            AttackAttributes? attrs = null,
            DefenceAttributes? resists = null
            )
        {
            Name = name;
            HealthAttrs = health;
            AttackAttrs = attrs ?? new AttackAttributes();
            DefenceAttrs = resists ?? new DefenceAttributes();
        }

        public class HealthAttributes
        {
            public int CurrentHealth { get; set; } = 0;
            public int CurrentShield { get; set; } = 0;
        }

        public class AttackAttributes
        {
            public int Strength { get; set; } = 0;
            public int Intellect { get; set; } = 0;
            public int Wisdom { get; set; } = 0;
            public int Agility { get; set; } = 0;
        }

        public class DefenceAttributes
        {
            public int MaxHealth { get; set; } = 0;
            public int Armor { get; set; } = 0;
            public int FireRes { get; set; } = 0;
            public int IceRes { get; set; } = 0;
            public int ArcaneRes { get; set; } = 0;
        }
    }
}