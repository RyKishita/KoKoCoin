using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CallCoinName : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public CallCoinName(Defines.CoinPosition targetCoinPosition, string coinName, bool bAll)
        {
            if (string.IsNullOrEmpty(coinName)) throw new ArgumentNullException(nameof(coinName));
            this.targetCoinPosition = targetCoinPosition;
            this.coinName = coinName;
            this.bAll = bAll;
        }

        readonly Defines.CoinPosition targetCoinPosition;
        readonly string coinName;
        readonly bool bAll;

        public override string Explain
        {
            get
            {
                int formatType = bAll ? 0 : 1;
                string coinname = CoinList.Instance.GetCoin(coinName).DisplayName;
                string targetcoinposition = Defines.GetLocalizedString(targetCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinname), coinname },
                    { nameof(targetcoinposition), targetcoinposition },
                };

                return GetEffectLocalizedString(nameof(CallCoinName), formatType, param);
            }
        }

        IEnumerable<CoinData> GetTargetCoins(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field))
            {
                if (duelEvent is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                    afterMoveCoinsToSet.CoinMoveReason == Defines.CoinMoveReason.Set &&
                    afterMoveCoinsToSet.SrcItems
                        .Where(item => item.CoinID == selectedCoinData.CoinData.ID)
                        .Any())
                {
                    var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                    return duelData.Players[playerNo].GetCoinDataList(targetCoinPosition).Items.Where(cd => cd.CoinName == coinName);
                }
            }

            return Enumerable.Empty<CoinData>();
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return GetTargetCoins(duelData, selectedCoinData, duelEvent).Any();
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After duelEvent)
        {
            var coins = GetTargetCoins(duelManager.DuelData, selectedCoinData, duelEvent).Select(cd => cd.ID).ToList();
            if (coins.Any())
            {
                var areaNo = duelManager.DuelData.FieldData.GetContainCoinAreaNo(selectedCoinData);

                duelManager.RegistDuelEventAction(new ActionMoveCoinsToSet()
                {
                    CoinIDs = bAll ? coins : coins.Take(1).ToList(),
                    DstAreaNo = areaNo.Value,
                    CoinMoveReason = Defines.CoinMoveReason.Effect
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
