using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class NeedleArmor : Scripts.Coin.Body.Guard.Core
    {
        public NeedleArmor(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationBarrier();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AppendProtectionByTag(Defines.CoinTag.接触, 800),
            new Effect.UseToDamage(false, 200)
        };
    }
}
