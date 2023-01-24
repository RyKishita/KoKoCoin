using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class IciclePic : Scripts.Coin.Body.DirectAttack.Core
    {
        public IciclePic(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearStab(Defines.SoundEffect.DirectAttackStab);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.TurnToMove(Defines.CoinPosition.Hand, 5, Defines.CoinPosition.Trash),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailSkipDraw.CreatePlayerCondition(2),
                Defines.ParticleType.Anesthetize)
        };
    }
}
