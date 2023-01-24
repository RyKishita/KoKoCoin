using Assets.Scripts.Duel.PlayerCondition;
using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class AppendConditionDamage : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentAppendConditionDamage
    {
        public AppendConditionDamage(string coinName, string playerConditionInnerName, int value)
            : base(coinName)
        {
            PlayerConditionInnerName = playerConditionInnerName;
            EnvironmentValue = value;
        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    int formatType = (0 < EnvironmentValue) ? 0 : 1;

                    string name = PlayerConditionDetail.GetLocalizedStringName(PlayerConditionInnerName);
                    string value = Math.Abs(EnvironmentValue).ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(name), name },
                        { nameof(value), value },
                    };

                    yield return GetLocalizedString(nameof(AppendConditionDamage), formatType, param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public string PlayerConditionInnerName { get; }

        public int EnvironmentValue { get; }
    }
}
