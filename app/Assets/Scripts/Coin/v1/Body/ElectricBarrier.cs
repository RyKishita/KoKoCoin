using Assets.Scripts.Duel;
using Assets.Scripts.Duel.PlayerCondition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ElectricBarrier : Scripts.Coin.Body.Guard.Core
    {
        public ElectricBarrier(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationWall();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(2)),
            new Effect.UseToChangeCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(-2),
                Defines.ParticleType.Electric)
        };
    }
}
