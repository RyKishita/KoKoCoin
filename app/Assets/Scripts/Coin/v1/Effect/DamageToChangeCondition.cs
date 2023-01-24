using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Duel.PlayerCondition;
using Cysharp.Threading.Tasks;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DamageToChangeCondition : DamageTo
    {
        public DamageToChangeCondition(bool bCoinOwner, PlayerCondition playerCondition, Defines.ParticleType particleType)
        {
            this.bCoinOwner = bCoinOwner;
            this.playerCondition = playerCondition;
            this.particleType = particleType;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < playerCondition.Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    formatType += 2;
                }
                string conditionname = playerCondition.GetDisplayName();
                string value = Math.Abs(playerCondition.Value).ToString();

                var param = new Dictionary<string, string>()
                {
                    { nameof(conditionname), conditionname},
                    { nameof(value), value },
                };

                return GetEffectLocalizedString(nameof(DamageToChangeCondition), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly PlayerCondition playerCondition;
        readonly Defines.ParticleType particleType;

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterDamageCoin afterDamageCoin)
        {
            var playerNo = bCoinOwner
                                ? selectedCoinData.CoinData.OwnerPlayerNo
                                : afterDamageCoin.DiffencePlayerNo;

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
