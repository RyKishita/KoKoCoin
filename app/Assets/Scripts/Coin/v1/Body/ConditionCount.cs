using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ConditionCount : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentConditionCount
    {
        public ConditionCount(string coinName, int value)
            : base(coinName)
        {
            EnvironmentValue = value;
        }

        public int EnvironmentValue { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    int format = (0 < EnvironmentValue) ? 0 : 1;
                    var abnormalstate = Defines.GetLocalizedString(Defines.StringEnum.AbnormalState);
                    var count = Defines.GetLocalizedString(Defines.StringEnum.Count);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(abnormalstate), abnormalstate },
                        { nameof(count), count },
                        { "value", Math.Abs(EnvironmentValue).ToString() },
                    };

                    yield return GetLocalizedString(nameof(ConditionCount), format, param);
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
