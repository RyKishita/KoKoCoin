using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Invisible : Scripts.Coin.Body.Guard.Core
    {
        public Invisible(string coinName, string guardPrefabName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationSwap(guardPrefabName);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] { new Effect.AppendProtectionByTag(Defines.CoinTag.特殊射程, 1200) };
    }
}
