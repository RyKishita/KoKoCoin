﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class CurseGhost : Scripts.Coin.Body.SetAttack.Core
    {
        public CurseGhost(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("CurseA", Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailCurse.CreatePlayerCondition(1),
                Defines.ParticleType.Curse)
        };

        public override bool IsCoexistence => true;
    }
}
