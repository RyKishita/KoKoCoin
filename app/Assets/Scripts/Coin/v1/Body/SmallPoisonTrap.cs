using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class SmallPoisonTrap : Scripts.Coin.Body.SetAttack.Core
    {
        public SmallPoisonTrap(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationEffect(Defines.SoundEffect.SetAttackTrap, Defines.ParticleType.Poison);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailPoison.CreatePlayerCondition(4),
                Defines.ParticleType.Poison)
        };
    }
}
