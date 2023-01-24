using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class AdjustDice : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentChangeDice
    {
        public AdjustDice(string coinName, int value)
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
                    string value = Math.Abs(EnvironmentValue).ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(value), value },
                    };

                    yield return GetLocalizedString(nameof(AdjustDice), format, param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }
    }
}
