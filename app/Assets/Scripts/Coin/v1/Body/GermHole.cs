using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class GermHole : Scripts.Coin.Body.SetAttack.Core
    {
        public GermHole(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationSurfacingMe(Defines.SoundEffect.SetAttackSurfacing);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AddConditionToAreaBySet(Duel.PlayerCondition.PlayerConditionDetailVirus.CreatePlayerCondition(3))
        };
    }
}
