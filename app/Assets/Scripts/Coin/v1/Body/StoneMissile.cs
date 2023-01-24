using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class StoneMissile : Scripts.Coin.Body.DirectAttack.Core
    {
        public StoneMissile(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrow("StoneMissileB", 1f, Defines.SoundEffect.DirectAttackStone);
        }
    }
}
