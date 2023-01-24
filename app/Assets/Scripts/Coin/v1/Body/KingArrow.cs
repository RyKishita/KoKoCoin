using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class KingArrow : Scripts.Coin.Body.DirectAttack.Core
    {
        public KingArrow(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow("KingArrowB", "KingArrowA");
        }

        public override Scripts.Coin.Body.DirectAttack.IRange Range { get; } = new Scripts.Coin.Body.DirectAttack.RangeAll();

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects => new [] {
            new Effect.CheckUsableArea(Defines.AreaType.A, Defines.AreaType.B)
        };
    }
}
