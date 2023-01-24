using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToGetCopy : UseTo
    {
        public UseToGetCopy(bool bCoinOwner, string copiedCoinName)
        {
            this.bCoinOwner = bCoinOwner;
            this.copiedCoinName = copiedCoinName;
        }

        readonly bool bCoinOwner;
        readonly string copiedCoinName;

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player},
                    { nameof(coinname), coinname },
                };

                return GetEffectLocalizedString(nameof(UseToGetCopy), param);
            }
        }

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        void ReceiveEventBody(DuelManager duelManager, SelectedCoinData selectedCoinData, int targetPlayerNo)
        {
            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = targetPlayerNo,
                ParticleType = Defines.ParticleType.CreateCopy,
                IsWaitPlayParticle = true
            });

            var token = duelManager.DuelData.CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, targetPlayerNo);
            duelManager.RegistDuelEventAction(new ActionAddCopyToPlayer()
            {
                Target = token,
                CoinPosition = Defines.CoinPosition.Hand
            });
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterEndUse)
        {
            if (!bCoinOwner)
            {
                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, nameof(UseToGetCopy), "対象が不明");
                return UniTask.CompletedTask;
            }
            ReceiveEventBody(duelManager, selectedCoinData, selectedCoinData.CoinData.OwnerPlayerNo);
            return UniTask.CompletedTask;
        }

        protected override UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterUseGuard)
        {
            var targetPlayerNo = bCoinOwner
                ? afterUseGuard.Use.CoinData.OwnerPlayerNo
                : afterUseGuard.DirectAttack.CoinData.OwnerPlayerNo;
            ReceiveEventBody(duelManager, selectedCoinData, targetPlayerNo);
            return UniTask.CompletedTask;
        }
    }
}
