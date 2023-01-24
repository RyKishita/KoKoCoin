using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByMoveCoinTagFrom : AddValueStatusBy
    {
        public AddValueStatusByMoveCoinTagFrom(
            int value,
            bool bCoinOwner,
            Defines.CoinPosition srcCoinPosition,
            Defines.CoinTag coinTag)
            : base(value)
        {
            if (srcCoinPosition == Defines.CoinPosition.None) throw new ArgumentOutOfRangeException(nameof(srcCoinPosition));
            if (coinTag == Defines.CoinTag.None) throw new ArgumentException("無効なタグ");
            this.bCoinOwner = bCoinOwner;
            this.srcCoinPosition = srcCoinPosition;
            this.coinTag = coinTag;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                string tagname = Defines.GetLocalizedString(coinTag);
                string src = Defines.GetLocalizedString(srcCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(field), field },
                    { nameof(tagname), tagname },
                    { "value", Math.Abs(Value).ToString() },
                    { nameof(src), src},
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByMoveCoinTagFrom), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition srcCoinPosition;
        readonly Defines.CoinTag coinTag;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var owner = selectedCoinData.CoinData.OwnerPlayerNo;

            if (after is AfterMoveCoinsToSet afterMoveCoinsToSet)
            {
                return  afterMoveCoinsToSet.SrcItems
                            .Where(item => item.CoinPosition == srcCoinPosition)
                            .Where(item =>
                            {
                                var coinData = duelData.GetCoin(item.CoinID);
                                return coinData.OwnerPlayerNo == owner && coinData.HasCoinTag(coinTag);
                            })
                            .Any();
            }
            else if (after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
            {
                return afterMoveCoinsToPlayer.SrcItems
                            .Where(item => item.CoinPosition == srcCoinPosition)
                            .Where(item =>
                            {
                                var coinData = duelData.GetCoin(item.CoinID);
                                return coinData.OwnerPlayerNo == owner && coinData.HasCoinTag(coinTag);
                            })
                            .Any();
            }
            return false;
        }
    }
}
