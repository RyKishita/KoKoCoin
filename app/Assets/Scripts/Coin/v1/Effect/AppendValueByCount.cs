using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    abstract class AppendValueByCount : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectAppendValue
    {
        public AppendValueByCount(int num, int value)
        {
            this.num = num;
            Value = value;
        }

        protected readonly int num;

        public int Value { get; }

        protected abstract int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue);

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            int count = GetCount(duelData, selectedCoinData, baseValue);

            if (0 < num)
            {
                return num <= count ? Value : 0;
            }
            else if (num < 0)
            {
                return count <= Math.Abs(num) ? Value : 0;
            }
            return count == 0 ? Value : 0;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            return 0 < Value;
        }
    }
}
