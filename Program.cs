using Metamon.Combat;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Started");

        // Create attacker (e.g., Warrior)
        var warrior = new FighterState(
            name: "Warrior",
            health: new FighterState.HealthAttributes
            {
                CurrentHealth = 100,
                CurrentShield = 0
            },
            attrs: new FighterState.AttackAttributes
            {
                Strength = 10,
                Intellect = 2,
                Wisdom = 1,
                Agility = 3
            },
            resists: new FighterState.DefenceAttributes
            {
                MaxHealth = 100,
                Armor = 5
            }
        );

        // Create defender (e.g., Frog)
        var frog = new FighterState(
            name: "Frog",
            health: new FighterState.HealthAttributes
            {
                CurrentHealth = 50,
                CurrentShield = 0
            },
            attrs: new FighterState.AttackAttributes
            {
                Strength = 3,
                Intellect = 1,
                Wisdom = 1,
                Agility = 2
            },
            resists: new FighterState.DefenceAttributes
            {
                MaxHealth = 50,
                Armor = 4
            }
        );

        // Create a base physical damage (will scale with attacker strength)
        var baseDamage = new PhysicalDamage
        {
            Amount = 10,
            FlatArmorPen = 1,
            PercentageArmorPen = 25
        };

        // Print pre-combat states
        Console.WriteLine("Before Damage:");
        Console.WriteLine(warrior);
        Console.WriteLine();
        Console.WriteLine(frog);
        Console.WriteLine();

        // Scale damage using attacker attributes
        var scaledDamage = baseDamage.GetDamage(warrior);

        // Apply damage to defender
        scaledDamage.DealDamage(frog);

        // Print post-combat state
        Console.WriteLine("\nAfter Damage:");
        Console.WriteLine(frog);
    }
}