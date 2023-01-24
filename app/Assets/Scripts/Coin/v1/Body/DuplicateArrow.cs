using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class DuplicateArrow : Scripts.Coin.Body.DirectAttack.Core
    {
        public DuplicateArrow(string coinName, int loopTurn)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow("DuplicateArrowB", "DuplicateArrowA");
            Effects = new [] { new Effect.LoopTurnToGetCopy(Defines.CoinPosition.Hand, loopTurn, coinName) };
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
