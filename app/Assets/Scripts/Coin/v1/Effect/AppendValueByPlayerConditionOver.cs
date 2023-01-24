﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByPlayerConditionOver : AppendValueByCount
    {
        public AppendValueByPlayerConditionOver(bool bCoinOwner, Duel.PlayerCondition.PlayerCondition playerCondition, int value)
            : base(playerCondition.Value, value)
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
                    { nameof(num), Math.Abs(num).ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByPlayerConditionOver), formatType, param);
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
