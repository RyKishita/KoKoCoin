using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ConcurrentShield : Scripts.Coin.Body.Guard.Core
    {
        public ConcurrentShield(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();

            Effects = new Assets.Scripts.Coin.Effect.IEffect[] {
                new Effect.AppendValueBySameName(),
                new Effect.UseToMoveCoinNameAll(true, Defines.CoinPosition.Hand, Defines.CoinPosition.Trash, coinName)
            };
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
