using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class HardPushBlower : Scripts.Coin.Body.DirectAttack.Core
    {
        public HardPushBlower(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrow("HardPushBlowerA", 1f, Defines.SoundEffect.DirectAttackThrowWind);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new [] { new Effect.DamageToPushEnemy(1) };
    }
}
