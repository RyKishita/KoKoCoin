using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class InfinityArrow : Scripts.Coin.Body.DirectAttack.Core
    {
        public InfinityArrow(string coinName)
            : base(coinName)
        {
            Effects = new [] { new Effect.DamageToGetCopy(coinName) };
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow("InfinityArrowB", "InfinityArrowA");
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
