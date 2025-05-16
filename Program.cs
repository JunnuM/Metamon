using Metamon.Combat;
using Metamon.Combat.Abilities;
using Metamon.Combat.Damage;
using Metamon.Combat.State;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Started");

        // Create attacker (e.g., Warrior)
        var warriorState = new FighterState(
            name: "Warrior",
            health: new HealthAttributes
            {
                CurrentHealth = 100,
                CurrentShield = 0
            },
            attrs: new AttackAttributes
            {
                Strength = 10,
                Intellect = 2,
                Wisdom = 1,
                Agility = 3
            }
        );
        var slashAttackDamage = new PhysicalDamage
        {
            Amount = 10,
            FlatArmorPen = 1,
            PercentageArmorPen = 25
        };
        var slashAttack = new Ability(
            name: "Slash",
            description: "Sharp and swift slash",
            cooldown: 10,
            damages: [slashAttackDamage]
        );
        var warrior = new Fighter(
            state: warriorState,
            abilities: [slashAttack]
        );

        // Create defender (e.g., Frog)
        var frogState = new FighterState(
            name: "Frog",
            health: new HealthAttributes
            {
                CurrentHealth = 50,
                CurrentShield = 0
            },
            attrs: new AttackAttributes
            {
                Strength = 3,
                Intellect = 1,
                Wisdom = 1,
                Agility = 2
            }
        );
        var frog = new Fighter(
            state: frogState,
            abilities: []
        );


        Console.WriteLine(frogState);
        warrior.ExecuteAbility(0, frog);
        // Print post-combat state
        Console.WriteLine("\nAfter Damage:");
        Console.WriteLine(frogState);
    }
}