using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByDiffOwnerSetCoinUnder : AppendValueByCount
    {
        public AppendValueByDiffOwnerSetCoinUnder(int num, int value)
            : base(-num, value)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);

                var param = new Dictionary<string, string>()
                {
                    { nameof(setcoin), setcoin },
                    { nameof(num), num.ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByDiffOwnerSetCoinUnder), formatType, param);
            }
        }

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            return duelData.FieldData
                        .GetAreas()
                        .Count(area => 2 <= area.Coins.Select(scd => scd.CoinData.OwnerPlayerNo).Distinct().Count());
        }
    }
}
