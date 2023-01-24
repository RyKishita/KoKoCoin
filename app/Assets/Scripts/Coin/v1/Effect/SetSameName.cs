using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class SetSameName : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public SetSameName(string coinName)
        {
            this.coinName = coinName;
        }

        readonly string coinName;

        public override string Explain
        {
            get
            {
                string name = CoinList.Instance.GetCoin(coinName).DisplayName;
                string handcoin = Defines.GetLocalizedString(Defines.CoinPosition.Hand);

                var param = new Dictionary<string, string>()
                {
                    { nameof(name), name },
                    { nameof(handcoin), handcoin },
                };

                return GetEffectLocalizedString(nameof(SetSameName), param);
            }
        }

        IEnumerable<int> GetTargetCoinIDs(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData.Players[playerNo]
                    .Hand
                    .Items
                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                    .Where(cd => cd.CoinName == selectedCoinData.CoinData.CoinName)
                    .Select(cd => cd.ID);
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return after is AfterEndUse afterUse &&
                    afterUse.Use.CoinData.ID == selectedCoinData.CoinData.ID &&
                    GetTargetCoinIDs(duelData, selectedCoinData).Any();
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                duelManager.RegistDuelEventAction(new ActionMoveCoinsToSet()
                {
                    CoinIDs = GetTargetCoinIDs(duelManager.DuelData, selectedCoinData).ToList(),
                    CoinMoveReason = Defines.CoinMoveReason.Effect,
                    DstAreaNo = duelManager.DuelData.Players[playerNo].CurrentAreaNo,
                    IsForce = true
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
