using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Diabrosis : Scripts.Coin.Body.SetAttack.Core
    {
        public Diabrosis(string coinName)
            : base(coinName)
        {
            Effects = new [] { new Effect.SetToSideCopy(coinName) };
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationPress(Defines.SoundEffect.SetAttackPress);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
