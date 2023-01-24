using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ConcurrentTrap : Scripts.Coin.Body.SetAttack.Core
    {
        public ConcurrentTrap(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTrap(Defines.SoundEffect.SetAttackTrap);
            Effects = new Assets.Scripts.Coin.Effect.IEffect[] { new Effect.SetSameName(coinName) };
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
