namespace Metamon.Combat
{
    public interface IDamage
    {
        // Get damage from status A (e.g. calculate with A's strength)
        public IDamage GetDamage(FighterState source);
        // Deal damage to status B
        public void DealDamage(FighterState target);
    }
}