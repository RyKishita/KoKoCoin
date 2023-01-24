using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class GuardToEmptyResource : GuardTo
    {
        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(Defines.StringEnum.Attacker);
                string resource = Defines.GetLocalizedString(Defines.StringEnum.Resource);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player},
                    { nameof(resource), resource},
                };

                return GetEffectLocalizedString(nameof(GuardToEmptyResource), param);
            }
        }

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterGuardNoDamage afterGuardNoDamage)
        {
            var owner = afterGuardNoDamage.DirectAttack.CoinData.OwnerPlayerNo;
            if (0 != duelManager.DuelData.Players[owner].TurnResource)
            {
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = owner,
                    ParticleType = Defines.ParticleType.EmptyResource
                });
                duelManager.RegistDuelEventAction(new ActionChangeResource()
                { 
                    PlayerNo = owner,
                    NewResource = 0
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
