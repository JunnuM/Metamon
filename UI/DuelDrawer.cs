using Metamon.Combat;
using Metamon.Game;

namespace Metamon.UI
{
    public static class DuelDrawer
    {
        private static Duel _duel;
        private static List<string> _battle_log = ["<<<       Battle log       >>>"];
        private static readonly int _battle_log_max_length = 32;

        public static void Init(Duel duel)
        {
            _duel = duel;

            Console.Clear();

            DrawTitle();

            Clock.CombatTimer.OnTick += OnCombatTimer;
        }

        public static void WriteToBattleLog(string message)
        {
            _battle_log.Add(message);
            if (_battle_log.Count > _battle_log_max_length)
            {
                _battle_log.RemoveAt(1); // Leave title
            }
        }

        private static void OnCombatTimer(object? sender, EventArgs e)
        {
            Update();
        }

        private static void Update()
        {
            // Clear console if needed
            DrawFighters();
            DrawPlayerAbilities();
            DrawBattleLog();
        }

        private static void DrawTitle()
        {
            ConsoleUtils.DrawImageAt(TITLE, 1, 1);
        }

        private static void DrawFighters()
        {
            var playerFighter = _duel.PlayerFighter;
            var enemyFighter = _duel.EnemyFighter;

            ConsoleUtils.DrawImageAt(playerFighter.Image, 1, 12, playerFighter.State.HealthNormalized(), ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleUtils.DrawImageAt(playerFighter.State.Name, 1, 31);
            ConsoleUtils.DrawImageAt(enemyFighter.Image, 56, 12, enemyFighter.State.HealthNormalized(), ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleUtils.DrawImageAt(enemyFighter.State.Name, 56, 31);
        }

        private static void DrawPlayerAbilities()
        {
            var abilities = _duel.PlayerFighter.Abilities;

            var upAbility = abilities[0];
            if (upAbility != null)
            {
                var text = ConsoleUtils.GenerateBorderedTextBox(upAbility.Name, 25);
                var cdNormalized = upAbility.CooldownNormalized();
                ConsoleUtils.DrawImageAt(text, 41, 33, cdNormalized, ConsoleColor.Cyan, ConsoleColor.Gray);
            }

            var leftAbility = abilities[1];
            if (leftAbility != null)
            {
                var text = ConsoleUtils.GenerateBorderedTextBox(leftAbility.Name, 25);
                var cdNormalized = leftAbility.CooldownNormalized();
                ConsoleUtils.DrawImageAt(text, 21, 36, cdNormalized, ConsoleColor.Cyan, ConsoleColor.Gray);
            }

            var rightAbility = abilities[2];
            if (rightAbility != null)
            {
                var text = ConsoleUtils.GenerateBorderedTextBox(rightAbility.Name, 25);
                var cdNormalized = rightAbility.CooldownNormalized();
                ConsoleUtils.DrawImageAt(text, 61, 36, cdNormalized, ConsoleColor.Cyan, ConsoleColor.Gray);

            }

            var downAbility = abilities[3];
            if (downAbility != null)
            {
                var abilityDownText = ConsoleUtils.GenerateBorderedTextBox(downAbility.Name, 25);
                var cdNormalized = downAbility.CooldownNormalized();
                ConsoleUtils.DrawImageAt(abilityDownText, 41, 39, cdNormalized, ConsoleColor.Cyan, ConsoleColor.Gray);
            }
        }

        private static void DrawBattleLog()
        {
            // Clear log
            var clearingText = new string(' ', 1000);
            ConsoleUtils.DrawWordWrappedText(clearingText, 107, 12, 34, 30);

            var text = string.Join("\n", _battle_log);
            ConsoleUtils.DrawWordWrappedText(text, 107, 12, 34, 30);
        }

        public static void Intro()
        {
            Console.Title = "Metamon";
            Console.CursorVisible = false;
            Console.Clear();

            var titleLines = TITLE.Split('\n');
            for (int i = 0; i < titleLines.Length; i++)
            {
                var line = titleLines[i];
                ConsoleUtils.DrawImageAt(line, 1, 1 + i);
                Thread.Sleep(300);
            }

            ConsoleUtils.DrawWordWrappedText(GUIDE, 1, 14, 80, 80);
        }

        public static void Outro_Defeat()
        {
            Console.Clear();

            var titleLines = TITLE.Split('\n');
            for (int i = 0; i < titleLines.Length; i++)
            {
                var line = titleLines[i];
                ConsoleUtils.DrawImageAt(line, 1, 1 + i);
                Thread.Sleep(300);
            }

            ConsoleUtils.DrawImageAt(FROG_IMAGE, 50, 15, 1, ConsoleColor.Blue, ConsoleColor.Black);
            ConsoleUtils.DrawWordWrappedText(DEFEAT, 3, 16, 80, 80);
        }

        public static void Outro_Victory()
        {
            Console.Clear();

            var titleLines = TITLE.Split('\n');
            for (int i = 0; i < titleLines.Length; i++)
            {
                var line = titleLines[i];
                ConsoleUtils.DrawImageAt(line, 1, 1 + i);
                Thread.Sleep(300);
            }

            ConsoleUtils.DrawImageAt(FROG_IMAGE, 50, 15, 1, ConsoleColor.Green, ConsoleColor.Black);
            ConsoleUtils.DrawWordWrappedText(VICTORY, 3, 16, 80, 80);
        }

        private static readonly string TITLE = @"
&&&&&&&&&&    &&&&&&&&&&&&&&&&&&&&&&&&&& &&&&&&&&&&&&&&&&&&    &&&&&&&      &&&&&&&&&&&   &&&&&&&&&&&     &&&&&&&&&&&     &&&&&&&&& &&&&&&&&
&&&&&&&&&&&  &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&    &&&&&&&&     &&&&&&&&&&&  $&&&&&&&&&&&   &&&&&&&&&&&&&&&   &&&&&&&&& &&&&&&&&
 &&&&&&&&&&& &&&&&&&&&$    &&&&&&  &&&&&&&&&&& &&&&&& &&&&&   &&&&&&&&&       &&&&&&&&&& &&&&&&&&&&&  &&&&&&&    &&&&&&&   &&&&&&&&& $&&&&& 
 &&&&&&&&&&&&&&&&&&&&&X    &&&&&&        &&&&  &&&&&&  &&&&   &&&&&&&&&&      &&&&&&&&&&&&&&&&&&&&&&  &&&&&&&     &&&&&&&  &&&&&&&&&&X&&&&& 
 &&&&&&&&&&&&&&&&&&&&&X    &&&&&&&&&&&         &&&&&&        &&&&$ &&&&&&     &&&&& &&&&&&&&&&&&&&&& &&&&&&&      &&&&&&&  &&&&&&&&&&&&&&&& 
 &&&&&& &&&&&&&& &&&&&X    &&&&&&&&&&&         &&&&&&       &&&&&&&&&&&&&$    &&&&& &&&&&&&& &&&&&&& $&&&&&&      &&&&&&&  &&&&&X&&&&&&&&&& 
 &&&&&& &&&&&&&  &&&&&X    &&&&&&              &&&&&&      &&&&&&&&&&&&&&&    &&&&& &&&&&&&& &&&&&&&  &&&&&&      &&&&&&&  &&&&&X &&&&&&&&& 
 &&&&&&  &&&&&&  &&&&&$    &&&&&&  $&&&&       &&&&&&      &&&&&&   &&&&&&$   &&&&&  &&&&&&  &&&&&&&  &&&&&&&    &&&&&&&   &&&&&X  &&&&&&&& 
&&&&&&&& &&&&& &&&&&&&&&&&&&&&&&&&&&&&&&     &&&&&&&&&&  &&&&&&&&  &&&&&&&&&&&&&&&&&& &&&&& &&&&&&&&&   &&&&&&&&&&&&&&&   &&&&&&&&  &&&&&&& 
&&&&&&&&       &&&&&&&&&&&&&&&&&&&&&&&&&     &&&&&&&&&&  &&&&&&&&  &&&&&&&&&&&&&&&&&&       &&&&&&&&&     &&&&&&&&&&&     &&&&&&&&  &&&&&&& ".Trim();


        private static readonly string GUIDE = @"
Welcome to Metamon.

To your left, you will see yourself: your story starts as an egg.
Will you survive through your transformations?

Tip:
- Abilities match the arrows on the dance mat
- Red = Health


Music by Juhani Junkala from opengameart (CC0)
Game by Juhana Moilanen for the METAMORPHOSIS game jam
        ".Trim();

        private static readonly string DEFEAT = @"
Defeated

Please restart to retry.
        ".Trim();

        private static readonly string VICTORY = @"
Victory

Thanks for playing! -Juhana
";

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

    }
}