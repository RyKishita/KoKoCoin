using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    public interface IEffectTriggerEvent : IEffect
    {
        bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, Duel.DuelEvent.After duelEvent);

        UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, Duel.DuelEvent.After duelEvent);
    }
}
