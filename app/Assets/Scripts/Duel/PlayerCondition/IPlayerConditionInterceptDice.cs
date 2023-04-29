using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptDice
    {
        int? InterceptDice(DuelManager duelManager, int targetPlayerNo, Player player);
    }
}
