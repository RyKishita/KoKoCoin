using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class FairySting : Scripts.Coin.Body.DirectAttack.Core
    {
        public FairySting(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearStab(Defines.SoundEffect.DirectAttackStab);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AppendValueByLifeDigit(false, 1, 9, 600),
            new Effect.AppendValueByLifeDigit(false, 1, 8, 1200)
        };
    }
}
