using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class DimensionSlash : Scripts.Coin.Body.DirectAttack.Core
    {
        public DimensionSlash(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarSlash("DimensionSlashA");
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCoinTagInField(true, Defines.CoinTag.異次元, 1)
        };
    }
}
