using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByCoinTagInField : AppendValuesByCount
    {
        public AppendValuesByCoinTagInField(bool bCoinOwner, Defines.CoinTag coinTag, int value)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinTag = coinTag;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    format += 2;
                }
                string tagname = Defines.GetLocalizedString(coinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByCoinTagInField), format, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinTag coinTag;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData
                    .FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                    .Where(scd => bCoinOwner == (scd.CoinData.OwnerPlayerNo == playerNo))
                    .Where(scd => scd.GetCoinTag().HasFlag(coinTag))
                    .Count();
        }
    }
}
