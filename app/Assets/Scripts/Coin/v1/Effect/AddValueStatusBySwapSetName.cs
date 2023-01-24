using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusBySwapSetName: AddValueStatusBy
    {
        public AddValueStatusBySwapSetName(int value, string coinName)
            : base(value)
        {
            this.coinName = coinName;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                var param = new Dictionary<string, string>()
                {
                    { "coinname", CoinList.Instance.GetCoin(coinName).DisplayName },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusBySwapSetName), formatType, param);
            }
        }

        readonly string coinName;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            return after is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                afterMoveCoinsToSet.CoinMoveReason == Defines.CoinMoveReason.Set &&
                afterMoveCoinsToSet.SrcItems.Any(item => item.CoinID == selectedCoinData.CoinData.ID) &&
                afterMoveCoinsToSet.TrashCoinIDs
                    .Select(coinID => duelData.GetCoin(coinID))
                    .Where(coinData => coinData.OwnerPlayerNo == selectedCoinData.CoinData.OwnerPlayerNo && coinData.CoinName == coinName)
                    .Any();
        }
    }
}
