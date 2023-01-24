using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PoisonEater : Scripts.Coin.Body.SetAttack.Core
    {
        public PoisonEater(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.UseSetToAddValueStatusByPlayerCondition(true, nameof(Duel.PlayerCondition.PlayerConditionDetailPoison), 100),
            new Effect.UseToAllLostCondition(true, nameof(Duel.PlayerCondition.PlayerConditionDetailPoison))
        };
    }
}
