using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptDuelStep
    {
        bool InterceptDuelStep(DuelManager duelManager, int targetPlayerNo, Defines.DuelPhase gamePhase, Player player);
    }
}
