using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class HeavyAttack : Scripts.Coin.Body.DirectAttack.Core
    {
        public HeavyAttack(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearFallen("HeavyAttack", Defines.SoundEffect.DirectAttackThrowHeavyAttack);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects => new [] {
            new Effect.AppendValuesByPlayerCondition(null, nameof(Assets.Scripts.Duel.PlayerCondition.PlayerConditionDetailGravity), 100)
        };
    }
}
