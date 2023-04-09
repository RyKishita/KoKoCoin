using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    abstract class UseTo : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return IsReceiveEventUse(duelData, selectedCoinData, after) ||
                IsReceiveEventUseGuard(duelData, selectedCoinData, after);
        }

        protected virtual bool IsReceiveEventUse(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return after is AfterEndUse afterUse &&
                    afterUse.Use.CoinData.ID == selectedCoinData.CoinData.ID;
        }

        protected virtual bool IsReceiveEventUseGuard(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return duelEvent is AfterEndUseGuard afterUseGuard &&
                afterUseGuard.Use.CoinData.ID == selectedCoinData.CoinData.ID;
        }

        public async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEventUse(duelManager.DuelData, selectedCoinData, after))
            {
                await ReceiveEventUseBodyAsync(duelManager, selectedCoinData, after as AfterEndUse);
            }

            if (IsReceiveEventUseGuard(duelManager.DuelData, selectedCoinData, after))
            {
                await ReceiveEventUseGuardBodyAsync(duelManager, selectedCoinData, after as AfterEndUseGuard);
            }
        }

        protected virtual UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterUse)
        {
            return UniTask.CompletedTask;
        }
        
        protected virtual UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterUse)
        {
            return UniTask.CompletedTask;
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
