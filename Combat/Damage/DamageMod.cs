namespace Metamon.Combat.Damage
{
    public abstract class DamageMod<T> where T : IDamage
    {
        public abstract T GetModifiedDamage(IDamage damage);
    }
}