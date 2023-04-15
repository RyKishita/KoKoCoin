using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ShockAbsorptionWall : Scripts.Coin.Body.Guard.Core
    {
        public ShockAbsorptionWall(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationWall();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] { new Effect.AppendProtectionByTag(Defines.CoinTag.殴打, 800) };
    }
}
