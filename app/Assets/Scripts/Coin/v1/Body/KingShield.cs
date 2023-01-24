using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class KingShield : Scripts.Coin.Body.Guard.Core
    {
        public KingShield(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects => new[] { new Effect.CheckUsableArea(Defines.AreaType.A, Defines.AreaType.B) };
    }
}
