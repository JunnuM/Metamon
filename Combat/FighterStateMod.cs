namespace Metamon.Combat
{
    public abstract class DefenceAttributeMod
    {
        public abstract FighterState.DefenceAttributes GetModified(FighterState.DefenceAttributes defenceAttributes);
    }

    public abstract class AttackAttributeMod
    {
        public abstract FighterState.AttackAttributes GetModified(FighterState.AttackAttributes attackAttributes);
    }
}