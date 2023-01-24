using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class DaemonLeftArm : Scripts.Coin.Body.SetAttack.Core
    {
        public DaemonLeftArm(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("FireballA", Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CallCoinName(Defines.CoinPosition.Stock, Main.DaemonBody.name, false),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailFire.CreatePlayerCondition(2),
                Defines.ParticleType.Fire)
        };

        public override bool IsAppendPut => true;
    }
}
