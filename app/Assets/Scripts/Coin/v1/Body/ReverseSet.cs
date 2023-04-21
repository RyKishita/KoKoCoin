using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ReverseSet : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentReverseSet
    {
        public ReverseSet(string coinName)
            : base(coinName)
        {

        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var hiddenside = Defines.GetLocalizedString(Defines.StringEnum.HiddenSide);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(hiddenside), hiddenside},
                    };

                    yield return GetLocalizedString(nameof(ReverseSet), param);
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
