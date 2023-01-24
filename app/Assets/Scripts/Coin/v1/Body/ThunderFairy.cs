using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ThunderFairy : Scripts.Coin.Body.SetAttack.Core
    {
        public ThunderFairy(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("ThunderA", Defines.SoundEffect.DamageElectric);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AddValueStatusByGetPlayerCondition(100, nameof(Duel.PlayerCondition.PlayerConditionDetailElectric))
        };
    }
}
