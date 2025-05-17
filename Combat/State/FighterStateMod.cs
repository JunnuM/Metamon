using Metamon.Combat.State;
using Metamon.Game;
using Metamon.UI;

namespace Metamon.Combat
{
    public abstract class DefenceAttributeMod
    {
        public string Name { get; set; } = "";
        public int Duration { get; set; } = 1;
        private int _duration_left = 1;
        private Fighter? _attached_to;

        public void AttachTo(Fighter target)
        {
            _duration_left = Duration;
            _attached_to = target;
            target.State.DefenceAttrMods.Add(this);

            Clock.CombatTimer.OnTick += UpdateTimer;
        }

        private void UpdateTimer(object? sender, EventArgs e)
        {
            _duration_left -= 1;
            if (_duration_left <= 0)
            {
                _attached_to?.State.DefenceAttrMods.Remove(this);
                Clock.CombatTimer.OnTick -= UpdateTimer;
                DuelDrawer.WriteToBattleLog($"{Name} has run out on {_attached_to?.State.Name}");
            }
        }

        public abstract DefenceAttributes GetModified(DefenceAttributes defenceAttributes);
    }

    public abstract class AttackAttributeMod
    {
        public string Name { get; set; } = "";
        public int Duration { get; set; } = 1;
        private int _duration_left = 1;
        private Fighter? _attached_to;

        public void AttachTo(Fighter target)
        {
            _duration_left = Duration;
            _attached_to = target;
            target.State.AttackAttrMods.Add(this);

            Clock.CombatTimer.OnTick += UpdateTimer;
        }

        private void UpdateTimer(object? sender, EventArgs e)
        {
            _duration_left -= 1;
            if (_duration_left <= 0)
            {
                _attached_to?.State.AttackAttrMods.Remove(this);
                Clock.CombatTimer.OnTick -= UpdateTimer;
                DuelDrawer.WriteToBattleLog($"{Name} has run out on {_attached_to?.State.Name}");
            }
        }

        public abstract AttackAttributes GetModified(AttackAttributes attackAttributes);
    }
}