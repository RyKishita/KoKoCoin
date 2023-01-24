using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToAddConditionTurn : UseTo
    {
        public UseToAddConditionTurn(bool bCoinOwner, string playerConditionInnerName, int maxCount, Defines.ParticleType particleType)
        {
            this.bCoinOwner = bCoinOwner;
            this.playerConditionInnerName = playerConditionInnerName;
            this.maxCount = maxCount;
            this.particleType = particleType;
        }

        readonly bool bCoinOwner;
        readonly string playerConditionInnerName;
        readonly int maxCount;
        readonly Defines.ParticleType particleType;

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string conditionname = Duel.PlayerCondition.PlayerConditionDetail.GetLocalizedStringName(playerConditionInnerName);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player},
                    { nameof(conditionname), conditionname},
                    { nameof(maxCount), maxCount.ToString()},
                };

                return GetEffectLocalizedString(nameof(UseToAddConditionTurn), param);
            }
        }

        void ReceiveEventBody(DuelManager duelManager, int targetPlayerNo, int turn)
        {
            if (turn == 0) return;
            var pc = new Duel.PlayerCondition.PlayerCondition(playerConditionInnerName, Math.Min(maxCount, turn));

            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = targetPlayerNo,
                ParticleType = particleType
            });

            duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
            {
                PlayerNo = targetPlayerNo,
                PlayerCondition = pc
            });
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterEndUse)
        {
            if (!bCoinOwner)
            {
                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, nameof(UseToAddConditionTurn), "対象が不明");
                return UniTask.CompletedTask;
            }
            ReceiveEventBody(duelManager, selectedCoinData.CoinData.OwnerPlayerNo, selectedCoinData.CoinData.Turn);
            return UniTask.CompletedTask;
        }

        protected override UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterUseGuard)
        {
            var targetPlayerNo = bCoinOwner
                ? afterUseGuard.Use.CoinData.OwnerPlayerNo
                : afterUseGuard.DirectAttack.CoinData.OwnerPlayerNo;

            ReceiveEventBody(duelManager, targetPlayerNo, selectedCoinData.CoinData.Turn);

            return UniTask.CompletedTask;
        }
    }
}
