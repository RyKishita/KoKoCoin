using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    abstract class AppendValuesByCount : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectAppendValue
    {
        public AppendValuesByCount(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected abstract int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue);

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            return GetCount(duelData, selectedCoinData, baseValue) * Value;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            return 0 < Value;
        }
    }
}
