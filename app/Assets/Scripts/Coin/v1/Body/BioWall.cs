﻿using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class BioWall : Scripts.Coin.Body.Guard.Core
    {
        public BioWall(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationWall();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.GuardToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailVirus.CreatePlayerCondition(2),
                Defines.ParticleType.Virus)
        };
    }
}
