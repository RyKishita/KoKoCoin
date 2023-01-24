using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCurrentArea : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectAppendValue
    {
        public AppendValueByCurrentArea(bool bCoinOwner, Defines.AreaType areaType, int value)
        {
            this.bCoinOwner = bCoinOwner;
            this.areaType = areaType;
            Value = value;
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
                string areatype = areaType.ToString();

                var param = new Dictionary<string, string>()
                {
                    { nameof(areatype), areatype },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByCurrentArea), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.AreaType areaType;
        public int Value { get; }

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var areaNo = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].CurrentAreaNo;
            return areaType.HasFlag(duelData.FieldData.AreaDatas[areaNo].GetAreaType())
                    ? Value
                    : 0;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            return 0 < Value;
        }
    }
}
