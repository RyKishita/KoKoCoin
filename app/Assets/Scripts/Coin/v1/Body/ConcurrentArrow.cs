using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ConcurrentArrow : Scripts.Coin.Body.DirectAttack.Core
    {
        public ConcurrentArrow(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow("ConcurrentArrowB", "ConcurrentArrowA");
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValueBySameName(),
            new Effect.UseToMoveCoinNameAll(true, Defines.CoinPosition.Hand, Defines.CoinPosition.Trash, Main.ConcurrentArrow.name)
        };
    }
}
