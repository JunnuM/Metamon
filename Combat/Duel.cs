using Metamon.Game;
using SharpDX.DirectInput;

namespace Metamon.Combat
{
    public class Duel
    {
        public Fighter PlayerFighter { get; private set; }
        public Fighter EnemyFighter { get; private set; }

        public event EventHandler<OnDuelEndedEventArgs>? OnDuelEnded;
        public class OnDuelEndedEventArgs
        {
            public Fighter Winner { get; private set; }
            public bool PlayerWon { get; private set; }

            public OnDuelEndedEventArgs(Fighter winner, bool leftWon)
            {
                Winner = winner;
                PlayerWon = leftWon;
            }
        }

        public Duel(Fighter leftFighter, Fighter rightFighter)
        {
            PlayerFighter = leftFighter;
            EnemyFighter = rightFighter;
        }

        public void Begin()
        {
            Clock.CombatTimer.Start();
            Clock.CombatTimer.OnTick += CheckDeaths;
            Clock.CombatTimer.OnTick += ControlPlayer;
            Clock.CombatTimer.OnTick += ControlEnemy;

            InitializeController();
        }

        private void ControlEnemy(object? sender, EventArgs e)
        {
            var randomAbility = GlobalRandom.NextInt(0, 5);
            if (EnemyFighter.Abilities[randomAbility] != null)
            {
                EnemyFighter.ExecuteAbility(randomAbility, PlayerFighter);
            }
        }

        private Joystick _joystick;
        private void InitializeController()
        {
            var directInput = new DirectInput();
            var gamepads = directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices);
            if (gamepads.Count == 0)
            {
                // Try joysticks too
                gamepads = directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly);
            }
            if (gamepads.Count == 0)
            {
                Console.WriteLine("No DirectInput gamepads found.");
                return;
            }

            var gamepadGuid = gamepads[0].InstanceGuid;
            _joystick = new Joystick(directInput, gamepadGuid);
            _joystick.Properties.BufferSize = 128;
            _joystick.Acquire();
        }

        private void ControlPlayer(object? sender, EventArgs e)
        {
            _joystick.Poll();
            var state = _joystick.GetCurrentState();

            if (state != null)
            {
                var buttons = state.Buttons;
                var up = buttons[2];
                var left = buttons[0];
                var right = buttons[3];
                var down = buttons[1];

                if (up) PlayerFighter.ExecuteAbility(0, EnemyFighter);
                if (left) PlayerFighter.ExecuteAbility(1, EnemyFighter);
                if (right) PlayerFighter.ExecuteAbility(2, EnemyFighter);
                if (down) PlayerFighter.ExecuteAbility(3, EnemyFighter);
            }
        }

        public void Stop()
        {
            Clock.CombatTimer.Stop();
        }

        private void CheckDeaths(object? sender, EventArgs e)
        {
            if (PlayerFighter.State.HealthAttrs.CurrentHealth <= 0)
            {
                OnDuelEnded?.Invoke(this, new OnDuelEndedEventArgs(EnemyFighter, false));
                Clock.CombatTimer.OnTick -= CheckDeaths;
            }
            else if (EnemyFighter.State.HealthAttrs.CurrentHealth <= 0)
            {
                OnDuelEnded?.Invoke(this, new OnDuelEndedEventArgs(PlayerFighter, true));
                Clock.CombatTimer.OnTick -= CheckDeaths;
            }
        }
    }
}