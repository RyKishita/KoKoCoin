using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCoinNameExistInField : AppendValueByCount
    {
        public AppendValueByCoinNameExistInField(bool bCoinOwner, string coinName, int value)
            : base(1, value)
        {
            this.bCoinOwner = bCoinOwner;
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

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinname), coinname },
                    { nameof(num), Math.Abs(num).ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByCoinNameExistInField), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly string coinName;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData
                    .FieldData
                    .GetAllAreaCoins()
                    .Select(scd => scd.CoinData)
                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                    .Where(cd => bCoinOwner == (cd.OwnerPlayerNo == playerNo))
                    .Where(cd => cd.CoinName == coinName)
                    .Count();
        }
    }
}
