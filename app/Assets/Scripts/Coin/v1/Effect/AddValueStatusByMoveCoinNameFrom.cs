using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByMoveCoinNameFrom : AddValueStatusBy
    {
        public AddValueStatusByMoveCoinNameFrom(
            int value,
            bool bCoinOwner,
            Defines.CoinPosition srcCoinPosition,
            string coinName)
            : base(value)
        {
            if (srcCoinPosition == Defines.CoinPosition.None) throw new ArgumentOutOfRangeException(nameof(srcCoinPosition));
            this.bCoinOwner = bCoinOwner;
            this.srcCoinPosition = srcCoinPosition;
            this.coinName = coinName;
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
                string coinname = CoinList.Instance.GetCoin(coinName).DisplayName;
                string src = Defines.GetLocalizedString(srcCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { "value", Math.Abs(Value).ToString() },
                    { nameof(coinname), coinname },
                    { nameof(src), src },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByMoveCoinNameFrom), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition srcCoinPosition;
        readonly string coinName;

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
                                return coinData.OwnerPlayerNo == owner && coinData.CoinName == coinName;
                            })
                            .Any();
            }
            else if (after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
            {
                return  afterMoveCoinsToPlayer.SrcItems
                            .Where(item => item.CoinPosition == srcCoinPosition)
                            .Where(item =>
                            {
                                var coinData = duelData.GetCoin(item.CoinID);
                                return coinData.OwnerPlayerNo == owner && coinData.CoinName == coinName;
                            })
                            .Any();
            }
            return false;
        }
    }
}
