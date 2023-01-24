using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class DozeFog : Scripts.Coin.Body.SetAttack.Core
    {
        public DozeFog(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("ConfusionMoveA", Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailConfusionMove.CreatePlayerCondition(6),
                Defines.ParticleType.ConfusionMove)
        };
    }
}
