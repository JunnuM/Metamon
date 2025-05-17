using Metamon.Combat.Abilities;
using Metamon.Combat.Damage;
using Metamon.Combat.State;
using Metamon.Combat.State.Mods;
using Metamon.Game;
using Metamon.UI;

namespace Metamon.Combat
{
    public static class FighterFactory
    {
        public enum FighterType
        {
            EGG,
            TADPOLE,
            FROG,
            CRAZYFROG,
            FISH,
            SNAKE,
            BANKER
        }

        public static Fighter CreateFighter(FighterType type)
        {
            return type switch
            {
                FighterType.EGG => EggFighter(),
                FighterType.TADPOLE => TadpoleFighter(),
                FighterType.FROG => FrogFighter(),
                FighterType.CRAZYFROG => CrazyFrogFighter(),
                FighterType.FISH => FishFighter(),
                FighterType.SNAKE => SnakeFighter(),
                FighterType.BANKER => BankerFighter(),
                _ => EggFighter(),
            };
        }

        private static Fighter FrogFighter()
        {
            var state = new FighterState(
                "Frog",
                new HealthAttributes { CurrentHealth = 30 },
                new AttackAttributes { Strength = 4, Agility = 5 },
                new DefenceAttributes { MaxHealth = 30, Armor = 2, IceRes = 1 }
            );

            var croak = new Ability(
                name: "Croak",
                description: "",
                cooldown: 10,
                damages: [new EventDamage { OnDeal = (s, t) =>
                    {
                        DuelDrawer.WriteToBattleLog("Croak");
                        Speakers.Play(Speakers.SoundKey.CROAK);

                        var success = GlobalRandom.NextInt(0, 4) == 0;
                        if (success)
                        {
                            s.TransformInto(CrazyFrogFighter());
                        }
                    }
                }]
            );

            var abilities = new Ability[]
            {
                new("Leap Strike", "", 3, [new PhysicalDamage { Amount = 6 }]),
                croak,
                new("Water Kick", "", 4, [new PhysicalDamage { Amount = 4, FlatArmorPen = 3 }]),
                croak
            };

            return new Fighter(state, FROG_IMAGE, abilities);
        }

        private static Fighter TadpoleFighter()
        {
            var state = new FighterState(
                "Tadpole",
                new HealthAttributes { CurrentHealth = 15 },
                new AttackAttributes { Strength = 2, Agility = 12 },
                new DefenceAttributes { MaxHealth = 15, Armor = 0, IceRes = 1 }
            );

            var grow = new Ability(
                name: "Grow",
                description: "",
                cooldown: 10,
                damages: [new EventDamage { OnDeal = (s, t) => {
                    var success = GlobalRandom.NextBool();
                    if (success)
                    {
                        DuelDrawer.WriteToBattleLog($"{s.State.Name} grew lungs");
                        s.TransformInto(FrogFighter());
                    } else
                    {
                        DuelDrawer.WriteToBattleLog($"{s.State.Name} grows slightly larger");
                    }
                } }]
            );

            var wiggleDash = new Ability("Wiggle Dash", "", 5, [new EventDamage { OnDeal = (s, t) => {
                    DuelDrawer.WriteToBattleLog($"{s.State.Name} became a lot faster");
                    var agilityMod = new AgilityMod { Name = "Speed++", Multiplier = 4, Duration = 3 };
                    agilityMod.AttachTo(s);
                }}]);

            var abilities = new Ability[]
            {
                wiggleDash,
                grow,
                grow,
                wiggleDash
            };

            return new Fighter(state, TADPOLE_IMAGE, abilities);
        }

        private static Fighter EggFighter()
        {
            var state = new FighterState(
                "Egg",
                new HealthAttributes { CurrentHealth = 5 },
                new AttackAttributes { },
                new DefenceAttributes { MaxHealth = 5, Armor = 0 }
            );

            var wait = new Ability(
                name: "Wait",
                description: "",
                cooldown: 10,
                damages: [new EventDamage { OnDeal = (s, t) => DuelDrawer.WriteToBattleLog($"{s.State.Name} waits patiently") }]
            );

            var hatch = new Ability(
                name: "Hatch",
                description: "",
                cooldown: 10,
                damages: [new EventDamage { OnDeal = (s, t) => {
                    var success = GlobalRandom.NextBool();
                    if (success)
                    {
                        DuelDrawer.WriteToBattleLog($"{s.State.Name} succesfully hatched");
                        s.TransformInto(TadpoleFighter());
                    } else
                    {
                        DuelDrawer.WriteToBattleLog($"{s.State.Name} struggles to break its shell");
                    }
                } }]
            );

            var abilities = new Ability[] { wait, hatch, hatch, wait };

            return new Fighter(state, EGG_IMAGE, abilities);
        }

        private static Fighter SnakeFighter()
        {
            var state = new FighterState(
                "Snake",
                new HealthAttributes { CurrentHealth = 35 },
                new AttackAttributes { Strength = 5, Agility = 6 },
                new DefenceAttributes { MaxHealth = 35, Armor = 2 }
            );

            var abilities = new Ability[]
            {
                new("Bite", "A venomous bite.", 24, [new PhysicalDamage { Amount = 6, PercentageArmorPen = 10 }]),
                new("Constrict", "Wrap and squeeze (placeholder).", 18),
            };

            return new Fighter(state, SNAKE_IMAGE, abilities);
        }

        private static Fighter CrazyFrogFighter()
        {
            var state = new FighterState(
                "Crazy Frog",
                new HealthAttributes { CurrentHealth = 45 },
                new AttackAttributes { Strength = 6, Agility = 7, Intellect = 8, Wisdom = 16 },
                new DefenceAttributes { MaxHealth = 45, Armor = 3 }
            );

            var abilities = new Ability[]
            {
                new("Ding Ding", "", 6, [new ArcaneDamage { Amount = 4, WisdomScaling = 0.5f }, new HealDamage { PercentageAmount = 50 }]),
                new("Swift Step", "", 12, [new EventDamage { OnDeal = (s, t) => {
                    var agilityMod = new AgilityMod { Name = "Swiftness", Duration = 8, FlatAddition = 11 };
                    agilityMod.AttachTo(s);
                    }
                }]),
                new("Turpiinveto", "", 18, [new PhysicalDamage { Amount = 12, AdditionalStrengthScaling = 1.2f }]),
                new("Crazy Jab", "", 32, [new PhysicalDamage { Amount = 24, PercentageArmorPen = 65 }])
            };

            return new Fighter(state, CRAZYFROG_IMAGE, abilities);
        }

        private static Fighter BankerFighter()
        {
            var state = new FighterState(
                "Banker",
                new HealthAttributes { CurrentHealth = 40 },
                new AttackAttributes { Intellect = 6, Wisdom = 5 },
                new DefenceAttributes { MaxHealth = 40, Armor = 1, ArcaneRes = 5 }
            );

            var abilities = new Ability[]
            {
                new("Audit Beam", "Destroys financial irregularities.", 18, [new ArcaneDamage { Amount = 7, WisdomScaling = 1f }]),
                new("Paper Shield", "Raises armor temporarily (placeholder).", 12),
            };

            return new Fighter(state, BANKER_IMAGE, abilities);
        }

        private static Fighter FishFighter()
        {
            var state = new FighterState(
                "Predator Fish",
                new HealthAttributes { CurrentHealth = 32 },
                new AttackAttributes { Strength = 5, Agility = 4 },
                new DefenceAttributes { MaxHealth = 32, Armor = 2 }
            );

            var abilities = new Ability[]
            {
                new("Bite", "A vicious snap with razor-sharp teeth.", 18, [new PhysicalDamage { Amount = 2, FlatArmorPen = 2 }]),
                new("Blood Scent", "", 52, [new EventDamage { OnDeal = (s, t) => DuelDrawer.WriteToBattleLog($"{state.Name} notices the smell of blood") }]),
            };

            return new Fighter(state, FISH_IMAGE, abilities);
        }

        private static readonly string FROG_IMAGE = @"
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

        private static readonly string TADPOLE_IMAGE = @"
..................................................
..................................................
..................................................
..................................................
..................................................
..................................................
...........:::::..................................
......:X+++;++xx+x+;;;:::+Xx;:::;;+++;:...........
....;+;;++x+;++++++x+xx++xxxx++;:::::::;;;+;;::;+:
...+;++xX&$+;++;++++x++++x+++++x++++++x+XX+xxX;+;.
..+;;;++++++++++xx+++xxXXx;;++;++;++;:;x+::::.....
..;++xx+++x+x++x++x+x:::::;;:+xx+++;..............
....:+Xx++x++x++xX+++XxxX;........................
.................+xxx+;;+:........................
.....................+++..........................
..................................................
..................................................
..................................................
..................................................
..................................................".Trim();

        private static readonly string EGG_IMAGE = @"
 ..................................................
................:::.:::::..:::::::................
...............:...:;;::;::...::::::..............
..............::::+xXX+;;::.;+xXx;;;:.............
..............:::;xXX$X;;:::+xXXX+;+;.............
...............::;+xx+;;;:::+XXXx;;;:.............
...........:...:::;;;;;:::::::;;;;;:::............
.........:...:;;;;;;::::::::::;;;:...::::.........
.........::;xxxXX+;;::..::;:::;:...;++;;;;........
.........:;;xxXX$+;::.:;xXX+;:;:::+xxXXx;+:.......
.........:::;xXx++;:::;xxXXX;;;::;xX$$$+;;........
..........::::;;;;;::::;xx+;;;;::::;++;;;:........
............::::..:::::::;;;;;::::::;;:...........
............::...:::::;::::::::::::::.............
...........::::;+xx+;:;::::.:;+xX+;:;;............
...........:::;xxXXX+;;;:;::;xxXXX+;;;............
...........:;:;x$$$X;;;;::::;xXXXx;;;.............
............::::;;;;;;;:.::::;;;;;;;:.............
..............:::;;;;::....:::::::................
..................................................".Trim();

        private static readonly string CRAZYFROG_IMAGE = @"
..................................................
..................................................
.........;+;......:...............................
........XxxXxxx::..;$::XXX$$Xx:............:;+;...
........:x;...::...:$XX$x;;xXX$:....+x++;;+++xxx..
...............:X$$$+;xXXXXX$&$$:.x+;.....+xxx$+..
...............:x++xxXXXX$$$$$$$X$$+..............
...............++.$xx;+$$XXxXXX&$X;...............
..............:Xx+;++xxx;X+;;XX$$&+...............
..............:x;;::.::;xxxxxx+X$$$:..............
..............+;;:X$+xx;;+xxx$XX&$$$x.............
..............X;:::::::::::;+x$&;;$$$:............
..............+.:x+xX$$$$$&$$XXX++.+;.............
....................xx$XXXxxXX$$$X::X;:...........
..................;+$X;XXXXxXX$X;;..::............
.................;X+:..:;xx+;:.x$Xx...............
............::;++x+.............:+Xx..............
...........:xxX+...................;:.............
...................................;::::..........
...................................;++;+:.........".Trim();

        private static readonly string BANKER_IMAGE = @"
..................................................
.................:++:..x$X:.......................
...............:$&&X+:.+&&+.......................
...............$&&&$X+.;&&+.......................
...............x&&&&$X+:x$+..:....................
................;X&&$x+:.:x+&&:...................
.................:X++xx+:X&$$&:...................
................+&&XxX$&X$$+..&+..................
................X&&&&+........&X..................
..................x&$;:::.;:X+.+X;................
.................:xx;::.:..:...x;.................
..................+x;..:;;;x..:+;.................
..................:;:....;;....x:.................
.................x$$x+:..:....:+$$X;..............
..............:X&XxX+$&&X;;;:;;;+;:+X+:...........
..........;$$x++X;xX;+X&$$xx$X.X&&$X$x+;;:........
........;$$&X&$$X&&&X;&&x$$&&$:&&&&x&&&X$$;.......
........X:$&$&&&&$$&&;.. ...  &&&$$&&&&&$&&;......
.......+X;&&X&&&&&&$&x.  .. .x&&$$$&&&&&$&&X......
.......:x;&&x&&&&x&&&&.. .. ;&&&&$&&$Xx$X&&$:.....".Trim();

        private static readonly string SNAKE_IMAGE = @"
..................:+;;;;;;;;++x;;;;;+;............
................;;;;;;;;;;;;;;;+;;;;;;;+:.........
.............+X;;;;;;+++;;;;;;;;X;;;;;;;;;........
...........;;;;;;;++;;;++;;;;;;;;;;;;;;;;;+.......
..........:;;;;;;x;:;x::+;...+::;;;;;;;;;;;+......
..........+;;;;;;x;;+x;;+:..:;..:.:;;;;;;;;;:.....
.......:;;;;;;;;;;;;;;;:..::;:;:::..:;;;;;;+:.....
.......+;;;;;+;;;;;;+;.:;;X::;.:X:...+;;;;;+:.....
.......;;;++;;+;;:+++xx+:;::..;;::.:..+;;;;;:.....
........:.;..:.::x++++;+;+;.::+:::...:;;;+;+......
.........;;:..+:+++;+;;+:;+::+:::....+;;;+x.......
...........;...+x;;+;;x:+:.:;::.::..;;;;++........
..............x+;X;;+;X..:+::......x;;++x:........
..............+XXx+++:.:;:....::.+;;;++;..........
....;xxXXXXXxx+:......::.......+;;;++x:...........
......+X;:..........:;.:::;..;;;;+++:.............
.....:;............;:......:+;;++++...............
..................;:......;;;;++;;................
..................:.::::::+;;+++;.................
..................+......x;;;+++..................".Trim();

        private static readonly string FISH_IMAGE = @"
..................................................
..............................::;;;;:.............
.................;++:.....;$x+xxxX$;..............
............;+x+xxXXXxxxXXXxxxxxXx................
.........:+x+xxXx:$xxxXxxxXxxXXXX:...........::::.
......;xx+x$$+.:+x+xxX;XxxxxxxxXX+.........xxxxXXX
..;+x+++++xxxxXx++XX:x;+xxxxXxxxxX;......+x+xXXx..
xx++++++++xXx:::;x+;:x;+xxXXxxxxxxxX....:x+xXXx...
.;+:::::;x+::;xxX+;+;x;+xxxxxxxxxxxxX:.;XxxxXX;...
...:Xx++++xX++..:;..:;;XxxxxxxxxxxxxxxxxxXxXX+....
....:+..+::..+x::.;:;;;xxxxxxxxxxxxxxxxxxxxXxx:...
......;.;$+..;:..+x$;:Xxxxxxxxxxxx$+x:..:XxxXx;...
........+x.:$+x:::x;::;xxxxxxX$X$.........+$xXX:..
..........+:..:+x;:::::xxxxx+xxx:.................
..........:+++;;;::::::;;Xxxxxxx$:................
.............:;xXx++xXx;::+$XXXxxX$:..............
...................:xxx.......::..................
..................................................
..................................................
..................................................".Trim();
    }
}