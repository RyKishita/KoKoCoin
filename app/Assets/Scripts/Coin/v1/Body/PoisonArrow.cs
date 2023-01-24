using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PoisonArrow : Scripts.Coin.Body.DirectAttack.Core
    {
        public PoisonArrow(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow("PoisonArrowB", "PoisonArrowA");
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailPoison.CreatePlayerCondition(2),
                Defines.ParticleType.Poison)
        };
    }
}
