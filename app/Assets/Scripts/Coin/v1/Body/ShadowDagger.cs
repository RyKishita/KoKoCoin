using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ShadowDagger : Scripts.Coin.Body.DirectAttack.Core
    {
        public ShadowDagger(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearStab(Defines.SoundEffect.DirectAttackStab);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.TurnToEvolution(Defines.CoinPosition.Hand, 2, Main.ShadowSword.name, true)
        };
    }
}
