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

        }

        public override Defines.CoinType CoinType => Defines.CoinType.None;

        public override string Summary => Defines.GetLocalizedString(Defines.StringEnum.Special);

        protected override IEnumerable<string> Explains
        {
            get
            {
                yield return GetLocalizedString(nameof(NotUse));

                foreach (string explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return false;
        }
    }
}
