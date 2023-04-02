using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class AnesthetizeShield : Scripts.Coin.Body.Guard.Core
    {
        public AnesthetizeShield(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.GuardToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailSkipGuard.CreatePlayerCondition(4),
                Defines.ParticleType.Anesthetize)
        };
    }
}
