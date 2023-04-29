using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptSelectCoin
    {
        bool InterceptSelectCoin(DuelManager duelManager, int targetPlayerNo, Defines.CoinType selectCoinType, Player player);
    }
}
