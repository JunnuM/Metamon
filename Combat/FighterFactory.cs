using Metamon.Combat.Abilities;
using Metamon.Combat.State;

namespace Metamon.Combat
{
    public static class FighterFactory
    {
        public enum FighterType
        {
            EGG,
            TADPOLE,
            FROG
        }

        public static Fighter CreateFighter(FighterType type)
        {
            switch (type)
            {
                case FighterType.EGG:
                    {
                        var state = new FighterState(
                        name: "Egg",
                        healthAttrs: new HealthAttributes
                        {
                            CurrentHealth = 4
                        },
                        attackAttrs: new AttackAttributes
                        {
                            Strength = 0,
                            Intellect = 0,
                            Wisdom = 0,
                            Agility = 0
                        },
                        defenceAttrs: new DefenceAttributes
                        {
                            MaxHealth = 4
                        }
                    );
                        var ability = new Ability(
                            name: "Wait",
                            description: "Wait and pray",
                            cooldown: 1,
                            damages: []
                        );
                        var fighter = new Fighter(
                            state: state,
                            "egg image",
                            abilities: [ability, ability, ability, ability]
                        );
                        return fighter;
                    }
                case FighterType.TADPOLE:
                    {
                        var state = new FighterState(
                        name: "Tadpole",
                        healthAttrs: new HealthAttributes
                        {
                            CurrentHealth = 9
                        },
                        attackAttrs: new AttackAttributes
                        {
                            Strength = 1,
                            Intellect = 0,
                            Wisdom = 0,
                            Agility = 6
                        },
                        defenceAttrs: new DefenceAttributes
                        {
                            MaxHealth = 9
                        }
                    );
                        var ability = new Ability(
                            name: "Burst swim",
                            description: "",
                            cooldown: 2,
                            damages: []
                        );
                        var fighter = new Fighter(
                            state: state,
                            "tadpole image",
                            abilities: [ability, ability, ability, ability]
                        );
                        return fighter;
                    }
                case FighterType.FROG:
                    {
                        var state = new FighterState(
                        name: "Frog",
                        healthAttrs: new HealthAttributes
                        {
                            CurrentHealth = 15
                        },
                        attackAttrs: new AttackAttributes
                        {
                            Strength = 5,
                            Intellect = 2,
                            Wisdom = 1,
                            Agility = 12
                        },
                        defenceAttrs: new DefenceAttributes
                        {
                            MaxHealth = 15
                        }
                    );
                        var ability = new Ability(
                            name: "Leap",
                            description: "",
                            cooldown: 5,
                            damages: []
                        );
                        var fighter = new Fighter(
                            state: state,
                            frogImage,
                            abilities: [ability, ability, ability, ability]
                        );
                        return fighter;
                    }
            }
            return null;
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
}