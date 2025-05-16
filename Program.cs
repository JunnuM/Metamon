using Metamon.Combat;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Started");

        #region Test states
        // Tadpole – weak, minimal resistances, low attributes
        var tadpole = new FighterState(
            name: "Tadpole",
            health: new FighterState.HealthAttributes
            {
                CurrentHealth = 5,
                MaxHealth = 5,
                CurrentShield = 0
            },
            attrs: new FighterState.AttackAttributes
            {
                Strength = 1,
                Intellect = 1,
                Wisdom = 0,
                Agility = 3
            }
        );

        // Banker – low physical stats, high wisdom (for strategic defense)
        var banker = new FighterState(
            name: "Banker",
            health: new FighterState.HealthAttributes
            {
                CurrentHealth = 40,
                MaxHealth = 40,
                CurrentShield = 5
            },
            attrs: new FighterState.AttackAttributes
            {
                Strength = 2,
                Intellect = 7,
                Wisdom = 10,
                Agility = 3
            },
            resists: new FighterState.ResistanceAttributes
            {
                Physical = 1,
                Fire = 1,
                Ice = 1,
                Arcane = 4
            }
        );

        // Wizard – high intellect, low armor, decent arcane resistance
        var wizard = new FighterState(
            name: "Wizard",
            health: new FighterState.HealthAttributes
            {
                CurrentHealth = 60,
                MaxHealth = 60,
                CurrentShield = 15
            },
            attrs: new FighterState.AttackAttributes
            {
                Strength = 2,
                Intellect = 18,
                Wisdom = 12,
                Agility = 6
            },
            resists: new FighterState.ResistanceAttributes
            {
                Physical = 2,
                Fire = 3,
                Ice = 3,
                Arcane = 10
            }
        );
        #endregion

        Console.WriteLine(tadpole.ToString());

    }
}