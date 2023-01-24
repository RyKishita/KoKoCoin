using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class HeresyBlowgun : Scripts.Coin.Body.DirectAttack.Core
    {
        public HeresyBlowgun(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow("HeresyBlowgunB", "HeresyBlowgunA");
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableCoinNameInField(true, Main.HeresyIdol.name, 1)
        };
    }
}
