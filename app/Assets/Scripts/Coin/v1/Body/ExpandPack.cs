using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ExpandPack : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentExpandPack
    {
        public ExpandPack(string coinName)
            : base(coinName)
        {

        }

        public int EnvironmentValue => 1;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    int format = (0 < EnvironmentValue) ? 0 : 1;
                    var handcoin = Defines.GetLocalizedString(Defines.CoinPosition.Hand);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(handcoin), handcoin },
                        { "value", Math.Abs(EnvironmentValue).ToString() },
                    };

                    yield return GetLocalizedString(nameof(ExpandPack), format, param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsNoReverseSet => true;
    }
}
