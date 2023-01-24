using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class FirstArrow : Scripts.Coin.Body.DirectAttack.Core
    {
        public FirstArrow(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow("FirstArrowB", "FirstArrowA");
        }
    }
}
