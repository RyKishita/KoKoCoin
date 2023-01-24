using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByUseCoinTag : AddValueStatusBy
    {
        public AddValueStatusByUseCoinTag(int value, Defines.CoinTag coinTag)
            : base(value)
        {
            this.coinTag = coinTag;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                string tagname = Defines.GetLocalizedString(coinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(field), field },
                    { nameof(tagname), tagname },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByUseCoinTag), formatType, param);
            }
        }

        readonly Defines.CoinTag coinTag;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            return duelEvent is AfterEndUse afterUse &&
                    afterUse.Use.GetCoinTag().HasFlag(coinTag);
        }
    }
}
