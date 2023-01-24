using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Coin.v1.Body
{
    class BlockLook : Scripts.Coin.Body.DirectAttack.Core
    {
        public BlockLook(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearEffect(
                Defines.ParticleType.Dark,
                Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToAddConditionTurn(nameof(Duel.PlayerCondition.PlayerConditionDetailConfusionMove), 5, Defines.ParticleType.ConfusionMove)
        };
    }
}
