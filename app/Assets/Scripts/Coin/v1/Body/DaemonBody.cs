using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class DaemonBody : Scripts.Coin.Body.SetAttack.Core
    {
        public DaemonBody(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.DirectAttackThrowMagic);
        }
    }
}
