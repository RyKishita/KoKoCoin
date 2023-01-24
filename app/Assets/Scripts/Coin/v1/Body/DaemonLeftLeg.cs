using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class DaemonLeftLeg : Scripts.Coin.Body.SetAttack.Core
    {
        public DaemonLeftLeg(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("ConfusionMoveA", Defines.SoundEffect.DamageMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CallCoinName(Defines.CoinPosition.Stock, Main.DaemonBody.name, false),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailConfusionMove.CreatePlayerCondition(2),
                Defines.ParticleType.ConfusionMove)
        };

        public override bool IsAppendPut => true;
    }
}
