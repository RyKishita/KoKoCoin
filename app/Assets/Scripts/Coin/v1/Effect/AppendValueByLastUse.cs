using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByLastUse : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectAppendValue
    {
        public AppendValueByLastUse(int value)
        {
            Value = value;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;

                var param = new Dictionary<string, string>()
                {
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByLastUse), formatType, param);
            }
        }

        public int Value { get; }

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData.Players[playerNo]
                    .Hand
                    .Items
                    .Where(coinData => coinData.ID != selectedCoinData.CoinData.ID)
                    .Any()
                ? 0
                : Value;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            return 0 < Value;
        }
    }
}
