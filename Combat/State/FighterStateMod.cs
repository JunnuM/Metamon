using Metamon.Combat.State;

namespace Metamon.Combat
{
    public abstract class DefenceAttributeMod
    {
        public abstract DefenceAttributes GetModified(DefenceAttributes defenceAttributes);
    }

    public abstract class AttackAttributeMod
    {
        public abstract AttackAttributes GetModified(AttackAttributes attackAttributes);
    }
}