using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByMoveCoinTagToField : AddValueStatusBy
    {
        public AddValueStatusByMoveCoinTagToField(
            int value,
            bool bCoinOwner,
            Defines.CoinTag coinTag)
            : base(value)
        {
            if (coinTag == Defines.CoinTag.None) throw new ArgumentException("無効なタグ");
            this.bCoinOwner = bCoinOwner;
            this.coinTag = coinTag;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    formatType += 2;
                }
                string tagname = Defines.GetLocalizedString(coinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByMoveCoinTagToField), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinTag coinTag;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var owner = selectedCoinData.CoinData.OwnerPlayerNo;

            return after is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                    afterMoveCoinsToSet.SrcItems
                        .Where(item => item.CoinID != selectedCoinData.CoinData.ID)
                        .Where(item =>
                        {
                            var coinData = duelData.GetCoin(item.CoinID);
                            return coinData.OwnerPlayerNo == owner && coinData.HasCoinTag(coinTag);
                        })
                        .Any();
        }
    }
}
