using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByPlayerConditionNotHas : AppendValueByCount
    {
        public AppendValueByPlayerConditionNotHas(bool bCoinOwner, Duel.PlayerCondition.PlayerCondition playerCondition, int value)
            : base(0, value)
        {
            this.bCoinOwner = bCoinOwner;
            this.playerCondition = playerCondition;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string conditionname = playerCondition.GetDisplayName();

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(conditionname), conditionname },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByPlayerConditionNotHas), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        Duel.PlayerCondition.PlayerCondition playerCondition;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNos = bCoinOwner
                            ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                            : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
            return playerNos.Max(playerNo =>
            {
                var hasStatus = duelData.Players[playerNo].ConditionList.GetItem(playerCondition);
                if (hasStatus == null) return 0;
                return hasStatus.Value;
            });
        }
    }
}
