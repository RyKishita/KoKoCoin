using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ExpandArea : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentExpandArea
    {
        public ExpandArea(string coinName)
            : base(coinName)
        {

        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                yield return GetLocalizedString(nameof(ExpandArea));

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsNoReverseSet => true;
    }
}
