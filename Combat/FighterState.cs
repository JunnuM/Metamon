namespace Metamon.Combat
{
    public class FighterState
    {
        public string Name { get; private set; }
        public HealthAttributes Health { get; private set; }
        public AttackAttributes Attacks { get; private set; }
        public ResistanceAttributes Resistances { get; private set; }

        public override string ToString()
        {
            return $@"
=== Fighter: {Name} ===
Health:
  Current Health : {Health.CurrentHealth}
  Max Health     : {Health.MaxHealth}
  Current Shield : {Health.CurrentShield}

Attack Attributes:
  Strength  : {Attacks.Strength}
  Intellect : {Attacks.Intellect}
  Wisdom    : {Attacks.Wisdom}
  Agility   : {Attacks.Agility}

Resistances:
  Physical : {Resistances.Physical}
  Fire     : {Resistances.Fire}
  Ice      : {Resistances.Ice}
  Arcane   : {Resistances.Arcane}
".Trim();
        }

        public FighterState(
            string name,
            HealthAttributes health,
            AttackAttributes? attrs = null,
            ResistanceAttributes? resists = null
            )
        {
            Name = name;
            Health = health;
            Attacks = attrs ?? new AttackAttributes();
            Resistances = resists ?? new ResistanceAttributes();
        }

        public class HealthAttributes
        {
            public int CurrentHealth { get; set; } = 0;
            public int MaxHealth { get; set; } = 0;
            public int CurrentShield { get; set; } = 0;
        }

        public class AttackAttributes
        {
            public int Strength { get; set; } = 0;
            public int Intellect { get; set; } = 0;
            public int Wisdom { get; set; } = 0;
            public int Agility { get; set; } = 0;
        }

        public class ResistanceAttributes
        {
            public int Physical { get; set; } = 0;
            public int Fire { get; set; } = 0;
            public int Ice { get; set; } = 0;
            public int Arcane { get; set; } = 0;
        }
    }
}