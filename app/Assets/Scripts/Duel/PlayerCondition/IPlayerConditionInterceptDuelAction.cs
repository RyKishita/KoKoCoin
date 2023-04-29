using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptDuelAction
    {
        bool InterceptDuelAction(DuelManager duelManager, Player player, DuelEvent.Action duelEventAction);
    }
}
