using Metamon.Combat.Damage;
using Metamon.Game;

namespace Metamon.Combat.Abilities
{
    public class Ability
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Cooldown { get; private set; } = 0;
        public int CurrentCooldown { get; private set; } = 0;
        public IDamage[] Damages { get; private set; } = [];

        public Ability(string name, string description, int cooldown, IDamage[]? damages = null)
        {
            Name = name;
            Description = description;
            Cooldown = cooldown;
            Damages = damages ?? [];

            Clock.CombatTimer.OnTick += UpdateCooldown;
        }

        private void UpdateCooldown(object? sender, EventArgs e)
        {
            CurrentCooldown = Math.Max(CurrentCooldown - 1, 0);
        }

        public void Execute(Fighter self, Fighter target)
        {
            if (CurrentCooldown > 0) return;
            CurrentCooldown = Cooldown;

            foreach (var damage in Damages)
            {
                var modifiedDamage = damage.GetDamage(self.State);
                modifiedDamage.DealDamage(target.State);
            }
        }
    }
}