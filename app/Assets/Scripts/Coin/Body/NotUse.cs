using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Body
{
    class NotUse : Core
    {
        public NotUse(string coinName)
            : base(coinName)
        {
            Effects = new[] { new Effect.NotUse() };
        }

        public override Effect.IEffect[] Effects { get; }

        public override Defines.CoinType CoinType => Defines.CoinType.None;

        public override IEnumerable<string> Summaries
        {
            get
            {
                foreach (var summary in base.Summaries)
                {
                    yield return summary;
                }

                yield return Defines.GetLocalizedString(Defines.StringEnum.Special);
            }
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return false;
        }
    }
}
