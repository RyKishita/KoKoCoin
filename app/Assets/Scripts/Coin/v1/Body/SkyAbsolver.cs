﻿using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class SkyAbsolver : Scripts.Coin.Body.Guard.Core
    {
        public SkyAbsolver(string coinName)
            :  base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        static readonly Duel.PlayerCondition.PlayerCondition playerCondition = Duel.PlayerCondition.PlayerConditionDetailGravity.CreatePlayerCondition(2);

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.GuardToChangeCondition(false, playerCondition, Defines.ParticleType.Gravity),
            new Effect.GuardToChangeCondition(true, playerCondition, Defines.ParticleType.Gravity)
        };
    }
}
