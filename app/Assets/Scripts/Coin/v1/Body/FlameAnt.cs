using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class FlameAnt : Scripts.Coin.Body.SetAttack.Core
    {
        public FlameAnt(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("FireballA", Defines.SoundEffect.DamageFire);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AddValueStatusBySetCoinName(200, true, Main.QueenAnt.name),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailFire.CreatePlayerCondition(3),
                Defines.ParticleType.Fire)
        };
    }
}
