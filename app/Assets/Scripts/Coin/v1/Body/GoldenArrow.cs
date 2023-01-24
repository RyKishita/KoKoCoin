using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class GoldenArrow : Scripts.Coin.Body.DirectAttack.Core
    {
        public GoldenArrow(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow("GoldenArrowB", "GoldenArrowA");
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByCoin(true, Defines.CoinPosition.Hand, 100)
        };
    }
}
