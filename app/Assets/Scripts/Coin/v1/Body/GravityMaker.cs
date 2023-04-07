using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class GravityMaker : Scripts.Coin.Body.SetAttack.Core
    {
        public GravityMaker(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("GravityA", Defines.SoundEffect.DirectAttackShotGravity);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailGravity.CreatePlayerCondition(2),
                Defines.ParticleType.Gravity)
        };
    }
}
