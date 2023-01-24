using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class SpikeShield : Scripts.Coin.Body.Guard.Core
    {
        public SpikeShield(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] 
        {
            new Effect.AppendProtectionByTag(Defines.CoinTag.接触, 700),
            new Effect.GuardToDamage(false, 200)
        };
    }
}
