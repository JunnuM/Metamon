using Metamon.Combat.Abilities;
using Metamon.Combat.Damage;
using Metamon.Combat.State;
using Metamon.Combat.State.Mods;
using Metamon.UI;

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
            return type switch
            {
                FighterType.EGG => EggFighter(),
                FighterType.TADPOLE => TadpoleFighter(),
                FighterType.FROG => FrogFighter(),
                _ => EggFighter(),
            };
        }

        private static Fighter FrogFighter()
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
                                        MaxHealth = 15,
                                        Armor = 1
                                    }
                                );
            var leap = new Ability(
                name: "Leap",
                description: "",
                cooldown: 5,
                damages: []
            );
            var bite = new Ability(
                name: "Bite",
                description: "",
                cooldown: 10,
                damages: [new PhysicalDamage{
                                Amount = 2
                            }]
            );
            var morph = new Ability(
                name: "Morph",
                description: "",
                cooldown: 6,
                damages: [
                    new EventDamage { OnDeal = (source, target) =>
                                {
                                    var modifier = new AgilityBoost() {
                                        FlatAddition = 10,
                                        Duration = 10
                                    };
                                    modifier.AttachTo(target);
                                    /*
                                    var success = GlobalRandom.NextBool();
                                    if (success)
                                    {
                                        DuelDrawer.WriteToBattleLog("Your legs keep on streching!");
                                        var next = TadpoleFighter();
                                    } else
                                    {
                                        DuelDrawer.WriteToBattleLog("Morph failed!");
                                    } */
                                }
                                }
                ]
            );
            var fighter = new Fighter(
                state: state,
                frogImage,
                abilities: [leap, bite, morph, leap]
            );
            return fighter;
        }

        private static Fighter TadpoleFighter()
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

        private static Fighter EggFighter()
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