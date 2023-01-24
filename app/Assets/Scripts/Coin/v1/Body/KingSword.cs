using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class KingSword : Scripts.Coin.Body.DirectAttack.Core
    {
        public KingSword(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearSlash();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects => new[] { new Effect.CheckUsableArea(Defines.AreaType.A, Defines.AreaType.B) };
    }
}
