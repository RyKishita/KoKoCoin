using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class BurningFairy : Scripts.Coin.Body.SetAttack.Core
    {
        public BurningFairy(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("FireballA", Defines.SoundEffect.DamageFire);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AddValueStatusByGetPlayerCondition(100, nameof(Duel.PlayerCondition.PlayerConditionDetailFire))
        };
    }
}
