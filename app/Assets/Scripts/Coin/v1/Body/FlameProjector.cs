using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Coin.v1.Body
{
    class FlameProjector : Scripts.Coin.Body.DirectAttack.Core
    {
        public FlameProjector(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearFlame("FlameProjectorB", "FlameProjectorA");
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailFire.CreatePlayerCondition(4),
                Defines.ParticleType.Fire)
        };
    }
}
