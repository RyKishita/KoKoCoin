using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByGetPlayerCondition : AddValueStatusBy
    {
        public AddValueStatusByGetPlayerCondition(int value, string playerConditionInnerName)
            : base(value)
        {
            this.playerConditionInnerName  = playerConditionInnerName;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                string conditionName = Duel.PlayerCondition.PlayerConditionDetail.GetLocalizedStringName(playerConditionInnerName);

                var param = new Dictionary<string, string>()
                {
                    { nameof(field), field },
                    { nameof(conditionName), conditionName },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByGetPlayerCondition), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly string playerConditionInnerName;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            return duelEvent is AfterAddPlayerCondition afterAddCondition &&
                    afterAddCondition.PlayerCondition.InnerName == playerConditionInnerName &&
                    0 < afterAddCondition.PlayerCondition.Value;
        }
    }
}
