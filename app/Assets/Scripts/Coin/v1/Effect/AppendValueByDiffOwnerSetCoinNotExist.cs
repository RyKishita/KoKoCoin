using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByDiffOwnerSetCoinNotExist : AppendValueByCount
    {
        public AppendValueByDiffOwnerSetCoinNotExist(int value)
            : base(0, value)
        {
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
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByDiffOwnerSetCoinNotExist), formatType, param);
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
