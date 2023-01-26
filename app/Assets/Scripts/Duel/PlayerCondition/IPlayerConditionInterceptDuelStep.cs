using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptDuelStep
    {
        bool IsInterceptDuelStep(DuelData duelData, int targetPlayerNo, Player player, Defines.DuelPhase gamePhase);

        UniTask InterceptDuelStep(DuelManager duelManager, Player player);
    }
}
