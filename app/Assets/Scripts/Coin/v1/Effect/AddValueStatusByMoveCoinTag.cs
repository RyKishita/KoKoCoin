using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByMoveCoinTag : AddValueStatusBy
    {
        public AddValueStatusByMoveCoinTag(
            int value,
            bool bCoinOwner,
            Defines.CoinPosition srcCoinPosition,
            Defines.CoinPosition dstCoinPosition,
            Defines.CoinTag coinTag)
            : base(value)
        {
            if (srcCoinPosition == Defines.CoinPosition.None) throw new ArgumentOutOfRangeException(nameof(srcCoinPosition));
            if (dstCoinPosition == Defines.CoinPosition.None) throw new ArgumentOutOfRangeException(nameof(dstCoinPosition));
            if (srcCoinPosition != Defines.CoinPosition.Field)
            {
                if (srcCoinPosition == dstCoinPosition) throw new ArgumentException("同じ場所にはできない。エリアの場合は他のエリアへの移動がありうる");
            }
            if (coinTag == Defines.CoinTag.None) throw new ArgumentException("無効なタグ");
            this.bCoinOwner = bCoinOwner;
            this.srcCoinPosition = srcCoinPosition;
            this.dstCoinPosition = dstCoinPosition;
            this.coinTag = coinTag;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                if (srcCoinPosition == dstCoinPosition)
                {
                    formatType += 2;
                }
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                string tagname = Defines.GetLocalizedString(coinTag);
                string src = Defines.GetLocalizedString(srcCoinPosition);
                string dst = Defines.GetLocalizedString(dstCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(field), field },
                    { nameof(tagname), tagname },
                    { "value", Math.Abs(Value).ToString() },
                    { nameof(src), src},
                    { nameof(dst), dst},
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByMoveCoinTag), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition srcCoinPosition;
        readonly Defines.CoinPosition dstCoinPosition;
        readonly Defines.CoinTag coinTag;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var owner = selectedCoinData.CoinData.OwnerPlayerNo;

            if (dstCoinPosition == Defines.CoinPosition.Field)
            {
                return after is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                        afterMoveCoinsToSet.SrcItems
                            .Where(item => item.CoinPosition == srcCoinPosition)
                            .Where(item =>
                            {
                                var coinData = duelData.GetCoin(item.CoinID);
                                return coinData.OwnerPlayerNo == owner && coinData.HasCoinTag(coinTag);
                            })
                            .Any();
            }
            else
            {
                return after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer &&
                        afterMoveCoinsToPlayer.DstCoinPosition == dstCoinPosition &&
                        afterMoveCoinsToPlayer.SrcItems
                            .Where(item => item.CoinPosition == srcCoinPosition)
                            .Where(item =>
                            {
                                var coinData = duelData.GetCoin(item.CoinID);
                                return coinData.OwnerPlayerNo == owner && coinData.HasCoinTag(coinTag);
                            })
                            .Any();
            }
        }
    }
}
