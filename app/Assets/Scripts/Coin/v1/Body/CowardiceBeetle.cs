﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class CowardiceBeetle : Scripts.Coin.Body.SetAttack.Core
    {
        public CowardiceBeetle(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.TurnToMove(Defines.CoinPosition.Field, 5, Defines.CoinPosition.Hand)
        };
    }
}