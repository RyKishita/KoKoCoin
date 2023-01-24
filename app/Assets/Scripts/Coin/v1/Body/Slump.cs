using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Slump : Scripts.Coin.Body.DirectAttack.Core
    {
        public Slump(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearFallen("SlumpA", Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailConfusionMove.CreatePlayerCondition(1),
                Defines.ParticleType.ConfusionMove),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailSkipMove.CreatePlayerCondition(1),
                Defines.ParticleType.Gravity)
        };
    }
}
