using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByTurnZero : AppendValueByCount
    {
        public AppendValueByTurnZero(Defines.CoinPosition coinPosition, int value)
            : base(0, value)
        {
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
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByTurnZero), formatType, param);
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
