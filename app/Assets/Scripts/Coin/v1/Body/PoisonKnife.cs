﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PoisonKnife : Scripts.Coin.Body.DirectAttack.Core
    {
        public PoisonKnife(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearSlash();
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
