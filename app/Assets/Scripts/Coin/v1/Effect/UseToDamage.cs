using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToDamage : UseTo
    {
        public UseToDamage(bool bCoinOwner, int damage)
        {
            this.bCoinOwner = bCoinOwner;
            this.damage = damage;
        }

        readonly bool bCoinOwner;
        readonly int damage;

        public override string Explain
        {
            get
            {
                int format = bCoinOwner ? 0 : 1;

                var param = new Dictionary<string, string>()
                {
                    { nameof(damage), damage.ToString() },
                };

                return GetEffectLocalizedString(nameof(UseToDamage), format, param);
            }
        }

        void ReceiveEventBody(DuelManager duelManager, SelectedCoinData selectedCoinData, int reasonPlayerNo, int sustainPlayerNo)
        {
            duelManager.RegistDuelEventAction(new ActionDamageCoin()
            {
                Damage = damage,
                DamageSource = selectedCoinData,
                ReasonPlayerNo = reasonPlayerNo,
                DiffencePlayerNo = sustainPlayerNo
            });
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterEndUse)
        {
            if (!bCoinOwner)
            {
                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, nameof(UseToDamage), "対象が不明");
                return UniTask.CompletedTask;
            }
            ReceiveEventBody(duelManager, selectedCoinData, selectedCoinData.CoinData.OwnerPlayerNo, selectedCoinData.CoinData.OwnerPlayerNo);
            return UniTask.CompletedTask;
        }

        protected override UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterUseGuard)
        {
            int sustainPlayerNo = bCoinOwner
                ? afterUseGuard.Use.CoinData.OwnerPlayerNo
                : afterUseGuard.DirectAttack.CoinData.OwnerPlayerNo;
            ReceiveEventBody(duelManager, selectedCoinData, afterUseGuard.Use.CoinData.OwnerPlayerNo, sustainPlayerNo);
            return UniTask.CompletedTask;
        }
    }
}
