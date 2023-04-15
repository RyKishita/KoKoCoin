using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class AlienBoundary : Scripts.Coin.Body.SetAttack.Core
    {
        public AlienBoundary(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("ConfusionMoveA", Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableCoinName(true, Defines.CoinPosition.Hand, Main.DimensionBeacon.name, 1),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailConfusionMove.CreatePlayerCondition(4),
                Defines.ParticleType.ConfusionMove)
        };
    }
}
