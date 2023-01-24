using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Duel.PlayerCondition;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToChangeCondition : UseTo
    {
        public UseToChangeCondition(bool bCoinOwner, PlayerCondition playerCondition, Defines.ParticleType particleType)
        {
            this.bCoinOwner = bCoinOwner;
            this.playerCondition = playerCondition;
            this.particleType = particleType;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < playerCondition.Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string conditionname = playerCondition.GetDisplayName();
                string value = Math.Abs(playerCondition.Value).ToString();

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(conditionname), conditionname },
                    { nameof(value), value },
                };

                return GetEffectLocalizedString(nameof(UseToChangeCondition), format, param);
            }
        }

        readonly bool bCoinOwner;
        readonly PlayerCondition playerCondition;
        readonly Defines.ParticleType particleType;

        void ReceiveEventBody(DuelManager duelManager, int targetPlayerNo)
        {
            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = targetPlayerNo,
                ParticleType = particleType
            });

            duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
            {
                PlayerNo = targetPlayerNo,
                PlayerCondition = playerCondition
            });
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterUse)
        {
            if (!bCoinOwner)
            {
                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, nameof(UseToChangeCondition), "対象が不明");
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
