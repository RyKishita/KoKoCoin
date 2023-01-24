using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class IcicleClub : Scripts.Coin.Body.DirectAttack.Core
    {
        public IcicleClub(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearImpact();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.TurnToMove(Defines.CoinPosition.Hand, 5, Defines.CoinPosition.Trash)
        };
    }
}
