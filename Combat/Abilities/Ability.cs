using Metamon.Combat.Damage;

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
        }

        public void Execute(Fighter self, Fighter target)
        {
            foreach (var damage in Damages)
            {
                var modifiedDamage = damage.GetDamage(self.State);
                modifiedDamage.DealDamage(target.State);
            }
        }
    }
}