using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendProtectionByTag : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectAppendProtection
    {
        public AppendProtectionByTag(Defines.CoinTag coinTag, int value)
        {
            this.coinTag = coinTag;
            Value = value;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string tagname = Defines.GetLocalizedString(coinTag);
                string cointype = Defines.GetLocalizedString(Defines.CoinType.DirectAttack);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { nameof(cointype), cointype },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendProtectionByTag), formatType, param);
            }
        }

        readonly Defines.CoinTag coinTag;

        public int Value { get; }

        public int GetAppendProtection(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData directAttackSelectedCoinData)
        {
            return directAttackSelectedCoinData.GetCoinTag().HasFlag(coinTag)
                    ? Value
                    : 0;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            return 0 < Value;
        }
    }
}
