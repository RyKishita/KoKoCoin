using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    public abstract class GuardTo : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public virtual bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return after is AfterGuardNoDamage afterGuardNoDamage &&
                afterGuardNoDamage.Guard.CoinData.ID == selectedCoinData.CoinData.ID;
        }

        public async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                await ReceiveEventAsync(duelManager, selectedCoinData, after as AfterGuardNoDamage);
            }
        }

        public abstract UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterGuardNoDamage afterGuardNoDamage);
    }
}
