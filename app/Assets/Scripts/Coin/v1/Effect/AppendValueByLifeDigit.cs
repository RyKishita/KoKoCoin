using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByLifeDigit : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectAppendValue
    {
        public AppendValueByLifeDigit(bool bCoinOwner, int digit, int number, int value)
        {
            this.bCoinOwner = bCoinOwner;
            this.digit = digit;
            this.number = number;
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

                var param = new Dictionary<string, string>()
                {
                    { nameof(digit), Math.Abs(digit).ToString() },
                    { nameof(number), number.ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByLifeDigit), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly int digit;
        readonly int number;
        public int Value { get; }

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNos = bCoinOwner
                            ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                            : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
            return playerNos.Any(playerNo =>
            {

                int life = duelData.Players[playerNo].Life;
                int sup = (life / digit) % 10;
                return sup == number;
            }) ? Value : 0;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            return 0 < Value;
        }
    }
}
