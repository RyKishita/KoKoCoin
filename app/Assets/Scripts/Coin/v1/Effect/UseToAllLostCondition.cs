using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToAllLostCondition : UseTo
    {
        public UseToAllLostCondition(bool bCoinOwner, string playerConditionName)
        {
            this.bCoinOwner = bCoinOwner;
            this.playerConditionName = playerConditionName;
        }

        readonly bool bCoinOwner;
        readonly string playerConditionName;

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string conditionname = Duel.PlayerCondition.PlayerConditionDetail.GetLocalizedStringName(playerConditionName);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(conditionname), conditionname },
                };

                return GetEffectLocalizedString(nameof(UseToAllLostCondition), param);
            }
        }

        void ReceiveEventBody(DuelManager duelManager, int targetPlayerNo)
        {
            var conditnoin = duelManager.DuelData.Players[targetPlayerNo].ConditionList.GetItem(playerConditionName);
            if (conditnoin != null)
            {
                var playerCondition = conditnoin.Duplicate();
                playerCondition.Value *= -1;

                duelManager.FocusPlayer(targetPlayerNo);

                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = targetPlayerNo,
                    PlayerCondition = playerCondition
                });
            }
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterUse)
        {
            if (bCoinOwner)
            {
                ReceiveEventBody(duelManager, selectedCoinData.CoinData.OwnerPlayerNo);
            }
            else
            {
                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, nameof(UseToAllLostCondition), "対象が不明");
            }

            return UniTask.CompletedTask;
        }

        protected override UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterEndUseGuard)
        {
            int targetPlayerNo = bCoinOwner
                ? afterEndUseGuard.Use.CoinData.OwnerPlayerNo
                : afterEndUseGuard.DirectAttack.CoinData.OwnerPlayerNo;

            ReceiveEventBody(duelManager, targetPlayerNo);

            return UniTask.CompletedTask;
        }
    }
}
