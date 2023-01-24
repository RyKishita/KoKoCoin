using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class GravityFlower : Scripts.Coin.Body.SetAttack.Core
    {
        public GravityFlower(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("GravityA", Defines.SoundEffect.DirectAttackShotGravity);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AddValueStatusBySwapSetName(400, Main.Manure.name),
            new Effect.DamageToAddConditionTurn(
                nameof(Duel.PlayerCondition.PlayerConditionDetailSkipMove),
                3,
                Defines.ParticleType.Gravity)
        };
    }
}
