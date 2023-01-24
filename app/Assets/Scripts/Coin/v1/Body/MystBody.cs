using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class MystBody : Scripts.Coin.Body.Guard.Core
    {
        public MystBody(string coinName, string guardPrefabName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationSwap(guardPrefabName);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] { new Effect.AppendProtectionByTag(Defines.CoinTag.刺突, 1200) };
    }
}
