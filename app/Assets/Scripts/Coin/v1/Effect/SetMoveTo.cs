using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    abstract class SetMoveTo : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public SetMoveTo(Defines.CoinPosition dstCoinPosition, Defines.CoinMoveReason? coinMoveReason)
        {
            if (dstCoinPosition == Defines.CoinPosition.Field) throw new ArgumentException("設置から設置は不可");

            this.dstCoinPosition = dstCoinPosition;
            this.coinMoveReason = coinMoveReason;
        }

        protected readonly Defines.CoinPosition dstCoinPosition;
        protected readonly Defines.CoinMoveReason? coinMoveReason;

        public virtual bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer &&
                    afterMoveCoinsToPlayer.DstCoinPosition == dstCoinPosition &&
                    (!coinMoveReason.HasValue || afterMoveCoinsToPlayer.CoinMoveReason == coinMoveReason.Value) &&
                    afterMoveCoinsToPlayer.SrcItems
                        .Where(item => item.CoinID == selectedCoinData.CoinData.ID)
                        .Where(item => item.CoinPosition == Defines.CoinPosition.Field)
                        .Any() &&
                IsReceiveEvent(duelData, selectedCoinData, afterMoveCoinsToPlayer);
        }

        protected virtual bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
        {
            return true;
        }

        public async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, duelEvent))
            {
                await ReceiveEventAsync(duelManager, selectedCoinData, duelEvent as AfterMoveCoinsToPlayer);
            }
        }

        public abstract UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterMoveCoinsToPlayer afterMoveCoinsToPlayer);
    }
}
