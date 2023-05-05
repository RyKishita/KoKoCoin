using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PoisonFlower : Scripts.Coin.Body.SetAttack.Core
    {
        public PoisonFlower(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("PoisonA", Defines.SoundEffect.DamagePoison);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AddValueStatusBySwapSetName(500, Main.Manure.name),
            new Effect.DamageToAddConditionTurn(
                nameof(Duel.PlayerCondition.PlayerConditionDetailPoison),
                3,
                Defines.ParticleType.Poison)
        };
    }
}
