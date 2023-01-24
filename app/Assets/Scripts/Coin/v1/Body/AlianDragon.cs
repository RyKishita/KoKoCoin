﻿using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class AlianDragon : Scripts.Coin.Body.SetAttack.Core
    {
        public AlianDragon(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("AlianDragonA", Defines.SoundEffect.DirectAttackThrowWind);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCoinName(true, Defines.CoinPosition.Hand, Main.DimensionBeacon.name, 1),
            new Effect.AppendValuesByCoinTagInField(true, Defines.CoinTag.異次元, 100)
        };
    }
}
