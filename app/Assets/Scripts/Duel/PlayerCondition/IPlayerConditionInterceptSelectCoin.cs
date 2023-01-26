using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptSelectCoin
    {
        bool IsInvalidSelectCoinType(DuelData duelData, int targetPlayerNo, Player player, Defines.CoinType selectCoinType);

        UniTask InterceptSelectCoin(DuelManager duelManager, Player player);
    }
}
