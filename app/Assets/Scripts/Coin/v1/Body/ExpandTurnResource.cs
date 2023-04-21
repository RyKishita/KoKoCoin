using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ExpandTurnResource : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentExpandTurnResource
    {
        public ExpandTurnResource(string coinName)
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
                    var resource = Defines.GetLocalizedString(Defines.StringEnum.Resource);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(resource), resource },
                        { "value", Math.Abs(EnvironmentValue).ToString() },
                    };

                    yield return GetLocalizedString(nameof(ExpandTurnResource), format, param);
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
