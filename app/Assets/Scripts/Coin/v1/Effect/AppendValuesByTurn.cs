using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByTurn : AppendValuesByCount
    {
        public AppendValuesByTurn(Defines.CoinPosition coinPosition, int value, int maxCount)
            : base(value)
        {
            if (coinPosition == Defines.CoinPosition.None) throw new ArgumentException("指定が必要", nameof(coinPosition));
            this.coinPosition = coinPosition;
            this.maxCount = maxCount;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                string coinposition = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinposition), coinposition },
                    { "value", Math.Abs(Value).ToString() },
                    { nameof(maxCount), maxCount.ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByTurn), format, param);
            }
        }

        readonly Defines.CoinPosition coinPosition;
        readonly int maxCount;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            return Math.Min(maxCount, selectedCoinData.CoinData.Turn);
        }

        public override bool? IsAdvantageProgressedTurn(DuelData duelData)
        {
            return 0 < Value;
        }
    }
}
