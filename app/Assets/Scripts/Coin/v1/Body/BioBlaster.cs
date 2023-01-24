using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class BioBlaster : Scripts.Coin.Body.DirectAttack.Core
    {
        public BioBlaster(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarShot("BioBlasterA", 1f, Defines.SoundEffect.DirectAttackShotLiquid);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailVirus.CreatePlayerCondition(3),
                Defines.ParticleType.Virus)
        };
    }
}
