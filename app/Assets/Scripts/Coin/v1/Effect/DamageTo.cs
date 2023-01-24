using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    abstract class DamageTo : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public virtual bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return after is AfterDamageCoin duelEventAfterDamage &&
                selectedCoinData.CoinData.ID == duelEventAfterDamage.DamageSource.CoinData.ID;
        }

        public async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                await ReceiveEventAsync(duelManager, selectedCoinData, after as AfterDamageCoin);
            }
        }

        public abstract UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterDamageCoin duelEvent);
    }
}
