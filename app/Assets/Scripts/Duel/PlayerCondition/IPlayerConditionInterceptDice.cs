using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptDice
    {
        bool IsInterceptDice(DuelData duelData, int targetPlayerNo, Player player);

        UniTask<int> InterceptDice(DuelManager duelManager, Player player);
    }
}
