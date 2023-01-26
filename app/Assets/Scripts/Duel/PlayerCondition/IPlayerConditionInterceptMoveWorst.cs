using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptMoveWorst
    {
        bool IsInterceptMoveWorst(DuelData duelData, int targetPlayerNo, Player player);

        UniTask InterceptMoveWorst(DuelManager duelManager, Player player);
    }
}
