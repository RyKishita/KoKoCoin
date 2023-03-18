using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class MagicShield : Scripts.Coin.Body.Guard.Core
    {
        public MagicShield(string coinName, int addValue)
            : base(coinName)
        {
            Effects = new[] { new Effect.AppendProtectionByTag(Defines.CoinTag.魔法, addValue) };
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
