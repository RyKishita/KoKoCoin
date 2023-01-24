﻿using Assets.Scripts.Duel.PlayerCondition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ThunderPunch : Scripts.Coin.Body.DirectAttack.Core
    {
        public ThunderPunch(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearStab(Defines.SoundEffect.DamageElectric);
        }

        static readonly PlayerCondition playerCondition = PlayerConditionDetailElectric.CreatePlayerCondition(2);

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(false, playerCondition, Defines.ParticleType.Electric),
            new Effect.DamageToChangeCondition(true, playerCondition, Defines.ParticleType.Electric)
        };
    }
}
