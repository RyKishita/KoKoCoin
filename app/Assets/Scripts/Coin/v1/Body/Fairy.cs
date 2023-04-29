using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Fairy : Scripts.Coin.Body.SetAttack.Core
    {
        public Fairy(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("FairyA", Defines.SoundEffect.DirectAttackBow);
        }

        public override bool IsCoexistence => true;
    }
}
