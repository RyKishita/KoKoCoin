using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ElectricShield : Scripts.Coin.Body.Guard.Core
    {
        public ElectricShield(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        static readonly Duel.PlayerCondition.PlayerCondition playerCondition = Duel.PlayerCondition.PlayerConditionDetailElectric.CreatePlayerCondition(1);

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.GuardToChangeCondition(false, playerCondition, Defines.ParticleType.Electric),
            new Effect.GuardToChangeCondition(true, playerCondition, Defines.ParticleType.Electric)
        };
    }
}
