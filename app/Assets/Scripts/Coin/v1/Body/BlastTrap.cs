using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class BlastTrap : Scripts.Coin.Body.SetAttack.Core
    {
        public BlastTrap(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTrap(Defines.SoundEffect.SetAttackTrap);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new [] { new Effect.SetTurnToBlast(5, 2, 300) };
    }
}
