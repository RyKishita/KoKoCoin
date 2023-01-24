using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class GoldenTrap : Scripts.Coin.Body.SetAttack.Core
    {
        public GoldenTrap(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTrap(Defines.SoundEffect.SetAttackTrap);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByCoin(true, Defines.CoinPosition.Hand, 200)
        };
    }
}
