using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DrawToSet : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public DrawToSet()
        {

        }

        public override string Explain => GetEffectLocalizedString(nameof(DrawToSet));

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer &&
                    afterMoveCoinsToPlayer.DstCoinPosition == Defines.CoinPosition.Hand &&
                    afterMoveCoinsToPlayer.SrcItems
                        .Where(item => duelData.GetCoin(item.CoinID).OwnerPlayerNo == playerNo)
                        .Where(item => item.CoinID == selectedCoinData.CoinData.ID)
                        .Any() &&
                    duelData.FieldData
                        .AreaDatas[duelData.Players[playerNo].CurrentAreaNo]
                        .GetAreaType() != Defines.AreaType.Safe;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                var player = duelManager.DuelData.Players[playerNo];
                duelManager.RegistDuelEventAction(new ActionMoveCoinsToSet()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    CoinMoveReason = Defines.CoinMoveReason.Effect,
                    DstAreaNo = player.CurrentAreaNo,
                    IsForce = true
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
