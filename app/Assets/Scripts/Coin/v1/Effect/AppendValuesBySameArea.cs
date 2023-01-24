using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesBySameArea : AppendValuesByCount
    {
        public AppendValuesBySameArea(bool bCoinOwner, int value)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
        }

        readonly bool bCoinOwner;

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    formatType += 2;
                }
                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);

                var param = new Dictionary<string, string>()
                {
                    { nameof(setcoin), setcoin },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesBySameArea), formatType, param);
            }
        }

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var areaNo = duelData.FieldData.GetContainCoinAreaNo(selectedCoinData);
            if (!areaNo.HasValue) return 0;
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData.FieldData
                        .AreaDatas[areaNo.Value]
                        .Coins
                        .Where(scd => bCoinOwner == (scd.CoinData.OwnerPlayerNo == playerNo))
                        .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                        .Count();
        }
    }
}
