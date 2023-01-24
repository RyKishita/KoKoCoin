using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToEmptyResource : UseTo
    {
        public UseToEmptyResource(bool bCoinOwner)
        {
            this.bCoinOwner = bCoinOwner;
        }

        readonly bool bCoinOwner;

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string resource = Defines.GetLocalizedString(Defines.StringEnum.Resource);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player},
                    { nameof(resource), resource},
                };

                return GetEffectLocalizedString(nameof(UseToEmptyResource), param);
            }
        }

        void ReceiveEventBody(DuelManager duelManager, int targetPlayerNo)
        {
            if (0 != duelManager.DuelData.Players[targetPlayerNo].TurnResource)
            {
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = targetPlayerNo,
                    ParticleType = Defines.ParticleType.EmptyResource
                });
                duelManager.RegistDuelEventAction(new ActionChangeResource()
                {
                    PlayerNo = targetPlayerNo,
                    NewResource = 0
                });
            }
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterEndUse)
        {
            if (!bCoinOwner)
            {
                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, nameof(UseToEmptyResource), "対象が不明");
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
