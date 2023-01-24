using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByMoveCoinTagTo : AddValueStatusBy
    {
        public AddValueStatusByMoveCoinTagTo(
            int value,
            bool bCoinOwner,
            Defines.CoinPosition dstCoinPosition,
            Defines.CoinTag coinTag)
            : base(value)
        {
            if (dstCoinPosition == Defines.CoinPosition.None) throw new ArgumentOutOfRangeException(nameof(dstCoinPosition));
            if (dstCoinPosition == Defines.CoinPosition.Field) throw new ArgumentException("AddValueStatusByMoveCoinTagToField を使用する");
            if (coinTag == Defines.CoinTag.None) throw new ArgumentException("無効なタグ");
            this.bCoinOwner = bCoinOwner;
            this.dstCoinPosition = dstCoinPosition;
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
                string dst = Defines.GetLocalizedString(dstCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { "value", Math.Abs(Value).ToString() },
                    { nameof(dst), dst},
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByMoveCoinTagTo), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition dstCoinPosition;
        readonly Defines.CoinTag coinTag;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var owner = selectedCoinData.CoinData.OwnerPlayerNo;

            return after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer &&
                    afterMoveCoinsToPlayer.DstCoinPosition == dstCoinPosition &&
                    afterMoveCoinsToPlayer.SrcItems
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
