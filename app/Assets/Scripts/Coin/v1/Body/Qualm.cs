using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Qualm : Scripts.Coin.Body.DirectAttack.Core
    {
        public Qualm(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrow("QualmA", 1f, Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Scripts.Coin.Body.DirectAttack.IRange Range { get; } = new Scripts.Coin.Body.DirectAttack.RangeAll();

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailConfusionMove.CreatePlayerCondition(4),
                Defines.ParticleType.ConfusionMove)
        };
    }
}
