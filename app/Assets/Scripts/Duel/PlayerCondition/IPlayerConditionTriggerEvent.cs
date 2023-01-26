using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionTriggerEvent
    {
        bool IsReceiveEvent(DuelData duelData, Player player, After duelEvent);

        UniTask ReceiveEventAsync(DuelManager duelManager, Player player, After duelEvent);
    }
}
