using Metamon.Combat;
using Metamon.Game;

namespace Metamon.UI
{
    public static class DuelDrawer
    {
        private static Duel _duel;
        private static List<string> _battle_log = ["<<<       Battle log       >>>"];
        private static readonly int _battle_log_max_length = 10;

        public static void Init(Duel duel)
        {
            _duel = duel;

            Console.Title = "Metamon";
            Console.CursorVisible = false;
            Console.Clear();

            // TODO Fun initialization sequence

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
            ConsoleUtils.DrawImageAt(title, 1, 1);
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
            var text = string.Join("\n", _battle_log);
            ConsoleUtils.DrawWordWrappedText(text, 107, 12, 34, 60);
        }

        private static readonly string title = @"
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
    }
}