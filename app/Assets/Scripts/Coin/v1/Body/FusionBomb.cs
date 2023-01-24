using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class FusionBomb : Scripts.Coin.Body.SetAttack.Core
    {
        public FusionBomb(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTrap(Defines.SoundEffect.SetAttackTrap);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.UseSetToFusion(150)
        };

        public override bool IsAppendPut => true;
    }
}
