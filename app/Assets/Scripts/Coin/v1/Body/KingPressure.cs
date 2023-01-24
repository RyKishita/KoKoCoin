using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class KingPressure : Scripts.Coin.Body.DirectAttack.Core
    {
        public KingPressure(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrow("KingPressureA", 1f, Defines.SoundEffect.DirectAttackThrowWave);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableArea(Defines.AreaType.A, Defines.AreaType.B),
            new Effect.DamageToPushEnemy(2),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailSkipMove.CreatePlayerCondition(5),
                Defines.ParticleType.Gravity)
        };
    }
}
