using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToMultipleCondition : UseTo
    {
        public UseToMultipleCondition(bool bCoinOwner, string playerConditionInnerName, int value, Defines.ParticleType particleType)
        {
            if (value < 2) throw new ArgumentOutOfRangeException(nameof(value));
            this.bCoinOwner = bCoinOwner;
            this.playerConditionInnerName = playerConditionInnerName;
            this.value = value;
            this.particleType = particleType;
        }

        readonly bool bCoinOwner;
        readonly string playerConditionInnerName;
        readonly int value;
        readonly Defines.ParticleType particleType;

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string conditionname = Duel.PlayerCondition.PlayerConditionDetail.GetLocalizedStringName(playerConditionInnerName);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(conditionname), conditionname },
                    { nameof(value), value.ToString() },
                };

                return GetEffectLocalizedString(nameof(UseToMultipleCondition), param);
            }
        }

        protected override bool IsReceiveEventUse(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return base.IsReceiveEventUse(duelData, selectedCoinData, duelEvent) &&
                    duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].ConditionList.Has(playerConditionInnerName);
        }

        protected override bool IsReceiveEventUseGuard(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return base.IsReceiveEventUseGuard(duelData, selectedCoinData, duelEvent) &&
                    duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].ConditionList.Has(playerConditionInnerName);
        }

        void ReceiveEventBody(DuelManager duelManager, int targetPlayerNo)
        {
            var pc = duelManager.DuelData.Players[targetPlayerNo].ConditionList.GetItem(playerConditionInnerName);
            if (pc != null)
            {
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = targetPlayerNo,
                    ParticleType = particleType
                });

                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = targetPlayerNo,
                    PlayerCondition = new Duel.PlayerCondition.PlayerCondition(playerConditionInnerName, pc.Value * (value - 1)),
                });
            }
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterEndUse)
        {
            if (!bCoinOwner)
            {
                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, nameof(UseToMultipleCondition), "対象が不明");
                return UniTask.CompletedTask;
            }
            ReceiveEventBody(duelManager, selectedCoinData.CoinData.OwnerPlayerNo);
            return UniTask.CompletedTask;
        }

        protected override UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterUseGuard)
        {
            var targetPlayerNo = bCoinOwner
                ? afterUseGuard.Use.CoinData.OwnerPlayerNo
                : afterUseGuard.DirectAttack.CoinData.OwnerPlayerNo;
            ReceiveEventBody(duelManager, targetPlayerNo);
            return UniTask.CompletedTask;
        }
    }
}
