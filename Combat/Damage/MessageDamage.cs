using Metamon.Combat.State;
using Metamon.UI;

namespace Metamon.Combat.Damage
{
    public class MessageDamage : IDamage
    {
        public string Message { get; set; } = "";

        public void DealDamage(FighterState source, FighterState target)
        {
            DuelDrawer.WriteToBattleLog(Message);
        }
    }
}