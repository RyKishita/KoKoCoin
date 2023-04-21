using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class AfterShot : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentAddUsableCoinType
    {
        public AfterShot(string coinName)
            : base(coinName)
        {

        }

        public int AddUsableCoinTypeStepNo => 1;

        public Defines.CoinType AddUsableCoinType => Defines.CoinType.DirectAttack;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string directattackcoin = Defines.GetLocalizedString(Defines.StringEnum.DirectAttackCoin);

                    var param = new Dictionary<string, string>()
                    {
                        { "no", (AddUsableCoinTypeStepNo + 1).ToString() },
                        { nameof(directattackcoin), directattackcoin },
                    };

                    yield return GetLocalizedString(nameof(AfterShot), param);
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
