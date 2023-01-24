using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByTurnUnder : AppendValueByCount
    {
        public AppendValueByTurnUnder(Defines.CoinPosition coinPosition, int num, int value)
            : base(-num, value)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
            this.coinPosition = coinPosition;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string coinposition = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinposition), coinposition },
                    { nameof(num), num.ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByTurnUnder), formatType, param);
            }
        }

        readonly Defines.CoinPosition coinPosition;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            return selectedCoinData.CoinData.Turn;
        }

        public override bool? IsAdvantageProgressedTurn(DuelData duelData)
        {
            return 0 < Value;
        }
    }
}
