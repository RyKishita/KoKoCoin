using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Fireball : Scripts.Coin.Body.DirectAttack.Core
    {
        public Fireball(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrow("FireballA", 1f, Defines.SoundEffect.DamageFire);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailFire.CreatePlayerCondition(2),
                Defines.ParticleType.Fire)
        };
    }
}
