using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class SpikeFlower : Scripts.Coin.Body.SetAttack.Core
    {
        public SpikeFlower(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("SpikeA", Defines.SoundEffect.DirectAttackBow);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AddValueStatusBySwapSetName(300, Main.Manure.name),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailSkipDraw.CreatePlayerCondition(2),
                Defines.ParticleType.Anesthetize)
        };
    }
}
