using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class GuardToDamage : GuardTo
    {
        public GuardToDamage(bool bCoinOwner, int damage)
        {
            this.bCoinOwner = bCoinOwner;
            this.damage = damage;
        }

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Attacker);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(damage), damage.ToString() },
                };

                return GetEffectLocalizedString(nameof(GuardToDamage), param);
            }
        }

        readonly bool bCoinOwner;
        readonly int damage;

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterGuardNoDamage afterGuardNoDamage)
        {
            var playerNo = bCoinOwner
                            ? selectedCoinData.CoinData.OwnerPlayerNo
                            : afterGuardNoDamage.DirectAttack.CoinData.OwnerPlayerNo;
            duelManager.RegistDuelEventAction(new ActionDamageCoin()
            {
                Damage = damage,
                DamageSource = selectedCoinData,
                ReasonPlayerNo = afterGuardNoDamage.Guard.CoinData.OwnerPlayerNo,
                DiffencePlayerNo = playerNo
            });

            return UniTask.CompletedTask;
        }
    }
}
