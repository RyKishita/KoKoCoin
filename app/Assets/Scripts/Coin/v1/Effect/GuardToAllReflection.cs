using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class GuardToAllReflection : GuardTo
    {
        public GuardToAllReflection()
        {

        }

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(Defines.StringEnum.Attacker);
                string directattack = Defines.GetLocalizedString(Defines.StringEnum.DirectAttackCoin);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(directattack), directattack},
                };

                return GetEffectLocalizedString(nameof(GuardToAllReflection), param);
            }
        }

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterGuardNoDamage afterGuardNoDamage)
        {
            duelManager.RegistDuelEventAction(new ActionDamageCoin()
            {
                Damage = afterGuardNoDamage.DirectAttackDamage,
                DamageSource = selectedCoinData,
                ReasonPlayerNo = afterGuardNoDamage.Guard.CoinData.OwnerPlayerNo,
                DiffencePlayerNo = afterGuardNoDamage.DirectAttack.CoinData.OwnerPlayerNo
            });

            return UniTask.CompletedTask;
        }
    }
}
