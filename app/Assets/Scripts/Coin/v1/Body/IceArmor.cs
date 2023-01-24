using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class IceArmor : Scripts.Coin.Body.Guard.Core
    {
        public IceArmor(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationBarrier();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] { new Effect.AppendValuesByTurn(Defines.CoinPosition.Hand, -100, 10) };
    }
}
