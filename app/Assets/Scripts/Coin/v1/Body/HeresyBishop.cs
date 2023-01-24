﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class HeresyBishop : Scripts.Coin.Body.SetAttack.Core
    {
        public HeresyBishop(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("MagicA", Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableCoinNameInField(true, Main.HeresyIdol.name, 1),
            new Effect.AddValueStatusByUseCoinTag(-100, Defines.CoinTag.異次元)
        };
    }
}
