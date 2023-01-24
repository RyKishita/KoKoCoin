using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using System.Data;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToMoveCurrentAreaTagALL : UseToMove
    {
        public UseToMoveCurrentAreaTagALL(Defines.CoinPosition dstCoinPosition, Defines.CoinTag coinTag)
            : base(dstCoinPosition)
        {
            if (dstCoinPosition == Defines.CoinPosition.Field) throw new ArgumentException();

            this.coinTag = coinTag;
        }

        readonly Defines.CoinTag coinTag;

        public override string Explain
        {
            get
            {
                string dstcoinposition = Defines.GetLocalizedString(dstCoinPosition);
                string tagname = Defines.GetLocalizedString(coinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(dstcoinposition), dstcoinposition },
                    { nameof(tagname), tagname },
                };

                return GetEffectLocalizedString(nameof(UseToMoveCurrentAreaTagALL), param);
            }
        }

        protected override IEnumerable<int> GetTargetCoins(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            int areaNo = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].CurrentAreaNo;

            return duelData.FieldData
                    .AreaDatas[areaNo]
                    .GetCoinsByOwner(selectedCoinData.CoinData.OwnerPlayerNo)
                    .Where(scd => scd.GetCoinTag().HasFlag(coinTag))
                    .Select(scd => scd.CoinData.ID);
        }
    }
}
