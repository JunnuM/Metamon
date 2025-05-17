using Metamon.Combat;
using Metamon.Combat.Abilities;
using Metamon.Combat.Damage;
using Metamon.Combat.State;
using Metamon.Game;
using Metamon.UI;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Started");

        // Create attacker (e.g., Warrior)
        var warriorState = new FighterState(
            name: "Warrior",
            healthAttrs: new HealthAttributes
            {
                CurrentHealth = 100,
                CurrentShield = 0
            },
            attackAttrs: new AttackAttributes
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
            frogImage,
            abilities: [slashAttack]
        );

        // Create defender (e.g., Frog)
        var frogState = new FighterState(
            name: "Frog",
            healthAttrs: new HealthAttributes
            {
                CurrentHealth = 50,
                CurrentShield = 0
            },
            attackAttrs: new AttackAttributes
            {
                Strength = 3,
                Intellect = 1,
                Wisdom = 1,
                Agility = 2
            },
            defenceAttrs: new DefenceAttributes
            {
                MaxHealth = 50
            }
        );
        var frog = new Fighter(
            state: frogState,
            frogImage,
            abilities: []
        );

        Clock.CombatTimer.OnTick += (o, s) =>
        {
            warrior.ExecuteAbility(0, frog);
        };

        var duel = new Duel(frog, warrior);
        DuelDrawer.Init(duel);
        duel.Begin();

        while (true) { Thread.Sleep(10); } // Keep the game running
    }


    private static readonly string frogImage = @"
--------------------------------------------------
--------------------------------------------------
--------------------------------==----------------
------------------------------*******=------------
---------------------------=+***@%***+**#*=-------
----------------------=******++*##**********=-----
-----------------=+*****##******************=-----
--------------*#++*%#%*%%+**************##*-------
-----------+*********+*##*******=======+=---------
---------**#######%#**#%##*#*+======*=------------
-------#******+********#++========+=--------------
-----*#*#**###***#*+=============+=---------------
----=#**********++**+========*===#----------------
-----+#*#*#*++++++++++++*+===#===*+++++-----------
-----=******+++++++*+***+++*=-+===++**+=----------
-----=*************#*+*+#+----====+++**=----------
-------+**#***#*#****#*--------+====**+=----------
-------------#**##***+*+=--------*==+**=----------
-------------=++****+++*=----------==++=----------
--------------------------------------------------".Trim();
}