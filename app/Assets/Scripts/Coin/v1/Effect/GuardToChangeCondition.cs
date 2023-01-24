using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class GuardToChangeCondition : GuardTo
    {
        public GuardToChangeCondition(bool bCoinOwner, Duel.PlayerCondition.PlayerCondition playerCondition, Defines.ParticleType particleType)
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
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Attacker);
                string name = playerCondition.GetDisplayName();
                string value = Math.Abs(playerCondition.Value).ToString();

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(name), name},
                    { nameof(value) , value },
                };

                return GetEffectLocalizedString(nameof(GuardToChangeCondition), format, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Duel.PlayerCondition.PlayerCondition playerCondition;
        readonly Defines.ParticleType particleType;

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterGuardNoDamage afterGuardNoDamage)
        {
            var playerNo = bCoinOwner
                        ? selectedCoinData.CoinData.OwnerPlayerNo
                        : afterGuardNoDamage.DirectAttack.CoinData.OwnerPlayerNo;

            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = playerNo,
                ParticleType = particleType
            });

            duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
            {
                PlayerNo = playerNo,
                PlayerCondition = playerCondition
            });

            return UniTask.CompletedTask;
        }
    }
}
