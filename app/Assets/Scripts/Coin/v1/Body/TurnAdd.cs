using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class TurnAdd : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentTurnAdd
    {
        public TurnAdd(string coinName, int value)
            : base(coinName)
        {
            if (value == 0) throw new ArgumentOutOfRangeException(nameof(value));
            EnvironmentValue = value;
        }

        public int EnvironmentValue { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var formatType = (0 < EnvironmentValue) ? 0 : 1;
                    var value = Math.Abs(EnvironmentValue).ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(value), value},
                    };

                    yield return GetLocalizedString(nameof(TurnAdd), formatType, param);
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
