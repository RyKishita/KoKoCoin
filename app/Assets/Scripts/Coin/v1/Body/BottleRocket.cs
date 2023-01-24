using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class BottleRocket : Scripts.Coin.Body.DirectAttack.Core
    {
        public BottleRocket(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarMachineryShot("BottleRocketA", "BottleRocketB", Defines.SoundEffect.DirectAttackShotRocket);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AppendValueByCurrentArea(true, Defines.AreaType.Safe, 200)
        };
    }
}
