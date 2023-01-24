using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Planter : Scripts.Coin.Body.SetAttack.Core
    {
        public Planter(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationSurfacing("PlanterA", 2f, Defines.SoundEffect.SetAttackSurfacing);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByCoinTagInField(true, Defines.CoinTag.植物, 100),
            new Effect.SetMoveToSideCopy(Defines.CoinPosition.Trash, null, Main.Manure.name)
        };
    }
}
