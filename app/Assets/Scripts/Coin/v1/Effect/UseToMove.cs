using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    abstract class UseToMove : UseTo
    {
        public UseToMove(Defines.CoinPosition dstCoinPosition)
        {
            this.dstCoinPosition = dstCoinPosition;
        }

        protected readonly Defines.CoinPosition dstCoinPosition;

        protected abstract IEnumerable<int> GetTargetCoins(DuelData duelData, SelectedCoinData selectedCoinData);

        protected override bool IsReceiveEventUse(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return base.IsReceiveEventUse(duelData, selectedCoinData, duelEvent) &&
                GetTargetCoins(duelData, selectedCoinData).Any();
        }

        protected override bool IsReceiveEventUseGuard(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return base.IsReceiveEventUseGuard(duelData, selectedCoinData, duelEvent) &&
                GetTargetCoins(duelData, selectedCoinData).Any();
        }

        void ReceiveEventBody(DuelManager duelManager, SelectedCoinData selectedCoinData)
        {
            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = GetTargetCoins(duelManager.DuelData, selectedCoinData).ToList(),
                DstCoinPosition = dstCoinPosition,
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterUse)
        {
            ReceiveEventBody(duelManager, selectedCoinData);
            return UniTask.CompletedTask;
        }

        protected override UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterEndUseGuard)
        {
            ReceiveEventBody(duelManager, selectedCoinData);
            return UniTask.CompletedTask;
        }
    }
}
