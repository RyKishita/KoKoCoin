using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class CurseSword : Scripts.Coin.Body.DirectAttack.Core
    {
        public CurseSword(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearSlash();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects => new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.UseForce(),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailCurse.CreatePlayerCondition(1),
                Defines.ParticleType.Curse)
        };
    }
}
