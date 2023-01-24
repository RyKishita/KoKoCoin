using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToMoveCurrentAreaCoinNameALL : UseToMove
    {
        public UseToMoveCurrentAreaCoinNameALL(bool bCoinOwner, Defines.CoinPosition dstCoinPosition, string coinName)
            :base(dstCoinPosition)
        {
            if (dstCoinPosition == Defines.CoinPosition.Field) throw new ArgumentException();

            this.bCoinOwner = bCoinOwner;
            this.coinName = coinName;
        }

        readonly bool bCoinOwner;
        readonly string coinName;

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string dstcoinposition = Defines.GetLocalizedString(dstCoinPosition);
                string coinname = CoinList.Instance.GetCoin(coinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(dstcoinposition), dstcoinposition },
                    { nameof(coinname), coinname },
                };

                return GetEffectLocalizedString(nameof(UseToMoveCurrentAreaCoinNameALL), param);
            }
        }

        protected override IEnumerable<int> GetTargetCoins(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            int areaNo = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].CurrentAreaNo;

            return duelData.FieldData
                    .AreaDatas[areaNo]
                    .GetCoinsByOwner(selectedCoinData.CoinData.OwnerPlayerNo)
                    .Where(scc => scc.CoinData.CoinName == coinName)
                    .Select(scc => scc.CoinData.ID);
        }
    }
}
