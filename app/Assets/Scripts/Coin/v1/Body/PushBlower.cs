using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PushBlower : Scripts.Coin.Body.DirectAttack.Core
    {
        public PushBlower(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrow("PushBlowerA", 1f, Defines.SoundEffect.DirectAttackThrowWind);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] { new Effect.DamageToPushEnemy(1) };
    }
}
