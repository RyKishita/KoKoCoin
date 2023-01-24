using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class DaemonRightArm : Scripts.Coin.Body.SetAttack.Core
    {
        public DaemonRightArm(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("PoisonA", Defines.SoundEffect.DirectAttackShotGravity);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CallCoinName(Defines.CoinPosition.Stock, Main.DaemonBody.name, false),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailPoison.CreatePlayerCondition(2),
                Defines.ParticleType.Poison)
        };

        public override bool IsAppendPut => true;
    }
}
