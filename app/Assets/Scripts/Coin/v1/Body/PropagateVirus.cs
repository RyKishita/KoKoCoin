using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PropagateVirus : Scripts.Coin.Body.SetAttack.Core
    {
        public PropagateVirus(string coinName)
            : base(coinName)
        {
            Effects = new Assets.Scripts.Coin.Effect.IEffect[] {
                new Effect.DamageToChangeCondition(
                    false,
                    Duel.PlayerCondition.PlayerConditionDetailVirus.CreatePlayerCondition(1),
                    Defines.ParticleType.Virus),
                new Effect.AfterStopSideCopy(coinName),
            };
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationPress(Defines.SoundEffect.SetAttackPress);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
