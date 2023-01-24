using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Sorcerer : Scripts.Coin.Body.SetAttack.Core
    {
        public Sorcerer(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("FireballA", Defines.SoundEffect.DamageFire);
        }
    }
}
