using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PlantNeedle : Scripts.Coin.Body.DirectAttack.Core
    {
        public PlantNeedle(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearStab(Defines.SoundEffect.DirectAttackStab);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByCoinTagInField(true, Defines.CoinTag.植物, 150)
        };
    }
}
