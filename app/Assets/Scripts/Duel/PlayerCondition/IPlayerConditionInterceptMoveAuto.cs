using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptMoveAuto
    {
        bool IsInterceptMoveAuto(DuelData duelData, int targetPlayerNo, Player player);

        UniTask InterceptMoveAuto(DuelManager duelManager, Player player);
    }
}
