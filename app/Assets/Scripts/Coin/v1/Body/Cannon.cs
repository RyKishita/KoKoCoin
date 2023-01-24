using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Cannon : Scripts.Coin.Body.DirectAttack.Core
    {
        public Cannon(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarMachineryShot("CannonA", "Cannon", Defines.SoundEffect.DirectAttackShotCannon);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToAddConditionTurn(nameof(Duel.PlayerCondition.PlayerConditionDetailFire), 5, Defines.ParticleType.Fire)
        };
    }
}
