using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToMoveCoinNameAll : UseToMove
    {
        public UseToMoveCoinNameAll(bool bCoinOwner, Defines.CoinPosition srcCoinPosition, Defines.CoinPosition dstCoinPosition, string coinName)
           : base(dstCoinPosition)
        {
            if (srcCoinPosition == dstCoinPosition) throw new ArgumentException();

            this.bCoinOwner = bCoinOwner;
            this.srcCoinPosition = srcCoinPosition;
            this.coinName = coinName;
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition srcCoinPosition;
        readonly string coinName;

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string srccoinposition = Defines.GetLocalizedString(srcCoinPosition);
                string dstcoinposition = Defines.GetLocalizedString(dstCoinPosition);
                string coinname = CoinList.Instance.GetCoin(coinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(srccoinposition), srccoinposition },
                    { nameof(dstcoinposition), dstcoinposition },
                    { nameof(coinname), coinname },
                };

                return GetEffectLocalizedString(nameof(UseToMoveCoinNameAll),param);
            }
        }

        protected override IEnumerable<int> GetTargetCoins(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

            IEnumerable<CoinData> coinDatas;
            if (srcCoinPosition == Defines.CoinPosition.Field)
            {
                coinDatas = duelData
                        .FieldData
                        .GetAllAreaCoins()
                        .Select(scd => scd.CoinData)
                        .Where(cd => bCoinOwner == (cd.OwnerPlayerNo == playerNo));
            }
            else
            {
                if (bCoinOwner)
                {
                    coinDatas = duelData.Players[playerNo]
                        .GetCoinDataList(srcCoinPosition)
                        .Items;
                }
                else
                {
                    coinDatas = duelData.Players
                        .Where(player => player.PlayerNo != playerNo)
                        .SelectMany(player => player.GetCoinDataList(srcCoinPosition).Items);
                }
            }

            return coinDatas
                .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                .Where(cd => cd.CoinName == coinName)
                .Select(cd => cd.ID);
        }
    }
}
