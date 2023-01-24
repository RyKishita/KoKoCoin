using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByPlayerCondition : AppendValuesByCount
    {
        public AppendValuesByPlayerCondition(bool bCoinOwner, string playerConditionInnerName, int value)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
            this.playerConditionInnerName = playerConditionInnerName;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    format += 2;
                }
                string conditionname = Duel.PlayerCondition.PlayerConditionDetail.GetLocalizedStringName(playerConditionInnerName);

                var param = new Dictionary<string, string>()
                {
                    { nameof(conditionname), conditionname},
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByPlayerCondition), format, param);
            }
        }

        readonly bool bCoinOwner;
        readonly string playerConditionInnerName;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNos = bCoinOwner
                            ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                            : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
            return playerNos.Max(playerNo =>
            {
                var hasStatus = duelData.Players[playerNo].ConditionList.GetItem(playerConditionInnerName);
                if (hasStatus == null) return 0;
                return hasStatus.Value;
            });
        }
    }
}
